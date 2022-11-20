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

    [Header("References")]
    [SerializeField] private Image imageBottom;
    [SerializeField] private Image imageSpriteCard;
    [SerializeField] private Image imageTipeDice;
    [SerializeField] private Image imageNameCard;
    [SerializeField] private Text textValueDice;
    [SerializeField] private Text textNameCard;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Seteo")]
    [SerializeField] List<Sprite> spritesDices;
    [SerializeField] private List<Color> bottomColors;
    [SerializeField] private List<Color> nameColors;
    [SerializeField] private List<Color> valueColors;

    public void OnPointerDown(PointerEventData eventData)
    {
        // solo posible si el personaje esta vivo
        if (PlayerHP.IsPlayerAlive())
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
            playerEquipment.Shoot(dice, value);
        }
    }

    public void SetDice(Dice d, Equipment e)
    {
        dice = d;
        playerEquipment = e;
        value = dice.RollDice();
        ApplyVisuals();
    }

    void ApplyVisuals()
    {
        //datos
        textValueDice.text = value.ToString();
        textNameCard.text = dice.DiceUse.ToString();
        // color de fondo y nombre dependiendo uso de dado
        switch (dice.DiceUse)
        {
            case DiceUse.Attack:
                imageBottom.color = bottomColors[0];
                imageNameCard.color = nameColors[0];
                imageSpriteCard.sprite = spritesDices[0];
                break;
            case DiceUse.Special1:
                imageBottom.color = bottomColors[1];
                imageNameCard.color = nameColors[1];
                imageSpriteCard.sprite = spritesDices[1];
                break;
            case DiceUse.Special2:
                imageBottom.color = bottomColors[2];
                imageNameCard.color = nameColors[2];
                imageSpriteCard.sprite = spritesDices[2];
                break;
        }
        // color de valor dependiendo propiedad de dado
        switch (dice.DiceProperty)
        {
            case DiceProperty.Normal:
                imageTipeDice.color = valueColors[0];
                break;
            case DiceProperty.Advanced:
                imageTipeDice.color = valueColors[1];
                break;
            case DiceProperty.Quick:
                imageTipeDice.color = valueColors[2];
                break;
        }
    }
}
