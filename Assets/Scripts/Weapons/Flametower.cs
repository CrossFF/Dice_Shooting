using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flametower : MonoBehaviour, IWeapon
{
    [Header("Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float distanceAttack;
    [SerializeField] private float timeTick;
    private bool isAttacking = false;
    private float specialAttackTime, attackTime, cronometer;

    [Header("References")]
    [SerializeField] private ParticleSystem particleNormalFlame;
    [SerializeField] private ParticleSystem particleSpecialFlame;
    [SerializeField] private ParticleSystem particlesNormalAttack;
    [SerializeField] private ParticleSystem particleSpecialAttack;
    private AnimationManager playerAnimationManager;
    private LineManager lineManager;
    private CameraController cameraController;

    void Start()
    {
        lineManager = GameObject.Find("Line Manager").GetComponent<LineManager>();
        cameraController = GameObject.Find("Camera Controller").GetComponent<CameraController>();
        playerAnimationManager = transform.parent.GetComponent<AnimationManager>();

        cronometer = 0;
        attackTime = 0;
        specialAttackTime = 0;
    }

    void Update()
    {
        // seteo el tipo de fuego del arma
        if (specialAttackTime > 0)
        {
            // ataque especial
            if (!particleSpecialFlame.isPlaying)
            {
                particleSpecialFlame.Play();
                particleNormalFlame.Stop();
            }
        }
        else
        {
            // ataque normal
            if (!particleNormalFlame.isPlaying)
            {
                particleNormalFlame.Play();
                particleSpecialFlame.Stop();
            }
        }

        // Ataque
        if (isAttacking)
        {
            // disminuyo tiempo de ataque
            attackTime -= Time.deltaTime;
            // seto el tipo de ataque
            if (specialAttackTime > 0)
            {
                specialAttackTime -= Time.deltaTime;
                if (!particleSpecialAttack.isPlaying)
                {
                    particleSpecialAttack.Play();
                    particlesNormalAttack.Stop();
                }
            }
            else
            {
                specialAttackTime = 0;
                if (!particlesNormalAttack.isPlaying)
                {
                    particlesNormalAttack.Play();
                    particleSpecialAttack.Stop();
                }
            }
            // cada un timeTick hago daño
            cronometer += Time.deltaTime;
            if (cronometer >= timeTick)
            {
                // info de enemigo en la linea
                List<Transform> tEnemys = lineManager.GetAllEnemysInLine();
                // Hago daño
                if (specialAttackTime > 0)
                {
                    List<IDamageable> allEnemys = new List<IDamageable>();
                    foreach (var item in tEnemys)
                    {
                        allEnemys.Add(item.GetComponent<IDamageable>());
                    }
                    // hago daño a todos los enemigos en la linea
                    foreach (var item in allEnemys)
                    {
                        item.GetDamage(damage);
                    }
                }
                else
                {
                    // hago daño a los enemigos que esten dentro del alcance
                    List<IDamageable> enemysInDistance = new List<IDamageable>();
                    foreach (var item in tEnemys)
                    {
                        if(Vector3.Distance(transform.position, item.position) <= distanceAttack)
                        enemysInDistance.Add(item.GetComponent<IDamageable>());
                    }
                    foreach (var item in enemysInDistance)
                    {
                        item.GetDamage(damage);
                    }
                }
                // reseto
                cronometer = 0;
            }
            // si el tiempo de ataque llega a 0 dejo de atacar
            if (attackTime <= 0)
            {
                attackTime = 0;
                isAttacking = false;
                particlesNormalAttack.Stop();
                particleSpecialAttack.Stop();
            }
        }
    }

    public void ClearEffects()
    {

    }

    public void Shoot(int dice)
    {
        attackTime += dice;
        isAttacking = true;
    }

    public void Special1(int dice)
    {

    }

    public void Special2(int dice)
    {
        specialAttackTime += dice;
    }
}
