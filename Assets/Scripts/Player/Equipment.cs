using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    //pool total de dados
    private DicePool dices;
    private int usedDices = 0; // cantidad de dados utilizados
                               // cuando llega a 3 genera 5 dados nuevos para usar.
    private int handSize;

    [Header("Armas")]
    public GameObject prefabPrimaryWeapon;// armas a usar
    private GameObject primaryWeapon;// arma instanciada
    private IWeapon weaponInUse; // arma que se esta usando actualmente

    [Header("UI")]
    [SerializeField] private Transform dicePanel;
    [SerializeField] private GameObject prefabDiceButton;
    private List<GameObject> dicesGameObjetc;

    [Header("Referencias")]
    [SerializeField] private AnimationManager animationManager; // controlador de animaciones
    [SerializeField] private LineManager lineManager;

    private void Start()
    {
        dicesGameObjetc = new List<GameObject>();
        // seteo de armas
        // instancio las armas que va a usar el jugador
        primaryWeapon = Instantiate(prefabPrimaryWeapon, transform);
        // defino el arma que se esta usando en primera instancia
        weaponInUse = primaryWeapon.GetComponent<IWeapon>();
        // seteo una pool de dados iniciales
        dices = new DicePool();
        ShowDiceToUse();
    }

    private void ShowDiceToUse()
    {
        //reseto valores
        handSize = 5;
        usedDices = 0;
        // limpio los dados anteriores
        if (dicesGameObjetc.Count != 0)
        {
            foreach (var item in dicesGameObjetc.ToArray())
            {
                Destroy(item);
            }
        }
        dicesGameObjetc.Clear();

        // muestro los primeros 5 dados a usar
        // elijo de manera random 5 dados a usar
        List<Dice> dicesToUse = dices.GetDices(5);
        //muestro los nuevos dados
        foreach (var item in dicesToUse)
        {
            GameObject temp = Instantiate(prefabDiceButton, dicePanel);
            temp.GetComponent<DiceButon>().SetDice(item, this);
            dicesGameObjetc.Add(temp);
        }
    }

    public void Shoot(Dice dice, int value)
    {
        handSize--;
        if (dice.DiceProperty != DiceProperty.Quick) usedDices++;

        switch (dice.DiceUse)
        {
            case DiceUse.Attack:
                weaponInUse.Shoot(value);
                break;
            case DiceUse.Special1:
                weaponInUse.Special1(value);
                break;
            case DiceUse.Special2:
                weaponInUse.Special2(value);
                break;
        }

        // muestro nuevos dados
        if (usedDices == 3 || handSize == 0)
        {
            ShowDiceToUse();
            weaponInUse.ClearEffects();
        }
    }

    public void AddDice(Dice d)
    {
        dices.AddDice(d);
    }

    public void DeleteDice(Dice d)
    {
        dices.DeleteDice(d);
    }

    public void TransformDice(Dice d, DiceProperty p)
    {
        dices.DeleteDice(d);
        dices.AddDice(new Dice(d.DiceUse, p));
    }

    public List<Dice> GetAllDices()
    {
        return dices.GetAllDices();
    }

    public List<Dice> GetAllUpgradeablesDices()
    {
        return dices.GetAllUpgradeablesDices();
    }

    public List<Dice> GetAllTypeDices(DiceProperty p)
    {
        return dices.GetAllTypeDices(p);
    }
}
