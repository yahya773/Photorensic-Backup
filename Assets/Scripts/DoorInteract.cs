using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator myDoor = null;

    [Header("Door")]
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private string doorOpen = "DoorOpen";
    [SerializeField] private string doorClose = "DoorClose"; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.Play("DoorOpen", 0, 0.0f);
                gameObject.SetActive(false); 
            }

            else if (closeTrigger)
            {
                myDoor.Play("DoorClose2", 0, 0.0f); 
                gameObject.SetActive(false);
            }
        }
    }


}
