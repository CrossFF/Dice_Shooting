using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]private Animator animator;

    public void MoveAnimation()
    {
        animator.SetTrigger("Move");
    }

    public void ShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    public void RepetitiveShootAnimation()
    {
        animator.SetTrigger("Repetitive Shoot");
    }
    public void StopRepetitiveShootAnimation()
    {
        animator.SetTrigger("Stop Repetitive Shoot");
    }

    public void GetDamageAnimation()
    {
        animator.SetTrigger("Damage");
    }

    public void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }

    public void InstallTurret()
    {
        animator.SetTrigger("Install Turret");
    }

    public void Walk(bool state)
    {
        animator.SetBool("Walk",state);
    }
}
