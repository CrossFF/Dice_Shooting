using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IWeapon
{
    public LineRenderer lineProyectile; // line renderer que hace como disparo
    public Transform weaponOrigin;
    public float fireRate;
    private AnimationManager playerAnimationManager;
    private int damageMultiplier = 1; // multiplicador de daño de special 2
    public GameObject prefabCover; // special 1

    private void Start()
    {
        playerAnimationManager = transform.parent.GetComponent<AnimationManager>();
        lineProyectile.enabled = false;

        //print("Multiplicador de daño :" + damageMultiplier);
    }

    public void Shoot(Dice dice)
    {
        // dispara el doble de lo que tenga el dado cantidad de veces
        int totalProyectiles = Random.Range(dice.minValue, dice.maxValue);
        totalProyectiles = totalProyectiles * 2 * damageMultiplier;
        //print("Total de proyectiles :" + totalProyectiles);
        StartCoroutine(InstantiateProjectiles(totalProyectiles));
    }

    public void Special1(Dice dice)
    {
        // crea una cobertura
        Vector3 coverSpawn = new Vector3(transform.position.x + 2, 
                              transform.position.y, 
                              transform.position.z);
        Instantiate(prefabCover, coverSpawn, Quaternion.identity);
    }

    public void Special2(Dice dice)
    {
        // los ataque disparan el doble de proyectiles
        damageMultiplier++;

        //print("Multiplicador de daño :" + damageMultiplier);
    }

    public void ClearEffects()
    {
        damageMultiplier = 1;

        //print("Multiplicador de daño :" + damageMultiplier);
    }

    IEnumerator InstantiateProjectiles(int proyectiles)
    {    
        playerAnimationManager.RepetitiveShootAnimation();
        for (int i = 0; i < proyectiles; i++)
        {         
            // seteo origen del disparo
            lineProyectile.SetPosition(0, weaponOrigin.position);
            // seteo un final de la linea simulando falta de precicions
            lineProyectile.SetPosition(1, new Vector3(weaponOrigin.position.x + 10,
                                                      weaponOrigin.position.y + (Random.Range(-0.5f, 0.5f)),
                                                      weaponOrigin.position.z));
            // activo la linea del disparo
            lineProyectile.enabled = true;

            yield return new WaitForSeconds(fireRate);

            // oculto la linea del disparo
            lineProyectile.enabled = false;
        }
        playerAnimationManager.StopRepetitiveShootAnimation();
    } 
}
