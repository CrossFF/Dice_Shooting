using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class GameLobby : MonoBehaviour
{
    [Header("General References")]
    [SerializeField] private Animator animEquipMenu;
    [SerializeField] private Animator animMisionsMenu;
    [SerializeField] private CinemachineVirtualCamera vcEquip;
    [SerializeField] private CinemachineVirtualCamera vcMisions;
    [SerializeField] private Transform character;

    [Header("Equipment References")]
    [SerializeField] private Text textPrimaryWeapon;
    [SerializeField] private Text textSecondaryWeapon;
    [SerializeField] private Image fillHPImage;
    [SerializeField] private Text textHP;
    [SerializeField] private List<GameObject> prefabsPrimaryWeapon;
    [SerializeField] private List<GameObject> prefabsSecondaryWeapon;
    private GameInfo gameInfo;

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
    }
}
