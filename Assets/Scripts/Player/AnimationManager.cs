using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;

    public void MoveAnimation()
    {
        animator.SetTrigger("Move");
    }

    public void ShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    public void GetDamageAnimation()
    {
        animator.SetTrigger("Damage");
    }

    public void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }
}
