using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamageable
{
    public float playerHP;// vida incial del jugador
    private float currentHP; // vida actual del jugador
    private bool alive;

    // animacion
    private AnimationManager animationManager;

    // sonido
    private AudioSource audioSource;
    public AudioClip getDamageSoundClip;
    public AudioClip deadSoundClip;

    // efectos
    public ParticleSystem getDamageParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = playerHP;
        alive = true;
        animationManager = GetComponent<AnimationManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // si la vida es menor o igual a 0
        if(currentHP <= 0 && alive)
        {
            ////activo animacion de muerte
            animationManager.DeathAnimation();
            //// sonido de muerte
            audioSource.clip = deadSoundClip;
            audioSource.Play();
            alive = false;
        }
                
    }

    public void GetDamage(float damage)
    {
        // activo animacion de recibir daño
        animationManager.GetDamageAnimation();
        // disminuyo vida
        currentHP -= damage;
        // activo sonido de recibir daño
        if(audioSource.clip != getDamageSoundClip) audioSource.clip = getDamageSoundClip;
        audioSource.Play();
        // activo FX de recibir daño
        getDamageParticleSystem.Play();
    }
}
