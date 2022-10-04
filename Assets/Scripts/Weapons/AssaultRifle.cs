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
    private LineManager lineManager;

    private void Start()
    {
        lineManager = GameObject.Find("Line Manager").GetComponent<LineManager>();
        playerAnimationManager = transform.parent.GetComponent<AnimationManager>();
        lineProyectile.enabled = false;
    }

    public void Shoot(int dice)
    {
        // dispara el doble de lo que tenga el dado cantidad de veces
        int totalProyectiles = dice * 2 * damageMultiplier;
        StartCoroutine(InstantiateProjectiles(totalProyectiles));
    }

    public void Special1(int dice)
    {
        // es posible crear una torrera en esta linea?
        if(!lineManager.IsTurretHere())
        {
            // animacion de instalar torreta
            playerAnimationManager.InstallTurret();
            // crea una cobertura en la linea correspondiente
            lineManager.InstallTurret(prefabCover);
        }  
    }

    public void Special2(int dice)
    {
        // los ataque disparan el doble de proyectiles
        damageMultiplier++;
    }

    public void ClearEffects()
    {
        damageMultiplier = 1;
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
