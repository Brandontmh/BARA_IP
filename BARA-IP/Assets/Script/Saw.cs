using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Saw : MonoBehaviour
{
    bool logExist = true;
    public ParticleSystem dustParticle;
    public GameObject[] chair;

    public TextMeshProUGUI legTimer;
    public TextMeshProUGUI seatTimer;
    public TextMeshProUGUI backTimer;
    public TextMeshProUGUI totalTimer;
    public float currentTime = 0f;
    private float totalTime;

    private bool legTimerActive = true;
    private bool seatTimerActive = false;
    private bool backTimerActive = false;
    private bool totalTimerActive = true;


    private void Awake()
    {
        currentTime = 0f;
        totalTimerActive = true;
        legTimerActive = true;
        seatTimerActive = false;
        backTimerActive = false;
}



    private void Update()
    {
        if(totalTimerActive = true)
        {
            totalTime += Time.deltaTime;
            totalTimer.text = totalTime.ToString("F2") + " seconds";
        }

        if (legTimerActive = true)
        {
            currentTime = currentTime + Time.deltaTime;

            legTimer.text = currentTime.ToString("F2") + " seconds";
            Debug.Log("Check1");

        }

        else if (seatTimerActive = true)
        {
            currentTime = currentTime + Time.deltaTime;

            seatTimer.text = currentTime.ToString("F2") + " seconds";
            Debug.Log("Check2");

        }

        else if (backTimerActive = true)
        {
            currentTime = currentTime + Time.deltaTime;

            backTimer.text = currentTime.ToString("F2") + " seconds";
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
            Debug.Log("logExist is true");
            Debug.Log("Collided with " + other);

            StartCoroutine(waiter());

            IEnumerator waiter()
            {
                yield return new WaitForSeconds(5);
                Debug.Log("waiting");
                Destroy(other.gameObject);
                logExist = true;
                Debug.Log("logExist is false");
                chair[0].SetActive(true);
                chair[1].SetActive(true);
                chair[2].SetActive(true);
                chair[3].SetActive(true);
                legTimerActive = false;
                currentTime = 0f;
                seatTimerActive = true;
                Debug.Log(seatTimerActive);
            }
        }

        //When the saw collide with the log that is for the seat of the chair
        //It will destory the log and the seat of the chair will appear.
        else if (other.tag == "LogChairSeat" && logExist == true && legTimerActive == false)
        {
            logExist = false;
            Debug.Log("logExist is true");
            Debug.Log("Collided with " + other);

            StartCoroutine(waiter1());

            IEnumerator waiter1()
            {
                yield return new WaitForSeconds(5);
                Debug.Log("waiting");
                Destroy(other.gameObject);
                logExist = true;
                Debug.Log("logExist is false");
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
            logExist = false;
            Debug.Log("logExist is true");
            Debug.Log("Collided with " + other);

            StartCoroutine(waiter2());

            IEnumerator waiter2()
            {
                yield return new WaitForSeconds(5);
                Debug.Log("waiting");
                Destroy(other.gameObject);
                logExist = true;
                Debug.Log("logExist is false");
                chair[5].SetActive(true);
                backTimerActive = false;
            }
        }
    }
}
    