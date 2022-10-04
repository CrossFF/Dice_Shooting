using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private Transform player;
    private Transform turret;
    private List<Transform> enemys; // enemigos que hay en la linea
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform turretPosition;
    [SerializeField] private Transform spawnPoisiiton;

    // propiedades
    public Vector3 PlayerPosition { get { return playerPosition.position; } }
    public Vector3 TurretPosition { get { return turretPosition.position; } }

    public void SetPlayer(Transform p)
    {
        player = p;
    }

    public void ClearPlayer()
    {
        if (player != null) player = null;
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public void SetTurret(Transform t)
    {
        turret = t;
    }

    public void ClearTurret()
    {
        if (turret != null) turret = null;
    }

    public Transform GetTurret()
    {
        return turret;
    }

    public void SpawnEnemy(GameObject enemy)
    {
        if (enemys == null) enemys = new List<Transform>();
        GameObject temp = Instantiate(enemy, spawnPoisiiton.position, Quaternion.identity);
        enemys.Add(temp.transform);
        temp.GetComponent<IEnemy>().Line = this;
    }
}
