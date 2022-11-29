using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsOptions : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField] private List<GameObject> prefabOptionsButtons;
    [SerializeField] private Transform parentOptions;
    [SerializeField] private Transform panelUpgrade;
    [SerializeField] private Transform panelTransform;
    [SerializeField] private Transform panelAdd;
    [SerializeField] private Transform panelDelete;
    private List<GameObject> tempButtons;

    private void Start()
    {
        tempButtons = new List<GameObject>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowOptions()
    {
        // seteo basico
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        // selecciono 2 opciones a mostrar
        GameObject op1, op2;
        op1 = prefabOptionsButtons[Random.Range(0, prefabOptionsButtons.Count)];
        op2 = prefabOptionsButtons[Random.Range(0, prefabOptionsButtons.Count)];
        // instancio los botones de las opciones
        RewardOptionsButton rOp1 = Instantiate(op1, parentOptions).GetComponent<RewardOptionsButton>();
        RewardOptionsButton rOp2 = Instantiate(op2, parentOptions).GetComponent<RewardOptionsButton>();
        tempButtons.Add(rOp1.gameObject);
        tempButtons.Add(rOp2.gameObject);
        // seteo la info del boton
        SettingButton(rOp1);
        SettingButton(rOp2);
    }

    private void SettingButton(RewardOptionsButton ob)
    {
        switch (ob.EnumRewardOptions)
        {
            case EnumRewardOptions.UpgradeDice:
                ob.SetInfo(this, panelUpgrade);
                break;
            case EnumRewardOptions.TransformDice:
                ob.SetInfo(this, panelTransform);
                break;
            case EnumRewardOptions.AddDice:
                ob.SetInfo(this, panelAdd);
                break;
            case EnumRewardOptions.DeleteDice:
                ob.SetInfo(this, panelDelete);
                break;
        }
    }

    public void HideOptions()
    {
        // oculto canvas
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        // borro botones
        for (int i = 0; i < tempButtons.Count; i++)
        {
            Destroy(tempButtons[i]);
        }
        tempButtons.Clear();
    }

    public void CancelReward()
    {
        // cancelo la recompenza e inicio una nueva oleada
        HideOptions();
        GameObject.Find("Character").GetComponent<Equipment>().NewWabe();
    }
}
