using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    private IDamageable damageable;
    [SerializeField] private Image imageFill;
    [SerializeField] private Image fixedImageFill;
    [SerializeField] private Text text;
    private float inicialHP;

    void Start()
    {
        damageable = transform.parent.GetComponent<IDamageable>();
        inicialHP = damageable.HP;
    }

    void Update()
    {
        // correccion de vida maxima, en caso de que el
        //  objeto supere la vida inicial.
        if (damageable.HP > inicialHP) inicialHP = damageable.HP;
        // muestro la vida restante en forma de porcentaje
        imageFill.fillAmount = damageable.HP / inicialHP;
        if(fixedImageFill.fillAmount > imageFill.fillAmount)
        {
            fixedImageFill.fillAmount -= 1f * Time.deltaTime;
        }
        text.text = damageable.HP + "/" + inicialHP;
    }
}
