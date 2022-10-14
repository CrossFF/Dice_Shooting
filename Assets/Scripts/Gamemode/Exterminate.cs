using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exterminate : MonoBehaviour, IGameMode
{
    [Header("Referancias")]
    [SerializeField] private List<GameObject> prefabEnemys;
    [SerializeField] private LineManager lineManager;
    [SerializeField] private RewardsOptions rewardsOptions;

    [Header("Extermine")]
    [SerializeField] private int level = 1;
    [SerializeField] private int wabe = 1;
    [SerializeField] private float timeSpawn;
    private float cronometer = 0f;
    private int enemysPerWabe;
    private int totalEnemySpawn;
    private int enemysInWabe;
    private bool isWabeActive = false;

    // para control de win condition
    private int enemysDefeat = 0;

    public void Activate()
    {
        isWabeActive = true;
    }

    public void DeathEnemy()
    {
        enemysInWabe--;
        enemysDefeat++;
    }

    public void DespawnEnemy()
    {
        enemysInWabe--;
        // pero es un enemigo menos para derrotar
    }

    public void Lose()
    {
        print("Perdiste pa");
    }

    public void Pause()
    {
        isWabeActive = false;
    }

    public void Win()
    {
        Pause();
        wabe = 1;
        level++;
        // inicio etapa de recompenza
        rewardsOptions.ShowOptions();
        //reseteo variables  
        totalEnemySpawn = 0;
        enemysInWabe = 0;
        cronometer = 0f;
        if (timeSpawn > 0.2f)
        {
            timeSpawn -= 0.2f;
        }
        else
        {
            timeSpawn = 0.2f;
        }
    }

    void Update()
    {
        if (isWabeActive)
        {
            enemysPerWabe = level * wabe;
            // si puedo spawnear un enemigo lo hago
            if (enemysPerWabe > totalEnemySpawn)
            {
                // el cronometro avanza
                cronometer += Time.deltaTime;
                if (cronometer >= timeSpawn)
                {
                    // cuando sea tiempo de spawner algo
                    //spawn de enemigo
                    lineManager.SpawnEnemy(prefabEnemys[0]);
                    totalEnemySpawn++;
                    enemysInWabe++;
                    cronometer = 0;
                }
            }
            else
            {
                // verifico si estan muertos todos los enemigos
                if (enemysInWabe <= 0)
                {
                    // aumento la oleada o el nivel
                    if (wabe < 3)
                    {
                        wabe++;
                    }
                    else
                    {
                        if (enemysDefeat > totalEnemySpawn / 2)
                        {
                            Win();
                        }
                        else
                        {
                            Lose();
                        }
                    }
                }
            }
        }
    }
}
