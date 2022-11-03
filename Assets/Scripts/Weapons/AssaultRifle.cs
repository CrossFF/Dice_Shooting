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
    private LineManager lineManager;
    private CameraController cameraController;
    //sonido
    public AudioSource audioSource;
    public AudioClip simpleShootSound;
    public AudioClip overchargeSound;

    private void Start()
    {
        lineManager = GameObject.Find("Line Manager").GetComponent<LineManager>();
        cameraController = GameObject.Find("Camera Controller").GetComponent<CameraController>();
        playerAnimationManager = transform.parent.GetComponent<AnimationManager>();
        lineProyectile.enabled = false;
    }

    public void Shoot(int dice)
    {
        // dispara el doble de lo que tenga el dado cantidad de veces
        int totalProyectiles = dice * 2 * damageMultiplier;
        StartCoroutine(InstantiateProjectiles(totalProyectiles));
    }

    public void Special(int dice)
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
        cameraController.Shake(1);
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
                // aplicacion de daño
                enemy.GetComponent<IDamageable>().GetDamage(1);
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
            yield return new WaitForSeconds(fireRate);
            // oculto la linea del disparo
            lineProyectile.enabled = false;
        }
        cameraController.Shake(0);
        fireFlash.Stop();
        playerAnimationManager.StopRepetitiveShootAnimation();
    }
}
