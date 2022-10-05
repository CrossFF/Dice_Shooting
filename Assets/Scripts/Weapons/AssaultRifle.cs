using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IWeapon
{
    public ParticleSystem fireFlash;
    public LineRenderer lineProyectile; // line renderer que hace como disparo
    public Transform weaponOrigin;
    public float fireRate;
    private AnimationManager playerAnimationManager;
    private int damageMultiplier = 1; // multiplicador de daño de special 2
    public GameObject prefabCover; // special 1
    private LineManager lineManager;
    //sonido
    public AudioSource audioSource;
    public AudioClip simpleShootSound;
    public AudioClip overchargeSound;

    private void Start()
    {
        lineManager = GameObject.Find("Line Manager").GetComponent<LineManager>();
        playerAnimationManager = transform.parent.GetComponent<AnimationManager>();
        lineProyectile.enabled = false;
    }

    public void Shoot(int dice)
    {
        // dispara el doble de lo que tenga el dado cantidad de veces
        int totalProyectiles = dice * 2 * damageMultiplier;
        StartCoroutine(InstantiateProjectiles(totalProyectiles));
    }

    public void Special1(int dice)
    {
        // animacion de instalar torreta
        playerAnimationManager.InstallTurret();
        // es posible crear una torrera en esta linea?
        if (!lineManager.IsTurretHere())
        {
            // conceguir coordenadas que le corresponde a la covertura
            Vector3 pos = lineManager.GetTurretPosition();
            // instanciar covertura
            GameObject temp = Instantiate(prefabCover, pos, Quaternion.identity);
            // seteo de covertura
            ITurret turret = temp.GetComponent<ITurret>();
            turret.Install(dice);
            // informo al line manager que guarde la info de la torreta en la linea}
            lineManager.SetTurret(temp.transform);

            audioSource.pitch = 1;
            if (audioSource.clip != overchargeSound) audioSource.clip = overchargeSound;
            audioSource.Play();
        }
        else
        {
            // aumento la vida de la torreta
            ITurret turret = lineManager.GetTurret().GetComponent<ITurret>();
            turret.HP += dice;
        }
    }

    public void Special2(int dice)
    {
        // los ataque disparan el doble de proyectiles
        damageMultiplier += dice;

        audioSource.pitch = 1;
        if (audioSource.clip != overchargeSound) audioSource.clip = overchargeSound;
        audioSource.Play();
    }

    public void ClearEffects()
    {
        damageMultiplier = 1;
    }

    IEnumerator InstantiateProjectiles(int proyectiles)
    {
        // efectos
        fireFlash.Play();
        playerAnimationManager.RepetitiveShootAnimation();
        // disparos
        for (int i = 0; i < proyectiles; i++)
        {
            // seteos
            // proyectiles
            lineProyectile.SetPosition(0, weaponOrigin.position);
            Transform enemy = lineManager.GetEnemy();
            Vector3 target;
            float impresision = Random.Range(-0.5f, 0.5f);
            if (enemy != null)
            {
                target = new Vector3(enemy.position.x, enemy.position.y + impresision, 0f);
            }
            else
            {
                target = new Vector3(weaponOrigin.position.x + 20,
                                     weaponOrigin.position.y + impresision,
                                     0f);
            }
            lineProyectile.SetPosition(1, target);
            // sonido        
            audioSource.pitch = 1;
            lineProyectile.enabled = true;
            // activo sonido variando su pich
            float pitch = Random.Range(-0.1f, 0.1f);
            audioSource.pitch += pitch;
            if (audioSource.clip != simpleShootSound) audioSource.clip = simpleShootSound;
            audioSource.Play();
            // aplicacion de daño
            enemy.GetComponent<IDamageable>().GetDamage(1);
            yield return new WaitForSeconds(fireRate);
            // oculto la linea del disparo
            lineProyectile.enabled = false;
        }
        fireFlash.Stop();
        playerAnimationManager.StopRepetitiveShootAnimation();
    }
}
