using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class buttonVR : MonoBehaviour
{
    public GameObject tableList;
    public GameObject chairList;
    public bool beginTheGame;
    public GameObject button;
    public GameObject otherButton;
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
        beginTheGame = false;
        
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
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void beginGame()
    {
        beginTheGame = true;
        chairList.SetActive(true);
        otherButton.SetActive(false);
    }

    public void beginTableGame()
    {

            beginTheGame = true;
            tableList.SetActive(true);
            otherButton.SetActive(false);
    
    }

}
