using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    TurnController turnController;
    Character characterStats;
    CharacterController characterController;

    //Acciones
    private PlayerAction _playerActions = PlayerAction.Wait; // acciones disponibles para el personaje
    [SerializeField]private CanvasGroup panelAcciones;// menu de seleccion de acciones del personaje
    [SerializeField]private Text textActiones; // para mostrar la accion que esta haciendo el personaje

    // Movimiento
    float maxTimeMovent;
    float actualTimeMovent;
    public float restMoventEnergy;// cantidad de movimiento restante en porcentaje 0:1;
    
    private void Start() {
        turnController = GameObject.Find("TurnController").GetComponent<TurnController>();
        characterStats = GetComponent<Character>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        // verifico de quien es el turno
        if(turnController.GetTurn())
        {
            // si es el turno del jugador
            PlayerActions();
        }
        else
        {
            // si no es el turno del jugador
            PlayerReactions();
        }
    }

    private void PlayerActions()
    {
        //Muestro que accion se encuentra haciendo el personaje en la UI
        textActiones.text = "Accion: " + _playerActions.ToString();
        
        //Activar menu de acciones
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeVisionMenu();
        }

        // Cosas a hacer segun acciones selecionadas por el jugador
        switch(_playerActions)
        {
            case PlayerAction.Movement:
                // movimiento
                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");
                Vector3 movemnt = new Vector3(x,y,0f).normalized * Time.deltaTime * 10f;
                // si el personaje tiene suficiente energia para moverse
                if(movemnt != Vector3.zero && actualTimeMovent > 0f)
                {
                    //muevo al personaje
                    characterController.Move(movemnt);
                    // bajo la cantidad de movimiento que queda
                    actualTimeMovent -= Time.deltaTime;
                    // hago porcentaje de energia restante
                    restMoventEnergy = actualTimeMovent / maxTimeMovent;
                }      
            break;
        }
    }

    private void PlayerReactions()
    {

    }

    private void ChangeVisionMenu()
    {
        //Cambia la visibilidad del menu de acciones del jugador
        panelAcciones.alpha = panelAcciones.alpha == 1 ? 0:1;
        panelAcciones.interactable =  panelAcciones.interactable ? false:true;
    }

    public void SetStatus(PlayerAction action)
    {
        _playerActions = action;
    }

    public PlayerAction GetStatus()
    {
        return _playerActions;
    }

    public void SelectMovent()
    {
        SetStatus(PlayerAction.Movement);
        ChangeVisionMenu();
    }

    public void SelectAtack()
    {
        SetStatus(PlayerAction.Atack);
        ChangeVisionMenu();
    }

    public void StartTurn()
    {
        // seteo energia de movimiento
        maxTimeMovent = characterStats.moveDistance;
        actualTimeMovent = maxTimeMovent;
        restMoventEnergy = actualTimeMovent / maxTimeMovent;
    }

    public void EndTurn()
    {
        SetStatus(PlayerAction.Wait);
        turnController.ChangeTurn(false);
        ChangeVisionMenu();
    }
}

public enum PlayerAction
{
    Movement,
    Atack,
    Wait
}
