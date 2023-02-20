using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    bool logExist = true;
    public ParticleSystem dustParticle;
    public GameObject[] chair;





    private void Update()
    {
        if(logExist == false)
        {
            dustParticle.Play();
        }

        else
        {
            dustParticle.Stop();
        }

    }

    public void OnTriggerEnter(Collider other)
    {
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
            }
        }
        else if (other.tag == "LogChairSeat" && logExist == true)
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
            }
        }
        else if (other.tag == "LogChairRest" && logExist == true)
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
            }
        }
    }
}
    