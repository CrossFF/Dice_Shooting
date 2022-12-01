using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDeCarteles : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private Text text;
    [SerializeField]private Animator animator;
    [SerializeField]private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0f;
    }

    public void MostrarCartel(string mensaje)
    {
        text.text = mensaje;
        animator.SetTrigger("Activar");
    }

    public void CambiarAlpha(float alpha)
    {

    }
}
