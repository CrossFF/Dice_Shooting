using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceButon : MonoBehaviour, IPointerDownHandler
{
    private Dice dice;
    private int value = 0;
    private Equipment playerEquipment;
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    [SerializeField] private CanvasGroup canvasGroup;

    public void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        playerEquipment.Shoot(dice, value);
    }

    public void SetDice(Dice d, Equipment e)
    {
        dice = d;
        playerEquipment = e;
        // aplico visual dependiendo el uso del dado
        switch (dice.diceUse)
        {
            case DiceUse.Attack:
                image.color = Color.red;
                break;
            case DiceUse.Special1:
                image.color = Color.blue;
                break;
            case DiceUse.Special2:
                image.color = Color.green;
                break;
        }
        // muestro el valor final del dado
        value = Random.Range(dice.minValue, dice.maxValue);
        text.text = value.ToString();
    }
}
