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
    public GameObject prefabPrimaryWeapon;// armas a usar
    private GameObject primaryWeapon;// arma instanciada
    private IWeapon weaponInUse; // arma que se esta usando actualmente
    private AnimationManager animationManager; // controlador de animaciones
    private DicePanelManager dicePanelManager; // panel de dados
    private LineManager lineManager; 

    private void Start()
    {
        dicePanelManager = GameObject.Find("Panel_Dice Selection").GetComponent<DicePanelManager>();
        lineManager = GameObject.Find("Line Manager").GetComponent<LineManager>();
        animationManager = GetComponent<AnimationManager>();
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
        usedDices = 0;
        // muestro los primeros 5 dados a usar
        // elijo de manera random 5 dados a usar
        List<Dice> dicesToUse = dices.GetDices(5);
        // muestro esos dados en el panel de dados
        dicePanelManager.ShowDicesToUse(dicesToUse, this);
    }

    public void Shoot(Dice dice, int value)
    {
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
        if (usedDices == 3)
        {
            ShowDiceToUse();
            weaponInUse.ClearEffects();
        }
           
    }

    public void AddDice(Dice d)
    {
        dices.AddDice(d);
        lineManager.ActivateWabe();
    }
    
    public void DeleteDice(Dice d)
    {
        dices.DeleteDice(d);
        lineManager.ActivateWabe();
    }

    public void UpgradeDice(Dice d)
    {
        d.UpgradeDice();
        lineManager.ActivateWabe();
    }

    public void TransformDice(Dice d, DiceProperty p)
    {
        dices.DeleteDice(d);
        dices.AddDice(new Dice(d.DiceUse, p));
        lineManager.ActivateWabe();
    }

    public List<Dice> GetAllDices()
    {
        return dices.GetAllDices();
    }

    public List<Dice> GetAllUpgradeablesDices()
    {
        return dices.GetAllUpgradeablesDices();
    }

    public List<Dice> GetAllTypeDices(DiceProperty p){
        return dices.GetAllTypeDices(p);
    }
}
