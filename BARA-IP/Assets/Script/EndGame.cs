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
    public AudioSource congrats;
    public bool gameEnded;

    private void Awake()
    {
        gameEnded = false;
    }
    public void endGame()
    {
        canvas.SetActive(true);
        gameEnded = true;
        congrats.Play();
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
