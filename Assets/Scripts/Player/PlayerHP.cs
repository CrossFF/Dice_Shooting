using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamageable
{
    [SerializeField] private float hp; // para poder ponerle la vida que considere necesaria
    public float HP { get; set; } // interfaz
    private bool alive;

    // animacion
    private AnimationManager animationManager;

    // sonido
    private AudioSource audioSource;
    public AudioClip getDamageSoundClip;
    public AudioClip deadSoundClip;

    // efectos
    public ParticleSystem getDamageParticleSystem;

    private void Awake()
    {
        HP = hp;
        alive = true;
        animationManager = GetComponent<AnimationManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public void GetDamage(float damage)
    {
        if (alive)
        {
            // disminuyo vida
            HP -= damage;
            // activo FX de recibir daño
            getDamageParticleSystem.Play();
            if (HP > 0)
            {
                // personaje solo sufre daño
                // activo animacion de recibir daño
                animationManager.GetDamageAnimation();
                // activo sonido de recibir daño
                if (audioSource.clip != getDamageSoundClip) audioSource.clip = getDamageSoundClip;
                audioSource.Play();
            }
            else
            {
                //perosnaje muere
                ////activo animacion de muerte
                animationManager.DeathAnimation();
                //// sonido de muerte
                audioSource.clip = deadSoundClip;
                audioSource.Play();
                alive = false;
            }
        }
        else
        {
            //esta muerto
        }
    }
}
