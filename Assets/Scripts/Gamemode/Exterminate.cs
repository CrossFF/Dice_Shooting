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
    [SerializeField] private SceneControl sceneControl;
    [SerializeField] private PanelDeCarteles panelDeCarteles;

    [Header("Extermine")]
    [SerializeField] private Difficulty theDifficulty;
    [SerializeField] private int limitWabe; // cantidad maxima de oleadas por dificultad
    [SerializeField] private int totalLevels; // cantidad de niveles por dificultad
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
        panelDeCarteles.MostrarCartel("Nuevo nivel");
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

    public void Pause()
    {
        isWabeActive = false;
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                level = 3;
                timeSpawn = 2f;
                limitWabe = 5;
                totalLevels = 3;
                break;
            case Difficulty.Normal:
                level = 5;
                timeSpawn = 1f;
                limitWabe = 5;
                totalLevels = 5;
                break;
            case Difficulty.Hard:
                level = 20;
                timeSpawn = 0.2f;
                limitWabe = 8;
                totalLevels = 8;
                break;
        }
        wabe = 1;
    }

    public void Win()
    {
        sceneControl.YouWin();
    }

    public void Lose()
    {
        sceneControl.GameOver();
    }

    private void Awake()
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
                    if (wabe < limitWabe)
                    {
                        wabe++;
                        panelDeCarteles.MostrarCartel("Nueva oleada");
                    }
                    else
                    {
                        if (enemysDefeat > totalEnemySpawn / 2)
                        {
                            Pause();
                            totalLevels--;
                            if (totalLevels > 0)
                            {
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
                            else
                            {
                                StartCoroutine(ActiveWin());
                            }
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

    IEnumerator ActiveWin()
    {
        panelDeCarteles.MostrarCartel("ENEMIGOS ELIMINADOS");
        yield return new WaitForSeconds(3f);
        Win();
    }
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}
