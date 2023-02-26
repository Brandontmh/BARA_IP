using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Saw : MonoBehaviour
{
    bool logExist = true;
    public ParticleSystem dustParticle;
    public GameObject[] chair;

    public buttonVR buttonVR;
    public GameObject buttonEnd;
    public TextMeshProUGUI legTimer;
    public TextMeshProUGUI seatTimer;
    public TextMeshProUGUI backTimer;
    public TextMeshProUGUI totalTimer;
    public float currentTime = 0f;
    private float totalTime;
    
    public AudioSource sawSound;

    private bool legTimerActive;
    private bool seatTimerActive;
    private bool backTimerActive;
    private bool totalTimerActive;


    private void Start()
    {
        currentTime = 0f;
        totalTimerActive = false;
        legTimerActive = false;
        seatTimerActive = false;
        backTimerActive = false;
    }



    public void Update()
    {
        if (buttonVR.beginTheGame == true && seatTimerActive == false && backTimerActive == false)
        {
            totalTimerActive = true;
            legTimerActive = true;
        }

        if (totalTimerActive == true)
        {
            totalTime += Time.deltaTime;
            totalTimer.text = "Total Time: " + totalTime.ToString("F2") + " seconds";
            if (legTimerActive == false && seatTimerActive == false && backTimerActive == false)
            {
                totalTimerActive = false;
            }
        }
       


        if (legTimerActive == true)
        {

            currentTime = currentTime + Time.deltaTime;

            legTimer.text = "Chair leg: " + currentTime.ToString("F2") + " seconds";
            Debug.Log("Check1");

        }

        if (seatTimerActive == true)
        {
            currentTime = currentTime + Time.deltaTime;

            seatTimer.text = "Chair seat: " + currentTime.ToString("F2") + " seconds";
            Debug.Log("Check2");

        }

        if (backTimerActive == true)
        {
            currentTime = currentTime + Time.deltaTime;

            backTimer.text = "Chair back: " + currentTime.ToString("F2") + " seconds";
            Debug.Log("Check3");

        }

        if (logExist == false)
        {
            //Play VFX
            dustParticle.Play();
        }

        else
        {
            //Stop VFX
            dustParticle.Stop();
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        //When the saw collide with the log that is for the leg of the chair
        //It will destory the log and the leg of the chair will appear.
        if (other.tag == "LogChairLeg" && logExist == true)
        {
            logExist = false;

            StartCoroutine(waiter());
            sawSound.Play();

            IEnumerator waiter()
            {
                yield return new WaitForSeconds(5);
                Destroy(other.gameObject);
                sawSound.Stop();
                logExist = true;
                chair[0].SetActive(true);
                chair[1].SetActive(true);
                chair[2].SetActive(true);
                chair[3].SetActive(true);
                legTimerActive = false;
                seatTimerActive = true;
                Debug.Log(legTimerActive);
                Debug.Log(seatTimerActive);
                currentTime = 0f;

            }
        }

        //When the saw collide with the log that is for the seat of the chair
        //It will destory the log and the seat of the chair will appear.
        else if (other.tag == "LogChairSeat" && logExist == true && legTimerActive == false)
        {
            sawSound.Play();
            logExist = false;

            StartCoroutine(waiter1());

            IEnumerator waiter1()
            {
                yield return new WaitForSeconds(5);
                Destroy(other.gameObject);
                sawSound.Stop();
                logExist = true;
                chair[4].SetActive(true);
                seatTimerActive = false;
                currentTime = 0f;
                backTimerActive = true;
            }
        }

        //When the saw collide with the log that is for the backrest of the chair
        //It will destory the log and the backrest of the chair will appear.
        else if (other.tag == "LegChairRest" && logExist == true && seatTimerActive == false && legTimerActive == false)
        {
            sawSound.Play();
            logExist = false;

            StartCoroutine(waiter2());

            IEnumerator waiter2()
            {
                yield return new WaitForSeconds(5);
                Destroy(other.gameObject);
                sawSound.Stop();
                logExist = true;
                chair[5].SetActive(true);
                backTimerActive = false;
                buttonEnd.SetActive(true);
                buttonVR.beginTheGame = false;
            }
        }
    }
}
