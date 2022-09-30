using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // velocidad del proyectil
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // muevo el proyectil hacia la derecha
        transform.position += Vector3.right * speed * Time.deltaTime;    
    }
}
