using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IWeapon
{
    public LineRenderer lineProyectile; // line renderer que hace como disparo
    public Transform weaponOrigin;
    public float fireRate;
    private AnimationManager playerAnimationManager;

    private void Start()
    {
        playerAnimationManager = transform.parent.GetComponent<AnimationManager>();
        lineProyectile.SetPosition(0, weaponOrigin.position);
        lineProyectile.SetPosition(1, new Vector3(weaponOrigin.position.x + 10,
                                                  weaponOrigin.position.y,
                                                  weaponOrigin.position.z));
        lineProyectile.enabled = false;
    }

    public void Shoot(int dice)
    {
        StartCoroutine(InstantiateProjectiles(dice));
    }

    IEnumerator InstantiateProjectiles(int dice)
    {    
        playerAnimationManager.RepetitiveShootAnimation();
        for (int i = 0; i < dice; i++)
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
