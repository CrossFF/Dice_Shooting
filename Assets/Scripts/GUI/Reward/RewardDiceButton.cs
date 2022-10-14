using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardDiceButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Text text;
    [SerializeField] Image image;
    [SerializeField] List<Sprite> spritesDices;
    private Dice dice;
    private IRewardPanel panel;

    public void OnPointerDown(PointerEventData eventData)
    {
        panel.Use(dice);
    }

    public void SetDice(Dice d, IRewardPanel p)
    {
        dice = d;
        panel = p;
        // defino visual del dado
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
        string t = dice.MinValue + "~" + (dice.MaxValue - 1);
        text.text = t;
    }
}
