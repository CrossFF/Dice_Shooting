using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SwipeControls swipeControls;// Movimiento tipo Swipe
    private AnimationManager animationManager;

    // Referencias al controlador de lineas, 
    //  para el movimiento y otras mecanicas
    public LineManager lineManager;
    private Vector3 posTarget;
    public float speed;

    //sonido del personaje
    private AudioSource audioSource;
    public AudioClip dashSoundClip;

    // Start is called before the first frame update
    void Start()
    {
        animationManager = GetComponent<AnimationManager>();
        audioSource = GetComponent<AudioSource>();
        swipeControls = GetComponent<SwipeControls>();
        //seteo de posicion de personaje
        posTarget = lineManager.GetPlayerPosition();
    }

    // Update is called once per frame
    void Update()
    {
        // movimiento por swipe
        if (swipeControls.SwipeUp)
        {
            ChangePosition(-1);
        }
        if (swipeControls.SwipeDown)
        {
            ChangePosition(+1);
        }

        // movimiento del personaje desde posicion origen hasta destino
        transform.position = Vector3.MoveTowards(transform.position, posTarget, speed * Time.deltaTime);
    }

    // cambio posicion del personaje en base a si tiene que ir hacia arriba o abajo
    void ChangePosition(int yMove)
    {
        Vector3 prevPos = posTarget;
        posTarget = lineManager.MovePlayer(yMove);
        if (prevPos != posTarget)
        {
            animationManager.MoveAnimation();
            if (audioSource.clip != dashSoundClip) audioSource.clip = dashSoundClip;
            audioSource.Play();
        }
    }
}
