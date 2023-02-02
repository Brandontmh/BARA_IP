using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject rightTeleportation;

    public InputActionProperty rightActive;


    // Update is called once per frame
    void Update()
    {
        rightTeleportation.SetActive(rightActive.action.ReadValue<float>() > 0.1f);        
    }
}
