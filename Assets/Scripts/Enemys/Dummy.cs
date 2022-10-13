using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable, IEnemy
{
    public float HP { get; set; }
    public Line Line { get; set; }

    public void Despawn()
    {
        throw new System.NotImplementedException();
    }

    public void GetDamage(float damage)
    {
        // activa animacion de recibir daño
    }

    private void OnMouseDown()
    {
        // realizaz un ataque normal
        if (Line.GetTurret() != null)
        {
            Line.GetTurret().GetComponent<IDamageable>().GetDamage(1f);
        }
        else if (Line.GetPlayer() != null)
        {
            Line.GetPlayer().GetComponent<IDamageable>().GetDamage(1f);
        }
    }
}
