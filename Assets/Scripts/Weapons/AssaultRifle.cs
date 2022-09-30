using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IWeapon
{
    public GameObject prefabProjectile;
    public Transform weaponOrigin;
    public float fireRate;

    public void Shoot(int dice)
    {
        StartCoroutine(InstantiateProjectiles(dice));
    }

    IEnumerator InstantiateProjectiles(int dice)
    {
        for (int i = 0; i < dice; i++)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(prefabProjectile, weaponOrigin.position, Quaternion.identity);
        }
    }
}
