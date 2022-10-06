using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour, IEnemy, IDamageable
{
    public Line Line { get; set; }
    [SerializeField] private Animator animator;
    [SerializeField] private float HP = 3;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float attackDistance;
    [SerializeField] private float timeBetweenAttacks;

    [Header("FXs")]
    [SerializeField] private ParticleSystem getDamage;

    private bool isAttacking = false;
    private bool alive = true;
    private bool isGetingDamage = false;
    private Transform target;

    private void Start()
    {
        animator.SetBool("Move", true);
    }

    private void Update()
    {
        #region Ataque
        if (alive)
        {
            // guardo data de objetivo en linea; torreta o jugador
            Transform t, p;
            t = Line.GetTurret();
            p = Line.GetPlayer();
            if (t != null)
            {
                target = t;
            }
            else if (p != null)
            {
                target = p;
            }

            // si hay un objetivo
            if (target)
            {
                // verifico a que distancia esta               
                // si esta cerca
                if (Vector3.Distance(transform.position, target.position) <= attackDistance)
                {
                    //// lo ataco
                    if (!isAttacking)
                    {
                        StopCoroutine("Attack");
                        StartCoroutine(Attack());
                    }
                }
                else
                {
                    if (!animator.GetBool("Move") && !isGetingDamage) animator.SetBool("Move", true);
                }
            }
            else
            {
                if (!animator.GetBool("Move") && !isGetingDamage) animator.SetBool("Move", true);
            }
        }
        #endregion

        if (animator.GetBool("Move"))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    public void GetDamage(float damage)
    {
        animator.SetBool("Move", false);
        getDamage.Play();
        if (alive)
        {
            StopAllCoroutines();
            HP -= damage;
            if (HP > 0)
            {
                // solso resive daño        
                StartCoroutine(OnlyDamage());
            }
            else
            {
                // el enemigo muere
                StartCoroutine(Death());
            }
        }
    }

    IEnumerator OnlyDamage()
    {
        isGetingDamage = true;
        animator.SetTrigger("Get Damage");
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        isGetingDamage = false;
    }

    IEnumerator Death()
    {
        alive = false;
        animator.SetTrigger("Death");
        Line.RemoveEnemy(transform);
        Line = null;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        // el dino no puede moverse
        animator.SetBool("Move", false);
        // activo animacion
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);
        // aplico daño al objetivo
        IDamageable t = target.GetComponent<IDamageable>();
        if (t != null)
        {
            t.GetDamage(damage);
        }
        StartCoroutine(ResetAttak());
    }

    IEnumerator ResetAttak()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

}
