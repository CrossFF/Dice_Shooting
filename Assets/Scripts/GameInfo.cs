using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfo : MonoBehaviour
{
    static GameObject permanent;
    static Difficulty difficulty = Difficulty.Easy;

    public Difficulty Difficulty { get { return difficulty; } }

    private void Awake()
    {
        if (permanent != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            permanent = this.gameObject;
        }
        DontDestroyOnLoad(permanent);
    }

    public void SetDifficulty(Difficulty d)
    {
        difficulty = d;
    }
}
