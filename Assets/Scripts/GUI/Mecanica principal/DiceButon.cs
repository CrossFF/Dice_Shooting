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
    [SerializeField] List<Sprite> spritesDices;

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
        switch (dice.DiceUse)
        {
            case DiceUse.Attack:
                if (dice.DiceProperty != DiceProperty.Quick)
                {
                    image.sprite = spritesDices[0];
                }
                else
                {
                    image.sprite = spritesDices[1];
                }
                break;
            case DiceUse.Special1:
                if (dice.DiceProperty != DiceProperty.Quick)
                {
                    image.sprite = spritesDices[2];
                }
                else
                {
                    image.sprite = spritesDices[3];
                }
                break;
            case DiceUse.Special2:
                if (dice.DiceProperty != DiceProperty.Quick)
                {
                    image.sprite = spritesDices[4];
                }
                else
                {
                    image.sprite = spritesDices[5];
                }
                break;
        }
        // muestro el valor final del dado
        value = dice.RollDice();
        text.text = value.ToString();
    }
}
