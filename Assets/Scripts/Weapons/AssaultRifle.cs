using System.Collections;
using UnityEngine;

public class AssaultRifle : Weapon, IShootable
{
    [Header("References")]
    // efectos visuales de arma
    [SerializeField] private ParticleSystem particleFireFlash; // particulas de disparo
    [SerializeField] private LineRenderer lineProyectile; // line renderer que hace como disparo
    [SerializeField] private Transform shootOrigin; // origen del disparo
    // efectos sonoros de arma
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip simpleShootSound;
    [SerializeField] private AudioClip overchargeSound;

    [Header("Stats iniciales")]
    [SerializeField] private float fireRate;
    [SerializeField] private float inicialDamage = 1;
    [SerializeField] private int proyectilesMultipler = 1;

    public void ClearEffects()
    {
        proyectilesMultipler = 1;
    }

    public void Shoot(int dice)
    {
        // dispara el doble de lo que tenga el dado cantidad de veces
        int totalProyectiles = dice * 2 * proyectilesMultipler;
        StartCoroutine(InstantiateProjectiles(totalProyectiles));
    }

    public void Special(int dice)
    {
        // los ataque disparan el doble de proyectiles
        proyectilesMultipler += dice;
        // efecto visual y sonoro
        audioSource.pitch = 1;
        if (audioSource.clip != overchargeSound) audioSource.clip = overchargeSound;
        audioSource.Play();
    }

    private void Start()
    {
        lineProyectile.enabled = false;
        SetDamage(inicialDamage);
    }

    IEnumerator InstantiateProjectiles(int proyectiles)
    {
        // efectos
        EquipmentReference.CameraController.Shake(1);
        particleFireFlash.Play();
        EquipmentReference.AnimationManager.RepetitiveShootAnimation();
        // disparos
        for (int i = 0; i < proyectiles; i++)
        {
            // seteos
            // proyectiles
            lineProyectile.SetPosition(0, shootOrigin.position);
            Transform enemy = EquipmentReference.LineManager.GetEnemy();
            Vector3 target;
            float impresision = Random.Range(-0.5f, 0.5f);
            if (enemy != null)
            {
                target = new Vector3(enemy.position.x, enemy.position.y + impresision, 0f);
                // aplicacion de daño
                enemy.GetComponent<IDamageable>().GetDamage(Damage);
            }
            else
            {
                target = new Vector3(shootOrigin.position.x + 20,
                                     shootOrigin.position.y + impresision,
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
        EquipmentReference.CameraController.Shake(0);
        particleFireFlash.Stop();
        EquipmentReference.AnimationManager.StopRepetitiveShootAnimation();
    }
}
