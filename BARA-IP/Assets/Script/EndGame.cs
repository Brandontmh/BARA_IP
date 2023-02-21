using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject canvas;
    public GameObject quit;
    public GameObject restart;

    public void endGame()
    {
        canvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(1);
    }

}
