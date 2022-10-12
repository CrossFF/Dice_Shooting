using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardDiceButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Text text;
    [SerializeField] Image image;
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
                image.color = Color.red;
                break;
            case DiceUse.Special1:
                image.color = Color.blue;
                break;
            case DiceUse.Special2:
                image.color = Color.green;
                break;
        }
        string t = dice.MinValue + "~" + (dice.MaxValue - 1);
        text.text = t;
    }
}
