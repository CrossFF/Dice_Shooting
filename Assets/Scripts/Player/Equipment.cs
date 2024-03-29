using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    //pool total de dados
    private DicePool dices;
    private int actionPoints = 3; // cantidad de dados utilizados
                                  // cuando llega a 3 genera 5 dados nuevos para usar.
    private int handSize;

    [Header("Armas")]
    [SerializeField] private GameObject prefabPrimaryWeapon;// arma primaria
    [SerializeField] private GameObject prefabTurret; // torreta a instalar
    private IShootable primaryWeapon; // arma que se esta usando actualmente

    [Header("UI")]
    [SerializeField] private Transform dicePanel;
    [SerializeField] private GameObject prefabDiceButton;
    private List<GameObject> dicesGameObjetc;
    [SerializeField] private Image actionPointImage;

    [Header("Referencias")]
    [SerializeField] private AnimationManager animationManager; // controlador de animaciones
    [SerializeField] private LineManager lineManager; // controlador de Lineas
    [SerializeField] private CameraController cameraController; // controlador de camaras
    private GameInfo gameInfo;

    // referencias como propíedades para el arma
    public AnimationManager AnimationManager { get { return animationManager; } }
    public LineManager LineManager { get { return lineManager; } }
    public CameraController CameraController { get { return cameraController; } }

    private void Start()
    {
        gameInfo = GameObject.Find("Game Info").GetComponent<GameInfo>();
        dicesGameObjetc = new List<GameObject>();
        if (gameInfo.Character != null)
        {
            // seteo la info del personaje segun lo que tenga el game info
            // vida del jugador
            gameObject.GetComponent<PlayerHP>().HP = gameInfo.Character.ActualHP;
            // informacion de equipamiento
            prefabPrimaryWeapon = gameInfo.Character.PrimaryWeapon;
            prefabTurret = gameInfo.Character.SecondaryWeapon;
            dices = gameInfo.Character.Dices;
            // instancio el arma que va a usar el jugador
            GameObject tPrimaryWeapon = Instantiate(prefabPrimaryWeapon, transform);
            // seteo de armas
            primaryWeapon = tPrimaryWeapon.GetComponent<IShootable>();
            tPrimaryWeapon.GetComponent<Weapon>().SetEquipment(this);
        }
        else
        {
            // dados
            dices = new DicePool();
            // instancio el arma que va a usar el jugador
            GameObject tPrimaryWeapon = Instantiate(prefabPrimaryWeapon, transform);
            // seteo de armas
            primaryWeapon = tPrimaryWeapon.GetComponent<IShootable>();
            tPrimaryWeapon.GetComponent<Weapon>().SetEquipment(this);
        }
        // muestro los dados
        ShowDiceToUse();
    }

    private void Update()
    {
        // muestro la cantidad de puntos de accion
        actionPointImage.fillAmount = actionPoints / 3f;
    }

    private void ShowDiceToUse()
    {
        // elijo de manera random 5 dados a usar
        List<Dice> dicesToUse = dices.GetDices(5);
        //seto valores de mano
        handSize = Mathf.Clamp(dicesToUse.Count, 0, 5);
        actionPoints = 3;
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
        if (dice.DiceProperty != DiceProperty.Quick) actionPoints--;

        switch (dice.DiceUse)
        {
            case DiceUse.WeaponAttack:
                primaryWeapon.Shoot(value);
                break;
            case DiceUse.SupportEquip:
                InstallTurret(value);
                break;
            case DiceUse.WeaponSkill:
                primaryWeapon.Special(value);
                break;
        }

        // muestro nuevos dados
        if (actionPoints == 0 || handSize == 0)
        {
            ShowDiceToUse();
            primaryWeapon.ClearEffects();
        }
    }

    void InstallTurret(int dice)
    {
        // Instalo el tipo de torreta que tenga equipado el perosnaje

        ITurret turret;
        // animacion de instalar torreta
        animationManager.InstallTurret();
        // es posible crear una torrera en esta linea?
        if (!lineManager.IsTurretHere())
        {
            // conceguir coordenadas que le corresponde a la torreta
            Vector3 pos = lineManager.GetTurretPosition();
            // instanciar torreta
            GameObject temp = Instantiate(prefabTurret, pos, Quaternion.identity);
            // seteo la torreta
            turret = temp.GetComponent<ITurret>();
            // informo al line manager que guarde la info de la torreta en la linea
            lineManager.SetTurret(temp.transform);
        }
        else
        {
            // aumento el tiempo que pude funcionar la torreta
            turret = lineManager.GetTurret().GetComponent<ITurret>();
        }
        turret.Install(dice);
    }

    public void AddDice(Dice d)
    {
        dices.AddDice(d);
        ShowDiceToUse();
        NewWabe();
    }

    public void DeleteDice(Dice d)
    {
        dices.DeleteDice(d);
        ShowDiceToUse();
        NewWabe();
    }

    public void TransformDice(Dice d, DiceProperty p)
    {
        dices.DeleteDice(d);
        dices.AddDice(new Dice(d.DiceUse, p));
        ShowDiceToUse();
        NewWabe();
    }

    public void UpgradeDice(Dice d)
    {
        d.UpgradeDice();
        ShowDiceToUse();
        NewWabe();
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

    // inicia nueva oleada, para centralizar funciones
    public void NewWabe()
    {
        StartCoroutine(Walking());
    }

    IEnumerator Walking()
    {
        // activo animacion de caminata
        animationManager.Walk(true);
        // muevo el escenario
        GameObject.Find("Ground Control").GetComponent<GroundControl>().MoveGround();
        // despawneo torretas
        lineManager.DespawnTurrets();
        yield return new WaitForSeconds(1.8f);
        // inicio nueva oleada
        animationManager.Walk(false);
        lineManager.ActivateGameMode();
    }

    public void SaveCharacter(int eD, int eND)
    {
        PlayerHP playerHP = GetComponent<PlayerHP>();
        float actualHP = playerHP.HP;
        Character character = new Character(dices,
                                            prefabPrimaryWeapon,
                                            prefabTurret,
                                            actualHP,
                                            gameInfo.Character.MissionCount + 1,
                                            gameInfo.Character.EnemysDefeat + eD,
                                            gameInfo.Character.EnemysNotDefeat + eND);
        gameInfo.SaveCharacter(character);
    }
}
