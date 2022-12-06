using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RewardsOptions rewardsOptions;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem particlesSpawn;
    private BoxCollider2D boxCollider2D;

    private void Awake() 
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
    }

    public void Spawn()
    {
        boxCollider2D.enabled = true;
        animator.SetTrigger("Spawn");
        particlesSpawn.Play();
    }

    public void Despawn()
    {
        animator.SetTrigger("Despawn");
    }

    IEnumerator Open()
    {
        boxCollider2D.enabled = false;
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(0.25f);
        rewardsOptions.ShowOptions();
        Despawn();
    }

    private void OnMouseDown()
    {
        StartCoroutine(Open());
    }

}
