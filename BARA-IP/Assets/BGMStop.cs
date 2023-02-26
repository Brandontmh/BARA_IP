using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMStop : MonoBehaviour
{
    public AudioSource bgm;
    public EndGame EndGame;
    public void backgroundMusic()
    {
        if (EndGame.gameEnded == false)
        {
            bgm.Play();
        }

       else if(EndGame.gameEnded == true)
        {
            bgm.Stop();
        }
    }
}
