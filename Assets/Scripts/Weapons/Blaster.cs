using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : Weapon, IShootable
{
    [Header("Weapon Stats")]
    [SerializeField] private int weaponPenetration = 0;
    [SerializeField] private float inicialDamage = 1;
    private float damageBonus = 0;

    [Header("References")]
    // efectos visuales de arma
    [SerializeField] private LineRenderer lineProyectile; // visual del disparo
    [SerializeField] private Transform rayOrigin; // origen del disparo
    [SerializeField] private ParticleSystem particleChargedAttack;
    [SerializeField] private ParticleSystem particleAttack;
    // efectos sonoros de arma
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip laserShoot;

    public void ClearEffects()
    {
        damageBonus = 0;
    }

    public void Shoot(int dice)
    {
        // lanza un rayo que golpea a X cantidad de enemigos en la linea
        //  Siendo x el valor del dado usado
        weaponPenetration = dice;
        StartCoroutine(BlasterAttack());
    }

    public void Special(int dice)
    {
        // el ataque normal gana potencia
        //  probocando mas daño
        damageBonus += dice;
    }

    void Start()
    {
        SetDamage(inicialDamage);
        lineProyectile.enabled = false;
    }

    private void Update()
    {
        // particulas de artaques cargados
        if (damageBonus > 0)
        {
            if (!particleChargedAttack.isPlaying) particleChargedAttack.Play();
        }
        else
        {
            if (particleChargedAttack.isPlaying) particleChargedAttack.Stop();
        }
    }

    IEnumerator BlasterAttack()
    {
        particleAttack.Play();
        // activo vibracion de la camara segun tipo de disparo
        if(damageBonus == 0)
        {
            // vibracion de camara baja
            EquipmentReference.CameraController.Shake(0.5f);
        }
        else
        {
            // vibracion de camara alta
            EquipmentReference.CameraController.Shake(1.5f);
        }
        // activo sonido variando su pich
        audioSource.clip = laserShoot;
        audioSource.pitch = 1;
        float pitch = Random.Range(-0.1f, 0.1f);
        audioSource.pitch += pitch;
        audioSource.Play();
        // solicito los enemigos que hay en la linea
        //  segun la penetracion del blaster
        List<Transform> enemys = EquipmentReference.LineManager.GetEnemys(weaponPenetration);
        List<IDamageable> dEnemys = new List<IDamageable>();
        foreach (var item in enemys)
        {
            dEnemys.Add(item.GetComponent<IDamageable>());
        }
        // hago daño a cada uno de ellos
        foreach (var item in dEnemys)
        {
            item.GetDamage(Damage + damageBonus);
        }
        // activo animacion de ataque
        EquipmentReference.AnimationManager.ShootAnimation();
        // dibujo el ataque
        lineProyectile.SetPosition(0, rayOrigin.position);
        if (enemys.Count != 0)
        {
            lineProyectile.SetPosition(1, enemys[enemys.Count - 1].position);
        }
        else
        {
            Vector3 target = new Vector3(rayOrigin.position.x + 20,
                                         rayOrigin.position.y,
                                         0f);
            lineProyectile.SetPosition(1, target);
        }
        lineProyectile.enabled = true;

        yield return new WaitForSeconds(0.1f);

        // oculto el rayo de ataque
        lineProyectile.enabled = false;
        // detengo vibracion de camara
        EquipmentReference.CameraController.Shake(0);
    }
}
