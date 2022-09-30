using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    private bool tap, swipeUp, swipeDown;
    private Vector2 startTouch, swipeDelta;
    private bool isDraging = false;

    // acceso publico a las variables de la clase, como parametros
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool Tap { get { return tap; } }

    private void Update()
    {
        // reseteo todos los frames
        tap = swipeDown = swipeUp = false;

        // #region sirve para generar puntos mas claros en el codigo 
        //   y poder ocultar partes del mismo
        //   basicamente para una mejor lectura del codigo, i like it
        #region PC Inputs
        // si presiono el clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            //// estoy haciendo un tap
            tap = true;
            isDraging = true;
            //// y guardo la pocision inicial del mouse al momento de hacer el Swipe
            startTouch = Input.mousePosition;
        }
        // si dejo de precionar el clic izquierdo
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Inputs
        // si hay toques en la pantala
        if (Input.touches.Length != 0)
        {
            //// si el primer touch en la pantalla esta en la fase de empezar
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                ////// estoy haciendo un tap
                tap = true;
                isDraging = true;
                ////// la pocicion de inicio del Swipe es la posicion del touch
                startTouch = Input.touches[0].position;
            }
            //// si el touch inicial termina o es cancelado
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        // calculo la distancia 
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length != 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        // Cruzamos la deadzone?
        if (swipeDelta.magnitude > 125)
        {
            // es un swipe
            // si el valor abosulto de y es mayor al de x
            //  hago algun movimiento
            if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
            {
                if (swipeDelta.y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
