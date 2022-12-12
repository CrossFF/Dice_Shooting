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

    [Header("Mission References")]
    [SerializeField] private Transform DisplayMision;
    [SerializeField] private GameObject prefabButtonMision;
    [SerializeField] private Text textMissions;
    [SerializeField] private Text textEnemysDefeat;
    [SerializeField] private Text textEnemysNotDefeat;
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
            Character newCharacter = new Character(new DicePool(),
                                                   prefabsPrimaryWeapon[0],
                                                   prefabsSecondaryWeapon[0],
                                                   5,
                                                   0,
                                                   0,
                                                   0);
            gameInfo.SaveCharacter(newCharacter);
        }
        SetInfoEquipMenu();

        //instancio misiones para que el jugador
        GenerateMissions();
        ShowMissionsStats();
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
        if (tCards == null) tCards = new List<GameObject>();
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
        animSelectionWeapon.SetBool("Menu Active", false);
        if (index <= prefabsPrimaryWeapon.Count - 1)
            gameInfo.Character.SetPrimaryWeapon(prefabsPrimaryWeapon[index]);
        SetInfoEquipMenu();
    }

    public void ChangeSecondaryWeapon(int index)
    {
        animSelectionWeapon.SetBool("Menu Active", false);
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
    #region Mision Functions
    public void GenerateMissions()
    {
        // creo misiones en base a la cantidad de misiones completadas
        if (gameInfo.Character.MissionCount < 2)
        {
            // genero 3 misiones faciles
            for (int i = 0; i < 3; i++)
            {
                Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Easy);
            }
        }
        if (gameInfo.Character.MissionCount > 1 && gameInfo.Character.MissionCount < 5)
        {
            switch (gameInfo.Character.MissionCount)
            {
                case 2:
                    // genero 2 misiones faciles, 1 normal}
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Easy);
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Easy);
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Normal);
                    break;
                case 3:
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Easy);
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Normal);
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Normal);
                    break;
                case 4:
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Normal);
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Normal);
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Normal);
                    break;
            }
        }
        if (gameInfo.Character.MissionCount > 4 && gameInfo.Character.MissionCount < 7)
        {
            switch (gameInfo.Character.MissionCount)
            {
                case 5:
                    // genero 2 misiones normales, 1 dificil
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Normal);
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Normal);
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Hard);
                    break;
                case 6:
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Normal);
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Hard);
                    Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Hard);
                    break;
            }
        }
        if (gameInfo.Character.MissionCount > 6)
        {
            // genero 3 misiones dificiles
            for (int i = 0; i < 3; i++)
            {
                Instantiate(prefabButtonMision, DisplayMision).GetComponent<MisionButton>().SetInfo(Difficulty.Hard);
            }
        }
    }

    public void StartMision(Difficulty difficulty)
    {
        GetComponent<SceneControl>().StartGame(difficulty);
    }

    void ShowMissionsStats()
    {
        textMissions.text = gameInfo.Character.MissionCount.ToString();
        textEnemysDefeat.text = gameInfo.Character.EnemysDefeat.ToString();
        textEnemysNotDefeat.text = gameInfo.Character.EnemysNotDefeat.ToString();
    }
    #endregion
}
