using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SwipeControls swipeControls;// Movimiento tipo Swipe
    public List<Transform> positionsList; // posiciones que puede estar el personaje
    private AnimationManager animationManager;
    private Vector3 posTarget;
    public float speed;

    //sonido del personaje
    private AudioSource audioSource;
    public AudioClip dashSoundClip;

    // Start is called before the first frame update
    void Start()
    {
        swipeControls = GetComponent<SwipeControls>();
        posTarget = positionsList[1].position;
        animationManager = GetComponent<AnimationManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // movimiento por teclas
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangePosition(false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangePosition(true);
        }
        */

        // movimiento por swipe
        if (swipeControls.SwipeUp)
        {
            ChangePosition(true);
        }
        if (swipeControls.SwipeDown)
        {
            ChangePosition(false);
        }

        // movimiento del personaje desde posicion origen hasta destino
        transform.position = Vector3.MoveTowards(transform.position, posTarget, speed * Time.deltaTime);
    }

    // cambio posicion del personaje en base a si tiene que ir hacia arriba o abajo
    void ChangePosition(bool up)
    {
        int actualPos = 0;
        foreach (var pos in positionsList)
        {
            if (pos.position == transform.position)
            {
                break;
            }
            actualPos++;
        }

        // verifico que pueda moverse en la direccion deseada
        int nextPos = up ? actualPos - 1 : actualPos + 1;
        if (nextPos > -1 && nextPos < 3)
        {
            StartCoroutine(NextPosition(nextPos));
        }
    }

    IEnumerator NextPosition(int index)
    {
        // activo animacion de movimiento
        animationManager.MoveAnimation();
        yield return new WaitForSeconds(0.2f);
        // muevo al personaje a su nueva pocicion
        posTarget = positionsList[index].position;
        // si el sonido a reproducir no es el de movimiento lo cambio
        if (audioSource.clip != dashSoundClip) audioSource.clip = dashSoundClip;
        audioSource.Play();
    }
}
