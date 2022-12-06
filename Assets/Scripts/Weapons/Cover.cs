using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour, IDamageable, ITurret
{
    public Line Line { get; set; }
    public float HP { get; set; }
    public AudioSource audioSource;
    public AudioClip getDamageSound;
    public AudioClip destroySound;
    public Animator animator;
    [SerializeField] private ParticleSystem particleDespawn;
    private bool move = false;

    public void Install(int dice)
    {
        // tiene una vida igual al dado utilizado para crearla
        HP += dice;
    }

    public void Dismantle()
    {
        // la coveretura es destruida
        StartCoroutine(DestroyCover());
    }

    public void Despawn()
    {
        move = true;
        animator.SetTrigger("Despawn");
        particleDespawn.Play();
        Destroy(gameObject, 0.5f);
    }

    public void GetDamage(float damage)
    {
        HP -= damage;
        if (HP > 0)
        {
            // solo recibe daño
            animator.SetTrigger("Get Damage");
            if (audioSource.clip != getDamageSound) audioSource.clip = getDamageSound;
            audioSource.Play();
        }
        else
        {
            Dismantle();
        }
    }

    IEnumerator DestroyCover()
    {
        Line.ClearTurret();
        audioSource.clip = destroySound;
        audioSource.pitch = 0.5f;
        audioSource.Play();
        animator.SetTrigger("Destroy");
        yield return new WaitForSeconds(destroySound.length);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (move)
        {
            transform.position -= (Vector3.right * 10) * Time.deltaTime;
        }
    }
}
