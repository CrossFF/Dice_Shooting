using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarMovement : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField]private CanvasGroup panelBar;
    [SerializeField]private Image energyBarMovent;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();    
    }

    
    void Update()
    {
        if(playerController.GetStatus()==PlayerAction.Movement)
        {
            //muestro la barra de movimiento;
            if(panelBar.alpha != 1)
            {
                panelBar.alpha = 1;
                panelBar.interactable = true;
                panelBar.blocksRaycasts = true;
            }
            // actualizo la barra de energia
            energyBarMovent.fillAmount = playerController.restMoventEnergy;
        }
        else
        {
            //oculto la barra de movimiento
            if(panelBar.alpha != 0)
            {
                panelBar.alpha = 0;
                panelBar.interactable = false;
                panelBar.blocksRaycasts = false;
            }
        }
    }
}
