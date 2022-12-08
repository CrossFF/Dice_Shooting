using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class GameLobby : MonoBehaviour
{
    #region Variables y Propiedades
    [Header("General References")]
    [SerializeField] private Animator animEquipMenu;
    [SerializeField] private Animator animMisionsMenu;
    [SerializeField] private CinemachineVirtualCamera vcEquip;
    [SerializeField] private CinemachineVirtualCamera vcMisions;
    [SerializeField] private Transform character;
    private GameInfo gameInfo;

    [Header("Equipment References")]
    [SerializeField] private Text textPrimaryWeapon;
    [SerializeField] private Text textSecondaryWeapon;
    [SerializeField] private Image fillHPImage;
    [SerializeField] private Text textHP;
    [SerializeField] private List<GameObject> prefabsPrimaryWeapon;
    [SerializeField] private List<GameObject> prefabsSecondaryWeapon;
    [SerializeField] private CanvasGroup canvasPWeapon;
    [SerializeField] private CanvasGroup canvasSWeapon;
    [SerializeField] private Animator animSelectionWeapon;
    [SerializeField] private Transform cardsContainer;
    [SerializeField] private GameObject prefabCard;
    [SerializeField] private Animator animCardsMenu;
    private List<GameObject> tCards;

    #endregion
    void Start()
    {
        // obtengo referencia al Game Info
        gameInfo = GameObject.Find("Game Info").GetComponent<GameInfo>();
        // seteo el menu a mostrar primero
        vcMisions.Priority = 10;
        vcEquip.Priority = 0;
        animMisionsMenu.SetTrigger("Show Menu");
        character.localScale = new Vector3(-3.5f, 3.5f, 3.5f);

        // seteo la info del menu de equipo
        if (gameInfo.Character == null)
        {
            // es una nueva partida
            // creo un personaje nuevo
            Character newCharacter = new Character(new DicePool(), prefabsPrimaryWeapon[0], prefabsSecondaryWeapon[0], 5);
            gameInfo.SaveCharacter(newCharacter);
        }
        SetInfoEquipMenu();
    }

    public void ShowEquipMenu()
    {
        vcEquip.Priority = 10;
        vcMisions.Priority = 0;
        animEquipMenu.SetTrigger("Show Menu");
        animMisionsMenu.SetTrigger("Hide Menu");
        character.localScale = new Vector3(3.5f, 3.5f, 3.5f);
    }

    public void ShowMisionsMenu()
    {
        vcMisions.Priority = 10;
        vcEquip.Priority = 0;
        animMisionsMenu.SetTrigger("Show Menu");
        animEquipMenu.SetTrigger("Hide Menu");
        character.localScale = new Vector3(-3.5f, 3.5f, 3.5f);
    }

    #region Equip Functions
    void SetInfoEquipMenu()
    {
        // vida
        textHP.text = gameInfo.Character.ActualHP.ToString() + "/" + 5;
        fillHPImage.fillAmount = gameInfo.Character.ActualHP / 5;
        // armas
        foreach (var item in prefabsPrimaryWeapon)
        {
            if (gameInfo.Character.PrimaryWeapon == item)
            {
                textPrimaryWeapon.text = item.name;
                break;
            }
        }
        foreach (var item in prefabsSecondaryWeapon)
        {
            if (gameInfo.Character.SecondaryWeapon == item)
            {
                textSecondaryWeapon.text = item.name;
                break;
            }
        }
        // cartas
        if(tCards == null) tCards = new List<GameObject>();
        // limpieza de lista
        foreach (var item in tCards.ToArray())
        {
            Destroy(item);
        }
        tCards.Clear();
        // instanciado de cartas
        foreach (var item in gameInfo.Character.Dices.GetAllDices())
        {
            var tCard = Instantiate(prefabCard, cardsContainer);
            tCards.Add(tCard);
            RewardDiceButton card = tCard.GetComponent<RewardDiceButton>();
            card.SetDice(item);
        }
    }

    public void ShowMenuPrimaryWeapon()
    {
        HideCards();
        animSelectionWeapon.SetBool("Menu Active", true);
        canvasPWeapon.alpha = 1;
        canvasPWeapon.interactable = true;
        canvasPWeapon.blocksRaycasts = true;
        canvasSWeapon.alpha = 0;
        canvasSWeapon.interactable = false;
        canvasSWeapon.blocksRaycasts = false;
    }

    public void ShowMenuSecondaryWeapon()
    {
        HideCards();
        animSelectionWeapon.SetBool("Menu Active", true);
        canvasSWeapon.alpha = 1;
        canvasSWeapon.interactable = true;
        canvasSWeapon.blocksRaycasts = true;
        canvasPWeapon.alpha = 0;
        canvasPWeapon.interactable = false;
        canvasPWeapon.blocksRaycasts = false;
    }

    public void ChangePrimaryWeapon(int index)
    {
        if (index <= prefabsPrimaryWeapon.Count - 1)
            gameInfo.Character.SetPrimaryWeapon(prefabsPrimaryWeapon[index]);
        SetInfoEquipMenu();
    }

    public void ChangeSecondaryWeapon(int index)
    {
        if (index <= prefabsSecondaryWeapon.Count - 1)
            gameInfo.Character.SetSecondaryWeapon(prefabsSecondaryWeapon[index]);
        SetInfoEquipMenu();
    }

    public void ShowCards()
    {
        animCardsMenu.SetBool("Menu Active", true);
        animSelectionWeapon.SetBool("Menu Active", false);
    }

    public void HideCards()
    {
        animCardsMenu.SetBool("Menu Active", false);
    }
    #endregion
}
