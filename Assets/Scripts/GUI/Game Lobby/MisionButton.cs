using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MisionButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text tetxDificultySector;
    [SerializeField] private Text textNameSector;
    private Difficulty difficulty;

    public void SetInfo(Difficulty d)
    {
        difficulty = d;
        string nameSector = Random.Range(1, 1000).ToString();
        switch (difficulty)
        {
            case Difficulty.Easy:
                tetxDificultySector.text = "A";
                break;
            case Difficulty.Normal:
                tetxDificultySector.text = "B";
                break;
            case Difficulty.Hard:
                tetxDificultySector.text = "C";
                break;
        }
        textNameSector.text = nameSector;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.Find("Lobby Manager").GetComponent<GameLobby>().StartMision(difficulty);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // agrando el panel
        transform.localScale = new Vector3(1.1f,1.1f,1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // el panel regresa a su tamaño original
        transform.localScale = Vector3.one;
    }
}
