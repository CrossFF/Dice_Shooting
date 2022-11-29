using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exterminate : MonoBehaviour, IGameMode
{
    [Header("Referancias")]
    [SerializeField] private List<GameObject> prefabEnemys;
    [SerializeField] private LineManager lineManager;
    [SerializeField] private RewardsOptions rewardsOptions;
    [SerializeField] private Text textCountEnemys;
    [SerializeField] private Text textTimePlayed;

    [Header("Extermine")]
    [SerializeField] private Difficulty theDifficulty;
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

    // para mostrar estadisticas
    private int totalEnemyDefeat = 0;
    private float timePlayed = 0f;

    public void Activate()
    {
        isWabeActive = true;
    }

    public void DeathEnemy()
    {
        enemysInWabe--;
        enemysDefeat++;
        totalEnemyDefeat++;
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

    public void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                level = 1;
                timeSpawn = 2f;
                break;
            case Difficulty.Normal:
                level = 5;
                timeSpawn = 1f;
                break;
            case Difficulty.Hard:
                level = 20;
                timeSpawn = 0.2f;
                break;
        }
        wabe = 1;
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
        if (timeSpawn > 0.2f) timeSpawn = Mathf.Clamp(timeSpawn - 0.2f, 0.2f, 5);
    }

    private void Start()
    {
        SetDifficulty(theDifficulty);
    }

    void Update()
    {
        if (isWabeActive)
        {
            // estadistica de tiempo
            timePlayed += Time.deltaTime;
            textTimePlayed.text = timePlayed.ToString();
            textCountEnemys.text = totalEnemyDefeat.ToString();

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

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}
