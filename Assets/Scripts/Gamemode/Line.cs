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
    [SerializeField] private int layerForSprite;

    // propiedades
    public Vector3 PlayerPosition { get { return playerPosition.position; } }
    public Vector3 TurretPosition { get { return turretPosition.position; } }
    public int LayerForSprite { get { return layerForSprite; } }

    private void Start()
    {
        enemys = new List<Transform>();
    }

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
        t.GetComponent<ITurret>().Line = this;
    }

    public void ClearTurret()
    {
        if (turret != null) turret = null;
    }

    public void DespawnTurret()
    {
        if(turret != null)
        {
            ITurret t = turret.GetComponent<ITurret>();
            ClearTurret();
            t.Despawn();
        }
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

    public void RemoveEnemy(Transform e)
    {
        enemys.Remove(e);
    }

    public Transform GetEnemy()
    {
        if (enemys.Count != 0)
        {
            return enemys[0];
        }
        else
        {
            return null;
        }
    }

    public List<Transform> GetEnemys(int amount)
    {
        List<Transform> list = new List<Transform>();
        for (int i = 0; i < enemys.Count; i++)
        {
            if (i < amount)
            {
                list.Add(enemys[i]);
            }
        }
        return list;
    }

    public List<Transform> GetAllEnemys()
    {
        return enemys;
    }

    public Transform GetTargetToEnemy()
    {
        if (GetTurret())
        {
            return GetTurret();
        }
        else if (GetPlayer())
        {
            return GetPlayer();
        }
        else
        {
            return null;
        }
    }
}
