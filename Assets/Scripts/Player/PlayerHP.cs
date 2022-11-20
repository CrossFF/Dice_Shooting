using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamageable
{
    [SerializeField] private float hp; // para poder ponerle la vida que considere necesaria
    public float HP { get; set; } // interfaz
    private bool alive;
    private static bool Alive;

    // animacion
    private AnimationManager animationManager;

    // sonido
    private AudioSource audioSource;
    public AudioClip getDamageSoundClip;
    public AudioClip deadSoundClip;

    // efectos
    public ParticleSystem getDamageParticleSystem;
    private CameraController cameraController;

    private void Awake()
    {
        HP = hp;
        alive = true;
        Alive = alive;
        animationManager = GetComponent<AnimationManager>();
        audioSource = GetComponent<AudioSource>();
        cameraController = GameObject.Find("Camera Controller").GetComponent<CameraController>();
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
                StartCoroutine(Shake());
                // activo sonido de recibir daño
                if (audioSource.clip != getDamageSoundClip) audioSource.clip = getDamageSoundClip;
                audioSource.Play();
            }
            else
            {
                //perosnaje muere
                StartCoroutine(Death());
            }
        }
    }

    IEnumerator Shake()
    {
        cameraController.Shake(1);
        yield return new WaitForSeconds(0.25f);
        cameraController.Shake(0);
    }

    IEnumerator Death()
    {
        ////activo animacion de muerte
        animationManager.DeathAnimation();
        //// sonido de muerte
        audioSource.clip = deadSoundClip;
        audioSource.Play();
        alive = false;
        Alive = alive;
        GameObject.Find("Game Mode").GetComponent<IGameMode>().Pause();
        yield return new WaitForSeconds(2);
        GameObject.Find("SceneController").GetComponent<SceneControl>().MainMenu();
    }

    public static bool IsPlayerAlive()
    {
        return Alive;
    }
}
