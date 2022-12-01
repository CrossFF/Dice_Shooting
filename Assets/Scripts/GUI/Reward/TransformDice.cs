using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformDice : MonoBehaviour, IRewardPanel
{
    private CanvasGroup canvasGroup;
    private Equipment playerEquipment;
    [SerializeField]private Transform parentDices;
    [SerializeField]private GameObject prefabDiceButton;
    private List<GameObject> tempDices;
    [SerializeField] DiceProperty diceTransformIn;

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
        // pido una lista con todos los dados mejorables del jugador
        List<Dice> dices = playerEquipment.GetAllTypeDices(DiceProperty.Normal);
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
        GameObject.Find("Character").GetComponent<Equipment>().NewWabe();
    }

    public void Use(Dice d)
    {
        playerEquipment.TransformDice(d, diceTransformIn);
        Desactivate();
    }   
}

