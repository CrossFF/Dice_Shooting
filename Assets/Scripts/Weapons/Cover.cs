using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour, IDamageable, ITurret
{  
    public float HP { get; set; }

    public void Install(int dice)
    {
        // tiene una vida igual al dado utilizado para crearla
        HP = dice;
    }

    public void Dismantle()
    {
        // la coveretura es destruida
        Destroy(this.gameObject);
    }

    public void GetDamage(float damage)
    {
        HP -= damage;
        if(HP > 0)
        {
            // solo recibe daño
        }    
        else
        {
            Dismantle();
        }
    }
}
