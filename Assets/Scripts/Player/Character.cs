using UnityEngine;

public class Character
{
    #region Variables y Propiedades
    //variables
    private DicePool dicePool;
    private GameObject primaryWeapon;
    private GameObject secondaryWeapon;
    private float actualHP;
    private int missionsCount;
    private int enemysDefeat;
    private int enemysNotDefeat;

    //propiedades
    public DicePool Dices { get { return dicePool; } }
    public GameObject PrimaryWeapon { get { return primaryWeapon; } }
    public GameObject SecondaryWeapon { get { return secondaryWeapon; } }
    public float ActualHP { get { return actualHP; } }
    public int MissionCount { get { return missionsCount; } }
    public int EnemysDefeat { get { return enemysDefeat; } }
    public int EnemysNotDefeat { get { return enemysNotDefeat; } }


    #endregion

    #region Constructores
    public Character(DicePool dices, GameObject pWeapon, GameObject sWeapon, float actHP, int missions, int eD, int eND)
    {
        dicePool = dices;
        primaryWeapon = pWeapon;
        secondaryWeapon = sWeapon;
        actualHP = actHP;
        missionsCount = missions;
        enemysDefeat = eD;
        enemysNotDefeat = eND;
    }
    #endregion

    #region Funciones y Metodos
    public void SetPrimaryWeapon(GameObject weapon)
    {
        primaryWeapon = weapon;
    }

    public void SetSecondaryWeapon(GameObject weapon)
    {
        secondaryWeapon = weapon;
    }
    #endregion
}
