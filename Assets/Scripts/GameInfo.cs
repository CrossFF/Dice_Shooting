using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfo : MonoBehaviour
{
    static GameObject permanent;
    static Difficulty difficulty = Difficulty.Test;
    static Character character;

    public Difficulty Difficulty { get { return difficulty; } }
    public Character Character { get { return character; } }

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

    public void SaveCharacter(Character chara)
    {
        character = chara;
    }
}
