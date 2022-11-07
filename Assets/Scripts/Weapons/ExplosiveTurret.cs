using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExplosiveTurret : MonoBehaviour, IDamageable, ITurret
{
    public Line Line { get; set; }
    public float HP { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    //stats
    private int explosiveCapacity = 0;
    private float levelChargue = 0f; // nivel de carga de la torreta

    [Header("References")]
    [SerializeField] private ParticleSystem particleExplosionCharge;
    [SerializeField] private ParticleSystem particleExplosion;
    [SerializeField] private Image chargueLevelImage;
    [SerializeField] private TMP_Text cgargueLevelText;

    public void Dismantle()
    {
        // efectos
        particleExplosion.Play();
        // hago daño a todos los enemigos en la linea, según la capacidad explosiva
        List<Transform> tEnemys = Line.GetAllEnemys();
        List<IDamageable> enemys = new List<IDamageable>();
        foreach (var item in tEnemys)
        {
            enemys.Add(item.GetComponent<IDamageable>());
        }
        foreach (var item in enemys)
        {
            item.GetDamage(levelChargue);
        }
        // la torreta es destruida
        Destroy(gameObject, 0.5f);
    }

    public void Install(int dice)
    {
        explosiveCapacity += dice;
    }

    public void GetDamage(float damage)
    {
        // la mina explota
        Dismantle();
    }

    private void Update()
    {
        // aumento la cantidad de carga de la torreta, 
        //  con respecto a la capacidad explosiva de la misma
        // El daño de la torreta se hace en base a la carga que tenga
        //  siendo la capacidad explosiva el daño maximo posible.
        if(levelChargue < explosiveCapacity)
        {
            levelChargue += Time.deltaTime;
        }
        else
        {
            levelChargue = explosiveCapacity;
        }  
        chargueLevelImage.fillAmount = levelChargue / explosiveCapacity;
        cgargueLevelText.text = Mathf.Floor(levelChargue).ToString();
    }
}
