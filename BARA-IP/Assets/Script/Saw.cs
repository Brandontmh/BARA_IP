using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    bool cutting = false;

    private void Update()
    {
       
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Log")
        {
            Debug.Log("Collided wiht " + other);
            StartCoroutine(waiter());

            IEnumerator waiter()
            {
                yield return new WaitForSeconds(5);
                Debug.Log("waiting");
                Destroy(other.gameObject);
                
            }
        }
    }
    

}
    