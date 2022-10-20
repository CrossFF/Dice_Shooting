using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTurret : MonoBehaviour, IDamageable, ITurret
{
    public float HP { get; set; }
    public Line Line { get; set; }

    [Header("Turret Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float attackTime;
    [SerializeField] private float attackDistance;
    private float cronometer;
    private bool isAttaking = false;

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

    private void Update()
    {
        if (HP > 0)
        {
            if (!particlesTurretActive.isPlaying) particlesTurretActive.Play();
            cronometer += Time.deltaTime;
            if (cronometer >= attackTime && !isAttaking)
            {
                // ataco
                isAttaking = true;
                StartCoroutine(Attack());
            }
        }
        else
        {
            if (!particlesTurretActive.isStopped) particlesTurretActive.Stop();
        }
    }

    IEnumerator Attack()
    {
        // verifico que haya un enemigo, y este a distancia de atque
        if (Line.GetEnemy() && Vector3.Distance(transform.position, Line.GetEnemy().position) <= attackDistance)
        {      
            if (!particlesAttack.isPlaying) particlesAttack.Play();
            // bajo energia de la torreta.
            HP -= 0.25f;
            // consigo la lista de enemigos en la lines
            List<Transform> enemysT = Line.GetAllEnemys();
            List<IDamageable> enemys = new List<IDamageable>();
            // los enemigos que esten a distancia de ataque
            //  reciben daño
            foreach (var item in enemysT)
            {
                if (Vector3.Distance(transform.position, item.position) <= attackDistance)
                {
                    enemys.Add(item.GetComponent<IDamageable>());
                }
            } 
            foreach (var item in enemys)
            {
                item.GetDamage(damage);
            }  
        }
        yield return new WaitForSeconds(particlesAttack.main.duration);
        isAttaking = false;
        cronometer = 0;
    }
}
