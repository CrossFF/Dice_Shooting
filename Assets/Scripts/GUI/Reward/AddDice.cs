using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDice : MonoBehaviour, IRewardPanel
{
    [SerializeField] private Transform parentDice;
    [SerializeField] private GameObject prefabDiceButton;
    [SerializeField] private CanvasGroup canvasGroup;
    private List<GameObject> tempDices;

    private void Start()
    {
        tempDices = new List<GameObject>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Activate()
    {
        // genero 3 dados random
        List<Dice> dices = DicePool.GetNewDices(3);
        // muestro esos tres dados
        for (int i = 0; i < dices.Count; i++)
        {
            tempDices.Add(Instantiate(prefabDiceButton, parentDice));
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
        GameObject.Find("Character").GetComponent<Equipment>().AddDice(d);
        Desactivate();
    }
}
