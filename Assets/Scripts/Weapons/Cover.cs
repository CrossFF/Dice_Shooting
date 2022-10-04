using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour, IDamageable
{
    [SerializeField]private float hp = 1;

    public void GetDamage(float damage)
    {
        hp -= damage;
        if(hp > 0)
        {
            // solo recibe daño
        }    
        else
        {
            // la coveretura es destruida
            Destroy(this.gameObject);
        }
    }
}
