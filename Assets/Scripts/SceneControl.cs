using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void StartGame()
    {
        // escena de Misiones y Equipo
        SceneManager.LoadScene(4);
    }

    public void StartGame(Difficulty difficulty)
    {
        // Modo de Juego EXTERMINIO
        GameInfo gameInfo = GameObject.Find("Game Info").GetComponent<GameInfo>();
        gameInfo.SetDifficulty(difficulty);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        // Cerrar Aplicacion
        Application.Quit();
    }

    public void MainMenu()
    {
        // Menu Principal
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        // Escena de GAME OVER
        GameObject.Find("Game Info").GetComponent<GameInfo>().SaveCharacter(null);
        SceneManager.LoadScene(2);
    }

    public void YouWin()
    {
        // Escena de GANASTE, pero la voy a eliminar, sobra un poquito
        SceneManager.LoadScene(3);
    }
}
