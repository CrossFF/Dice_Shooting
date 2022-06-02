using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    TurnController turnController;
    [SerializeField]private float timeTurn = 3f;
    private float cronometer;

    void Start()
    {
        turnController = GameObject.Find("TurnController").GetComponent<TurnController>();
        cronometer = timeTurn;
    }

    void Update()
    {
        if(!turnController.GetTurn())
        {
            // si es el turno del enemigo
            cronometer -= Time.deltaTime;
            if(cronometer <= 0f)
            {
                cronometer = timeTurn;
                turnController.ChangeTurn();
            }
        }
    }
}
