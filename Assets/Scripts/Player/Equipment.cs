using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    //pool total de dados
    private List<Dice> dices;
    private int usedDices = 0; // cantidad de dados utilizados
                               // cuando llega a 3 genera 5 dados nuevos para usar.

    public GameObject prefabPrimaryWeapon;// armas a usar
    private GameObject primaryWeapon;// arma instanciada
    private IWeapon weaponInUse; // arma que se esta usando actualmente
    private AnimationManager animationManager; // controlador de animaciones
    public DicePanelManager dicePanelManager; // panel de dados

    private void Start()
    {
        animationManager = GetComponent<AnimationManager>();
        // seteo de armas
        // instancio las armas que va a usar el jugador
        primaryWeapon = Instantiate(prefabPrimaryWeapon, transform);
        // defino el arma que se esta usando en primera instancia
        weaponInUse = primaryWeapon.GetComponent<IWeapon>();

        // si la lista de dados no existe creo una
        if (dices == null)
        {
            dices = new List<Dice>();
            for (int i = 0; i < 2; i++)
            {
                // ataque
                dices.Add(new Dice(DiceUse.Attack));
            }
            for (int i = 0; i < 2; i++)
            {
                // ataque 1 mejora
                dices.Add(new Dice(DiceUse.Attack, DiceProperty.Normal, 1));
            }
            for (int i = 0; i < 2; i++)
            {
                // especial 1
                dices.Add(new Dice(DiceUse.Special1));
            }
            for (int i = 0; i < 2; i++)
            {
                //especial 2
                dices.Add(new Dice(DiceUse.Special2));
            }
        }

        ShowDiceToUse();
    }

    private void ShowDiceToUse()
    {
        usedDices = 0;
        // muestro los primeros 5 dados a usar
        // elijo de manera random 5 dados a usar
        List<Dice> dicesToUse = new List<Dice>();
        List<Dice> tempDices = new List<Dice>();
        foreach (var item in dices)
        {
            tempDices.Add(item);
        }
        for (int i = 0; i < 5; i++)
        {
            //print(tempDices.Count);
            int index = Random.Range(0, tempDices.Count);
            dicesToUse.Add(tempDices[index]);
            tempDices.Remove(tempDices[index]);
            //print(tempDices.Count);
        }
        // muestro esos dados en el panel de dados
        dicePanelManager.ShowDicesToUse(dicesToUse, this);
    }

    public void Shoot(Dice dice)
    {
        if (dice.diceProperty != DiceProperty.Quick) usedDices++;

        switch (dice.diceUse)
        {
            case DiceUse.Attack:
                weaponInUse.Shoot(dice);
                break;
            case DiceUse.Special1:
                weaponInUse.Special1(dice);
                break;
            case DiceUse.Special2:
                weaponInUse.Special2(dice);
                break;
        }

        // muestro nuevos dados
        if (usedDices == 3)
        {
            ShowDiceToUse();
            weaponInUse.ClearEffects();
        }
           
    }
}
