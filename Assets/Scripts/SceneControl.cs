using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(4);
    }

    public void SetDEasy()
    {
        StartGame(Difficulty.Easy);
    }
    public void SetDNormal()
    {
        StartGame(Difficulty.Normal);
    }
    public void SetDHard()
    {
        StartGame(Difficulty.Hard);
    }
    void StartGame(Difficulty difficulty)
    {
        GameInfo gameInfo = GameObject.Find("Game Info").GetComponent<GameInfo>();
        gameInfo.SetDifficulty(difficulty);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    public void YouWin()
    {
        SceneManager.LoadScene(3);
    }
}
