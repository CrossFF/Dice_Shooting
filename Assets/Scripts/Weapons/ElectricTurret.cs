using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTurret : MonoBehaviour, IDamageable, ITurret
{
    public float HP { get; set; }
    public Line Line { get; set; }

    [Header("Turret Stats")]
    [SerializeField] private float attackTime;
    private float cronometer;

    [Header("References")]
    [SerializeField] private ParticleSystem particlesTurretActive;
    [SerializeField] private ParticleSystem particlesAttack;

    public void Install(int dice)
    {
        // el HP de la torreta representa el tiempo que es capaz de hacer daño
        //  si el HP llega a 0 la torreta no es destruida
        //   pero no es capaz de realizar daño.
        HP += dice;
    }

    public void Dismantle()
    {
        Destroy(gameObject, 0.2f);
    }

    public void GetDamage(float damage)
    {
        // si la torreta recibe daño es destruida
        Dismantle();
    }

    private void Start()
    {
        HP = 0;
    }

    private void Update()
    {
        if (HP > 0)
        {
            if (!particlesTurretActive.isPlaying) particlesTurretActive.Play();
            HP -= Time.deltaTime;
            cronometer += Time.deltaTime;
            if (cronometer >= attackTime)
            {
                // ataco
                if (!particlesAttack.isPlaying) particlesAttack.Play();
                cronometer = 0;
            }
        }
        else
        {
            if (!particlesTurretActive.isStopped) particlesTurretActive.Stop();
        }
    }
}
