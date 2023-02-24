using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class buttonVR : MonoBehaviour
{
    public GameObject button;
    //public Rigidbody shoppingListModel;
    //public Transform spawnpoint;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void beginMiniGame()
    {
        RevealUI.freePlayStatus = true;
        Debug.Log("off Ui");
        DetectObjects.stopwatchActive = true;
        //Instantiate(shoppingListModel, spawnpoint.position, spawnpoint.rotation);
    }

}
