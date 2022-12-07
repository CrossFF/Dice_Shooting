using UnityEngine;

public class Character
{
    #region Variables y Propiedades
    //variables
    private DicePool dicePool;
    private GameObject primaryWeapon;
    private GameObject secondaryWeapon;
    private float hp;
    //private float damagePWeapon;
    //private float damageSWeapon;

    //propiedades
    public DicePool Dices { get { return dicePool; } }
    public GameObject PrimaryWeapon { get { return primaryWeapon; } }
    public GameObject SecondaryWeapon { get { return secondaryWeapon; } }
    public float HP { get { return hp; } }
    //public float DamagePrimaryWeapon { get { return damagePWeapon; } }
    //public float DamageSecondaryWeapon { get { return damageSWeapon; } }
    #endregion

    #region Constructores
    /*public Character(DicePool dices, GameObject pWeapon, GameObject sWeapon, float health, float pWDamage, float sWDamage)
    {
        dicePool = dices;
        primaryWeapon = pWeapon;
        secondaryWeapon = sWeapon;
        hp = health;
        damagePWeapon = pWDamage;
        damageSWeapon = sWDamage;
    }*/

    public Character(DicePool dices, GameObject pWeapon, GameObject sWeapon, float health)
    {
        dicePool = dices;
        primaryWeapon = pWeapon;
        secondaryWeapon = sWeapon;
        hp = health;
    }
    #endregion

    #region Funciones y Metodos

    #endregion
}
