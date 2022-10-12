using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardOptionsButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private EnumRewardOptions enumRewardOptions;
    [SerializeField] private Text text;
    private RewardsOptions rewardsOptions;
    private Transform panel;

    // parametro
    public EnumRewardOptions EnumRewardOptions { get { return enumRewardOptions; } }

    public void SetInfo(RewardsOptions r, Transform c)
    {
        rewardsOptions = r;
        panel = c;
        string theText = "";
        switch (enumRewardOptions)
        {
            case EnumRewardOptions.UpgradeDice:
                theText = "Mejorar dado";
                break;
            case EnumRewardOptions.TransformDice:
                theText = "Transformar dado";
                break;
            case EnumRewardOptions.AddDice:
                theText = "Agregar dado";
                break;
            case EnumRewardOptions.DeleteDice:
                theText = "Eliminar dado";
                break;
        }
        text.text = theText;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //muestro el panel correspondiente
        panel.GetComponent<IRewardPanel>().Activate();
        //oculto el panel de opciones
        rewardsOptions.HideOptions();
    }
}

public enum EnumRewardOptions
{
    UpgradeDice,
    TransformDice,
    AddDice,
    DeleteDice
}

