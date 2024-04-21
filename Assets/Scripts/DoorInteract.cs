using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] public Animator OpenDoor;
    [SerializeField] public Animator CloseDoor; 

    [Header("UI Interact")]
    [SerializeField] private GameObject InteractE;

    [Header("Door")]
    [SerializeField] public GameObject Door; 

    private void Start()
    {
        InteractE.SetActive(false);
        Animator animator = Door.GetComponent<Animator>();
    }

    
    private void OnTriggerEnter (Collider other) 
    {
        Debug.Log("EnterDoor"); 
        InteractE.SetActive(true); 

        if(Input.GetKey(KeyCode.E)) 
        {
            OpenDoor.Play("OpenDoor");
            Debug.Log("AnimationDoorPlay"); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractE.SetActive(false);
        Debug.Log("LeaveDoor"); 
    }
}
