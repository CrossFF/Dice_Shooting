using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    private bool _turnPlayer; // turno del jugador
    [SerializeField] private Text textTurno;

    private void Start() 
    {
        ChangeTurn(true);    
    }

    //recibo la info de que turno es
    public bool GetTurn()
    {
        return _turnPlayer;
    }

    // Cambio el turno por el siguiente
    public void ChangeTurn()
    {
        // cambio el turno
        _turnPlayer = !_turnPlayer;
        if(_turnPlayer)
        {
            // si es el turno del jugador le informo que cambio el turno
            GameObject.Find("Player").GetComponent<PlayerController>().StartTurn();
        }
        // muestro el cambio
        vfxTurn();
    }

    // Cambio el turno a un turno en especifico
    public void ChangeTurn(bool turn)
    {
        _turnPlayer = turn;
        if(_turnPlayer)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().StartTurn();
        }
        vfxTurn();
    }

    // efectos de cambio de turno
    private void vfxTurn()
    {
        textTurno.text = _turnPlayer ? "Player turn":"Enemy turn";
    }
}
