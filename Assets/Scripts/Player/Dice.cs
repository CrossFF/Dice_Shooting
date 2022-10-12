using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePool
{
    private List<Dice> dices;

    #region constructores
    public DicePool()
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
    #endregion
    #region fucniones
    public List<Dice> GetDices(int amount)
    {
        List<Dice> tempDices = new List<Dice>();
        List<Dice> dicesToUse = new List<Dice>();
        foreach (var item in dices)
        {
            tempDices.Add(item);
        }
        for (int i = 0; i < amount; i++)
        {
            if (tempDices.Count != 0)
            {
                int index = Random.Range(0, tempDices.Count);
                dicesToUse.Add(tempDices[index]);
                tempDices.Remove(tempDices[index]);
            }
            else
            {
                break;
            }
        }
        return dicesToUse;
    }

    public List<Dice> GetAllDices()
    {
        List<Dice> tempDices = new List<Dice>();
        foreach (var item in dices)
        {
            tempDices.Add(item);
        }
        return tempDices;
    }

    public void AddDice(Dice d)
    {
        dices.Add(d);
    }

    public void DeleteDice(Dice d)
    {
        dices.Remove(d);
    }

    public List<Dice> GetAllUpgradeablesDices()
    {
        List<Dice> tempDices = new List<Dice>();
        foreach (var item in dices)
        {
            if (item.IsUpgradePosible()) tempDices.Add(item);
        }
        return tempDices;
    }

    public List<Dice> GetAllTypeDices(DiceProperty p)
    {
        List<Dice> tempDices = new List<Dice>();
        foreach (var item in dices)
        {
            if (item.DiceProperty == p) tempDices.Add(item);
        }
        return tempDices;
    }
    #endregion
    #region static functions
    public static List<Dice> GetNewDices(int amount)
    {
        List<Dice> tempDices = new List<Dice>();
        for (int i = 0; i < amount; i++)
        {
            // defino el uso del dado
            DiceUse diceUse;
            int num = Random.Range(0, 3);
            switch (num)
            {
                case 0:
                    diceUse = DiceUse.Attack;
                    break;
                case 1:
                    diceUse = DiceUse.Special1;
                    break;
                case 2:
                    diceUse = DiceUse.Special2;
                    break;
                default:
                    diceUse = DiceUse.Attack;
                    break;
            }
            // defino el tipo del dado
            DiceProperty diceProperty;
            num = Random.Range(0, 3);
            switch (num)
            {
                case 0:
                    diceProperty = DiceProperty.Normal;
                    break;
                case 1:
                    diceProperty = DiceProperty.Advanced;
                    break;
                case 2:
                    diceProperty = DiceProperty.Quick;
                    break;
                default:
                    diceProperty = DiceProperty.Normal;
                    break;
            }
            // defino el nivel
            num = Random.Range(0, 4);
            // agrego el dado a la lista
            tempDices.Add(new Dice(diceUse, diceProperty, num));
        }
        return tempDices;
    }
    #endregion
}

public class Dice
{
    private DiceUse diceUse;
    private int minValue;
    private int maxValue;
    private int upgrades;
    private DiceProperty diceProperty;

    // propiedades
    public DiceUse DiceUse { get { return diceUse; } }
    public DiceProperty DiceProperty { get { return diceProperty; } }
    public int MinValue { get { return minValue; } }
    public int MaxValue { get { return maxValue; } }

    #region Constructors
    public Dice(DiceUse use)
    {
        diceUse = use;
        minValue = 1;
        maxValue = 2;
        upgrades = 0;
        diceProperty = DiceProperty.Normal;
    }

    public Dice(DiceUse use, DiceProperty property)
    {
        diceUse = use;
        diceProperty = property;
        if (diceProperty == DiceProperty.Advanced)
        {
            minValue = 3;
            maxValue = 4;
        }
        else
        {
            minValue = 1;
            maxValue = 2;
        }
        upgrades = 0;
    }

    public Dice(DiceUse use, DiceProperty property, int level)
    {
        diceUse = use;
        diceProperty = property;

        // dependiendo la propiedad el dado tiene un numero maximo de mejoras
        // y seteo el valor minimo del dado
        if (diceProperty == DiceProperty.Quick)
        {
            // dado rapido, 1 mejora, minimo 1
            minValue = 1;
            if (level > 0 && level < 2)
            {
                upgrades = level;
            }
            else
            {
                upgrades = 0;
            }
        }
        else
        {
            if (diceProperty == DiceProperty.Normal)
            {
                // dado normal, 2 mejoras, minimo 1
                minValue = 1;
                if (level > 0 && level < 3)
                {
                    upgrades = level;
                }
                else
                {
                    upgrades = 0;
                }
            }
            else
            {
                // dado avanzado, 3 mejoras, minimo 3
                minValue = 3;
                if (level > 0 && level < 4)
                {
                    upgrades = level;
                }
                else
                {
                    upgrades = 0;
                }
            }
        }

        // aplico el valor maximo dependiendo la propiedad y cantidad de mejoras
        switch (diceProperty)
        {
            case DiceProperty.Normal:
                switch (upgrades)
                {
                    case 0:
                        maxValue = 2;
                        break;
                    case 1:
                        maxValue = 3;
                        break;
                    case 2:
                        maxValue = 4;
                        break;
                }
                break;
            case DiceProperty.Advanced:
                switch (upgrades)
                {
                    case 0:
                        maxValue = 4;
                        break;
                    case 1:
                        maxValue = 5;
                        break;
                    case 2:
                        maxValue = 6;
                        break;
                    case 3:
                        maxValue = 7;
                        break;
                }
                break;
            case DiceProperty.Quick:
                switch (upgrades)
                {
                    case 0:
                        maxValue = 2;
                        break;
                    case 1:
                        maxValue = 3;
                        break;
                }
                break;
        }
    }
    #endregion
    #region Functions
    public int RollDice()
    {
        return Random.Range(minValue, maxValue);
    }

    public void UpgradeDice()
    {
        // aumento el nivel
        switch (diceProperty)
        {
            case DiceProperty.Normal:
                if (upgrades + 1 < 3) upgrades++;
                break;
            case DiceProperty.Advanced:
                if (upgrades + 1 < 4) upgrades++;
                break;
            case DiceProperty.Quick:
                if (upgrades + 1 < 2) upgrades++;
                break;
        }
        // aumento el valor maximo del dado
        switch (diceProperty)
        {
            case DiceProperty.Normal:
                switch (upgrades)
                {
                    case 1:
                        maxValue = 3;
                        break;
                    case 2:
                        maxValue = 4;
                        break;
                }
                break;
            case DiceProperty.Advanced:
                switch (upgrades)
                {
                    case 1:
                        maxValue = 5;
                        break;
                    case 2:
                        maxValue = 6;
                        break;
                    case 3:
                        maxValue = 7;
                        break;
                }
                break;
            case DiceProperty.Quick:
                switch (upgrades)
                {
                    case 1:
                        maxValue = 3;
                        break;
                }
                break;
        }
    }

    public bool IsUpgradePosible()
    {
        bool r = false;
        switch (diceProperty)
        {
            case DiceProperty.Normal:
                if (upgrades + 1 < 3) r = true;
                break;
            case DiceProperty.Advanced:
                if (upgrades + 1 < 4) r = true;
                break;
            case DiceProperty.Quick:
                if (upgrades + 1 < 2) r = true;
                break;
        }
        return r;
    }
    #endregion
}

public enum DiceUse
{
    Attack,
    Special1,
    Special2
}

public enum DiceProperty
{
    Normal,
    Advanced,
    Quick
}
