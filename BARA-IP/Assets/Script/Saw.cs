using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    bool logExist = true;
    public ParticleSystem dustParticle;
    public GameObject chairLeg1;
    public GameObject chairLeg2;
    public GameObject chairLeg3;
    public GameObject chairLeg4;



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
        if (other.tag == "Log" && logExist == true)
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
                Instantiate(chairLeg1, transform.position, Quaternion.identity);
                Instantiate(chairLeg2, transform.position, Quaternion.identity);
                Instantiate(chairLeg3, transform.position, Quaternion.identity);
                Instantiate(chairLeg4, transform.position, Quaternion.identity);


            }
        }
    }
    

}
    