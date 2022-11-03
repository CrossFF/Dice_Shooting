using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTurret : MonoBehaviour, IDamageable, ITurret
{
    public Line Line { get; set; }
    public float HP { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    //stats
    private int explosiveCapacity = 0;

    [Header("References")]
    [SerializeField]private ParticleSystem particleExplosionCharge;
    [SerializeField]private ParticleSystem particleExplosion;

    public void Dismantle()
    {
        // efectos
        particleExplosion.Play();
        // hago daño a todos los enemigos en la linea, según la capacidad explosiva
        List<Transform> tEnemys = Line.GetAllEnemys();
        List<IDamageable> enemys = new List<IDamageable>();
        foreach (var item in tEnemys)
        {
            enemys.Add(item.GetComponent<IDamageable>());
        }
        foreach (var item in enemys)
        {
            item.GetDamage(explosiveCapacity);
        }
        // la torreta es destruida
        Destroy(gameObject, 0.5f);
    }

    public void Install(int dice)
    {
        explosiveCapacity += dice;
    }

    public void GetDamage(float damage)
    {
        // la mina explota
        Dismantle();
    }
}
