using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Test
}

public class Exterminate : MonoBehaviour, IGameMode
{
    [Header("Referancias")]
    [SerializeField] private List<GameObject> prefabEnemys;
    [SerializeField] private LineManager lineManager;
    [SerializeField] private RewardsOptions rewardsOptions;
    [SerializeField] private Text textCountEnemys;
    [SerializeField] private Text textCountEnemysNotDead;
    [SerializeField] private SceneControl sceneControl;
    [SerializeField] private PanelDeCarteles panelDeCarteles;

    [Header("Extermine")]
    [SerializeField] private int limitWabe; // cantidad maxima de oleadas por dificultad
    [SerializeField] private int totalLevels; // cantidad de niveles por dificultad
    [SerializeField] private int enemysToSpawn = 1;
    [SerializeField] private int wabe = 1;
    [SerializeField] private float timeSpawn;
    private float cronometer = 0f;
    private int enemysPerWabe;
    private int totalEnemySpawn;
    private int enemysInWabe;
    private bool isWabeActive = false;
    private static bool status;

    // para control de win condition
    private int enemysDefeat = 0;

    // para mostrar estadisticas
    private int totalEnemyDefeat = 0;
    private int totalEnemysEscape = 0; // enemigos que lograron escaparse

    private Difficulty difficulty;

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
        totalEnemysEscape++;
        // pero es un enemigo menos para derrotar
    }

    public void Pause()
    {
        isWabeActive = false;
    }

    public void SetDifficulty(Difficulty dif)
    {
        difficulty = dif;
        switch (difficulty)
        {
            case Difficulty.Easy:
                enemysToSpawn = 3;
                timeSpawn = 2f;
                limitWabe = 5;
                totalLevels = 3;
                break;
            case Difficulty.Normal:
                enemysToSpawn = 5;
                timeSpawn = 1f;
                limitWabe = 5;
                totalLevels = 5;
                break;
            case Difficulty.Hard:
                enemysToSpawn = 20;
                timeSpawn = 0.2f;
                limitWabe = 8;
                totalLevels = 8;
                break;
            case Difficulty.Test:
                enemysToSpawn = 1;
                timeSpawn = 2f;
                limitWabe = 3;
                totalLevels = 3;
                break;
        }
        wabe = 1;
        Activate();
    }

    public void Win()
    {
        GameObject.Find("Character").GetComponent<Equipment>().SaveCharacter(totalEnemyDefeat, totalEnemysEscape);
        // vuelo al menu de misones y Equipo
        sceneControl.StartGame();
    }

    public void Lose()
    {
        sceneControl.GameOver();
    }

    private void Start()
    {
        GameInfo gameInfo = GameObject.Find("Game Info").GetComponent<GameInfo>();
        SetDifficulty(gameInfo.Difficulty);
    }

    void Update()
    {
        status = isWabeActive;
        if (isWabeActive)
        {
            // estadistica de tiempo
            textCountEnemysNotDead.text = totalEnemysEscape.ToString();
            textCountEnemys.text = totalEnemyDefeat.ToString();

            enemysPerWabe = enemysToSpawn * wabe;
            // si puedo spawnear un enemigo lo hago
            if (enemysPerWabe > totalEnemySpawn)
            {
                // el cronometro avanza
                cronometer += Time.deltaTime;
                if (cronometer >= timeSpawn)
                {
                    // cuando sea tiempo de spawner algo
                    SpawnEnemy();
                }
            }
            else
            {
                // verifico la win condition
                WinCondition();
            }
        }
    }

    private void SpawnEnemy()
    {
        if (difficulty == Difficulty.Easy)
        {
            lineManager.SpawnEnemy(prefabEnemys[0]);
        }
        else
        {
            int num = Random.Range(0, prefabEnemys.Count);
            lineManager.SpawnEnemy(prefabEnemys[num]);
        }
        totalEnemySpawn++;
        enemysInWabe++;
        cronometer = 0;
    }

    private void WinCondition()
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
                        enemysToSpawn++;
                        // inicio etapa de recompenza
                        panelDeCarteles.MostrarCartel("Enemigos Derrotados");
                        rewardsOptions.StartRewardState();
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

    IEnumerator ActiveWin()
    {
        panelDeCarteles.MostrarCartel("MISION COMPLETADA");
        yield return new WaitForSeconds(3f);
        Win();
    }

    public static bool GetStatus()
    {
        return status;
    }
}
