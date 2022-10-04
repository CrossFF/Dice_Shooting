using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private List<Line> linesList;
    [SerializeField] private Transform player;
    private int playerIndex; // posicion del jugador en la lista de lineas

    private void Awake() 
    {
        // seteo pocicion inicial del jugador 
        player = GameObject.Find("Character").transform;
        playerIndex = 1;
        linesList[playerIndex].SetPlayer(player);   
    }

    public Vector3 GetPlayerPosition()
    {
        for (int i = 0; i < linesList.Count; i++)
        {
            if(linesList[i].GetPlayer() != null) playerIndex = i;
        }
        return linesList[playerIndex].PlayerPosition;
    }

    public Vector3 MovePlayer(int yMove)
    {
        int nextPos = playerIndex + yMove;
        if(nextPos > -1 && nextPos < linesList.Count)
        {
            // es posible mover al personaje
            linesList[playerIndex].ClearPlayer();
            linesList[nextPos].SetPlayer(player);
        }
        return GetPlayerPosition(); // pos del personaje    
    }

    public bool IsTurretHere()
    {
        if(linesList[playerIndex].GetTurret() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void InstallTurret(GameObject pTurret)
    {
        GameObject temp = Instantiate(pTurret, linesList[playerIndex].TurretPosition, Quaternion.identity);
        linesList[playerIndex].SetTurret(temp.transform);
    }
}
