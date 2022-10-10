using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private List<Line> linesList;
    [SerializeField] private Transform player;
    private int playerIndex; // posicion del jugador en la lista de lineas

    [Header("Enemies")]
    public GameObject prefabDummy;
    public List<GameObject> prefabEnemys;

    [Header("Extermine")]
    [SerializeField]private int level = 1;
    [SerializeField]private int wabe = 1;
    [SerializeField] private float timeSpawn;
    private float cronometer = 0f;
    [SerializeField]private int enemysPerWabe;
    [SerializeField]private int totalEnemySpawn;
    [SerializeField]private int enemysInWabe;
    [SerializeField]private bool isWabeActive = false;

    private void Awake()
    {
        // seteo pocicion inicial del jugador 
        player = GameObject.Find("Character").transform;
        playerIndex = 1;
        linesList[playerIndex].SetPlayer(player);
    }

    private void Start()
    {
        /*// instancio 3 dummys, uno en cada linea
        for (int i = 0; i < 3; i++)
        {
            linesList[i].SpawnEnemy(prefabDummy);
        }*/

        isWabeActive = true;
    }

    private void Update()
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
                    int index = Random.Range(0, linesList.Count);
                    linesList[index].SpawnEnemy(prefabEnemys[0]);
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
                        wabe = 1;
                        level++;
                    }

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
            }
        }
    }

    public Vector3 GetPlayerPosition()
    {
        for (int i = 0; i < linesList.Count; i++)
        {
            if (linesList[i].GetPlayer() != null) playerIndex = i;
        }
        return linesList[playerIndex].PlayerPosition;
    }

    public Vector3 MovePlayer(int yMove)
    {
        int nextPos = playerIndex + yMove;
        if (nextPos > -1 && nextPos < linesList.Count)
        {
            // es posible mover al personaje
            linesList[playerIndex].ClearPlayer();
            linesList[nextPos].SetPlayer(player);
        }
        return GetPlayerPosition(); // pos del personaje    
    }

    public bool IsTurretHere()
    {
        if (linesList[playerIndex].GetTurret() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 GetTurretPosition()
    {
        return linesList[playerIndex].TurretPosition;
    }

    public void SetTurret(Transform t)
    {
        linesList[playerIndex].SetTurret(t);
    }

    public Transform GetTurret()
    {
        return linesList[playerIndex].GetTurret();
    }

    public Transform GetEnemy()
    {
        return linesList[playerIndex].GetEnemy();
    }
    public void RemoveEnemy()
    {
        enemysInWabe--;
    }
}
