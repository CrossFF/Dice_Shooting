using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice
{
    public DiceUse diceUse;
    public int minValue;
    public int maxValue;
    public int upgrades;
    public DiceProperty diceProperty;

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
    public void UpgradeDice()
    {
        if (diceProperty == DiceProperty.Quick)
        {
            if (upgrades + 1 < 2)
            {
                upgrades++;
            }
        }
        else
        {
            if (upgrades + 1 < 3)
            {
                upgrades++;

            }
        }
        // aumento el uso maximo
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
