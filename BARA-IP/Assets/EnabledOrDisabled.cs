using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledOrDisabled : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Saw")
        {
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(5);
        Object.Destroy(this.gameObject);
    }
}
