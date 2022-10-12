using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDice : MonoBehaviour , IRewardPanel
{
    private CanvasGroup canvasGroup;
    private Equipment playerEquipment;
    [SerializeField]private Transform parentDices;
    [SerializeField]private GameObject prefabDiceButton;
    private List<GameObject> tempDices;

    void Start()
    {
        playerEquipment = GameObject.Find("Character").GetComponent<Equipment>();
        tempDices = new List<GameObject>();
        // seteo del panel
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;    
    }

    public void Activate()
    {
        // pido una lista con todos los dados del jugador
        List<Dice> dices = playerEquipment.GetAllDices();
        // instancio esos dados
        for (int i = 0; i < dices.Count; i++)
        {
            tempDices.Add(Instantiate(prefabDiceButton, parentDices));
            RewardDiceButton button = tempDices[i].GetComponent<RewardDiceButton>();
            button.SetDice(dices[i], this);
        }
        // muestro el panel
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Desactivate()
    {
        // oculto el panel
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        // borro los dados instanciados
        for (int i = 0; i < tempDices.Count; i++)
        {
            Destroy(tempDices[i]);
        }
        tempDices.Clear();
    }

    public void Use(Dice d)
    {
        // elimino el dado seleccionado
        playerEquipment.DeleteDice(d);
        // desactivo el panel
        Desactivate();
    }

    
}
