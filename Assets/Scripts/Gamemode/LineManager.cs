using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private List<Line> linesList;
    [SerializeField] private Transform player;
    private int playerIndex; // posicion del jugador en la lista de lineas
    private IGameMode gameMode; // referencia al modo de juego

    private void Awake()
    {
        // seteo pocicion inicial del jugador 
        player = GameObject.Find("Character").transform;
        playerIndex = 1;
        linesList[playerIndex].SetPlayer(player);
        gameMode = GameObject.Find("Game Mode").GetComponent<IGameMode>();
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

    public int GetLayer()
    {
        return linesList[playerIndex].LayerForSprite;
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

    public void SpawnEnemy(GameObject pE)
    {
        int index = Random.Range(0, linesList.Count);
        linesList[index].SpawnEnemy(pE);
    }

    public Transform GetEnemy()
    {
        return linesList[playerIndex].GetEnemy();
    }

    public List<Transform> GetEnemys(int amount)
    {
        return linesList[playerIndex].GetEnemys(amount);
    }

    public List<Transform> GetAllEnemysInLine()
    {
        return linesList[playerIndex].GetAllEnemys();
    }

    public void RemoveEnemy()
    {
        gameMode.DeathEnemy();
    }

    // activa la siguiente oleada desde el inventario
    public void ActivateGameMode()
    {
        gameMode.Activate();
    }

    public void DespawnEnemy()
    {
        gameMode.DespawnEnemy();
    }

    public void DespawnTurrets()
    {
        foreach (var item in linesList)
        {
            item.DespawnTurret();
        }
    }
}
