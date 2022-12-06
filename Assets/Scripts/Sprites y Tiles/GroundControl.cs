using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    [SerializeField] private Transform ground1;
    [SerializeField] private Transform ground2;
    [SerializeField] private Transform spawn;
    [SerializeField] private Transform center;
    [SerializeField] private Transform despawn;
    [SerializeField] private float vel;
    private bool groundToShow = true;
    private bool move = false;

    void Update()
    {
        if (move)
        {
            if (groundToShow)
            {
                ground1.position = Vector3.MoveTowards(ground1.position, despawn.position, vel * Time.deltaTime);
                ground2.position = Vector3.MoveTowards(ground2.position, center.position, vel * Time.deltaTime);
                if (ground1.position.x <= despawn.position.x)
                {
                    ground1.position = spawn.position;
                    ground1.GetComponent<GroundTile>().ChangeSprites();
                    groundToShow = false;
                    move = false;
                }
            }
            else
            {
                ground2.position = Vector3.MoveTowards(ground2.position, despawn.position, vel * Time.deltaTime);
                ground1.position = Vector3.MoveTowards(ground1.position, center.position, vel * Time.deltaTime);
                if (ground2.position.x <= despawn.position.x)
                {
                    ground2.position = spawn.position;
                    ground2.GetComponent<GroundTile>().ChangeSprites();
                    groundToShow = true;
                    move = false;
                }
            }
        }
    }

    public void MoveGround()
    {
        move = true;
    }
}
