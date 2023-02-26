using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TableSaw : MonoBehaviour
{
    bool tableExist = true;
    public ParticleSystem dustParticle2;
    public GameObject[] table;
    public buttonVR buttonVR;
    public GameObject buttonEnd;
    public TextMeshProUGUI tableLegTimer;
    public TextMeshProUGUI tableTopTimer;
    public TextMeshProUGUI totalTimer2;
    public float currentTime2 = 0f;
    private float totalTime2;

    public AudioSource sawSound2;

    private bool tableLegTimerActive;
    private bool tableTopTimerActive;
    private bool totalTimer2Active;


    private void Start()
    {
        currentTime2 = 0f;
        totalTimer2Active = true;
        tableLegTimerActive = true;
        tableTopTimerActive = false;
    }



    private void Update()
    {
        if(buttonVR.beginTheGame == true && tableLegTimerActive == false && tableTopTimerActive == false)
        {
            totalTimer2Active = true;
        }

        if (totalTimer2Active == true)
        {
            totalTime2 += Time.deltaTime;
            totalTimer2.text = "Total Time: " + totalTime2.ToString("F2") + " seconds";
            if(tableLegTimerActive == false && tableTopTimerActive == false)
                {
                    totalTimer2Active = false;
                }
        }
        
        if (tableLegTimerActive == true)
        {
            currentTime2 = currentTime2 + Time.deltaTime;

            tableLegTimer.text = "Table leg: " + currentTime2.ToString("F2") + " seconds";
            Debug.Log("Check1");

        }

        else if (tableTopTimerActive == true)
        {
            currentTime2 = currentTime2 + Time.deltaTime;

            tableTopTimer.text ="Table Top: " + currentTime2.ToString("F2") + " seconds";
            Debug.Log("Check2");

        }
        if (tableExist == false)
        {
            //Play VFX
            dustParticle2.Play();
        }

        else
        {
            //Stop VFX
            dustParticle2.Stop();
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        //When the saw collide with the log that is for the leg of the table
        //It will destory the log and the leg of the table will appear.
        if (other.tag == "LogTableLeg" && tableExist == true)
        {
            tableExist = false;

            StartCoroutine(waiterA());
            sawSound2.Play();

            IEnumerator waiterA()
            {
                yield return new WaitForSeconds(5);
                Destroy(other.gameObject);
                sawSound2.Stop();
                tableExist = true;
                table[0].SetActive(true);
                table[1].SetActive(true);
                table[2].SetActive(true);
                table[3].SetActive(true);
                tableLegTimerActive = false;
                tableTopTimerActive = true;
                currentTime2 = 0f;

            }
        }

        //When the saw collide with the log that is for the top of the table
        //It will destory the log and the seat of the table will appear.
        else if (other.tag == "LogTableTop" && tableExist == true && tableLegTimerActive == false)
        {
            sawSound2.Play();
            tableExist = false;

            StartCoroutine(waiterB());

            IEnumerator waiterB()
            {
                yield return new WaitForSeconds(5);
                Destroy(other.gameObject);
                sawSound2.Stop();
                tableExist = true;
                table[4].SetActive(true);
                tableTopTimerActive = false;
                currentTime2 = 0f;
                buttonEnd.SetActive(true);
                buttonVR.beginTheGame = false;
            }
        }

    }
}