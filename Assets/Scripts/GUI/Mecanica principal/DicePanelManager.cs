using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePanelManager : MonoBehaviour
{
    public GameObject prefabDiceButton;
    public List<GameObject> dices;

    private void Start()
    {
        dices = new List<GameObject>();
    }
    public void ShowDicesToUse(List<Dice> d, Equipment equipment)
    {
        // limpio los dados anteriores
        if (dices.Count != 0)
        {
            foreach (var item in dices.ToArray())
            {
                Destroy(item);
            }
        }
        dices.Clear();
        //muestro los nuevos dados
        foreach (var item in d)
        {
            GameObject temp = Instantiate(prefabDiceButton, transform);
            temp.GetComponent<DiceButon>().SetDice(item, equipment);
            dices.Add(temp);
        }
    }
}
