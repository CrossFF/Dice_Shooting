using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public GameObject prefabPrimaryWeapon;// armas posibles de usar: proximamente una lista
    private GameObject primaryWeapon;// arma instanciada
    private IWeapon weaponInUse; // arma que se esta usando actualmente
    public List<int> diceList; // lista de dados a disparar
    private AnimationManager animationManager; // controlador de animaciones
    public Text diceTextButton; // texto del boton de dados
    public Button diceButton; // boton de los dados
    public List<int> diceBuildList; // lista de dados guardados
    public List<Image> dicesInBuild; // lista de imagenes de dados guardados

    private void Start() 
    {
        animationManager = GetComponent<AnimationManager>();
        // seteo de armas
        // instancio las armas que va a usar el jugador
        primaryWeapon = Instantiate(prefabPrimaryWeapon, transform);
        // defino el arma que se esta usando en primera instancia
        weaponInUse = primaryWeapon.GetComponent<IWeapon>();

        // si la lista de dados no existe creo una
        if (diceList.Count == 0)
        {
            GenerateDices();
        }

        // muestro el primer dado a disparar
        diceTextButton.text = diceList[0].ToString();
    }

    private void Update() 
    {

    }

    void GenerateDices()
    {
        diceList = new List<int>();

        // si la lista de dados guardados es mayor a 0
        if(diceBuildList.Count > 0)
        {
            //// coloco esos dados como primeros elementos de dados disponibles
            foreach (var item in diceBuildList)
            {
                diceList.Add(item);
            }
        }

        // genero dados en base a la cantidad de dados que estuvieran guardados 
        for (int i = 0; i < 6 - diceBuildList.Count; i++)
        {
            diceList.Add(Random.Range(1, 7));
        }

        // limpio la lista de dados guardados
        diceBuildList.Clear();

        // oculto los dados de la lista de Dados guardados
        foreach (var item in dicesInBuild)
        {
            item.GetComponent<CanvasGroup>().alpha = 0f;
        }
    }

    void NextDiceAviable()
    {
        // mostramos el proximo dado a disparar
        // si la lista de dados no esta vacia
        if (diceList.Count != 0)
        {
            //// muestro el primero elemento de la lista
            diceTextButton.text = diceList[0].ToString();
            //// si el boton es ininteractuable, lo hago interactuable
            if (!diceButton.interactable) diceButton.interactable = true;
        }
        else
        {
            //// hago el boton de dados ininteractuable   
            diceTextButton.text = "Empty";
            diceButton.interactable = false;
        }
    }

    public void Shoot()
    {
        // verifico si hay balas
        //// si hay disparo
        if (diceList.Count != 0)
        {
            ////// le digo al arma que dispare
            weaponInUse.Shoot(diceList[0]);
            ////// activo animacion de disparo
            animationManager.ShootAnimation();
            ////// gasto una bala
            diceList.RemoveAt(0);
        }
        else
        //// sino recargo
        {
            ////// genero un nuevo array de dados
            GenerateDices();
            ////// activo animacion de recarga
        }

        NextDiceAviable();
    }

    public void SaveDice()
    {
        // si la lista de dados guardados no existe la creo
        if (diceBuildList.Count == 0) diceBuildList = new List<int>();

        // si la lista de dados guardados tiene menos de 2 elementos
        if (diceBuildList.Count < dicesInBuild.Count)
        {
            //// guardo el primer elemnto de la lista de dados disponibles
            //// en la lista de dados guardados
            diceBuildList.Add(diceList[0]);
            //// borro el dado guardado de la lista de dados disponibles
            diceList.RemoveAt(0);
            //// actualizo la visual del panel y lista de dados disponibles
            //// activo animacion de guardado

            //// muestro dado en lugar correspondiente dentro del panel
            int lastIndex = diceBuildList.Count - 1;
            dicesInBuild[lastIndex].GetComponent<CanvasGroup>().alpha = 1;
            Text textDice = dicesInBuild[lastIndex].transform.GetChild(0).GetComponent<Text>();
            textDice.text = diceBuildList[lastIndex].ToString();
            //// muestro proximo dado disponible
            NextDiceAviable();
        }
        // sino muestro un mensaje de error
        else
        {
            //// activo animacion de dados completos
        }
    }
}
