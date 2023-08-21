using UnityEngine;
using System;

[Serializable]
public class Weapon : MonoBehaviour
{
    // stats del arma
    private float damage;
    private Equipment equipmentReference;

    // propiedades
    public float Damage { get { return damage; } }
    public Equipment EquipmentReference { get { return equipmentReference; } }

    //constructores

    // funciones
    public void SetDamage(float d)
    {
        damage = d;
    }
    public void SetEquipment(Equipment e)
    {
        equipmentReference = e;
    }
}
