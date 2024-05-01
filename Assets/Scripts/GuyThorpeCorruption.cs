using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GuyThorpeCorruption : MonoBehaviour
{
    [Header("OpenUI")]
    public GameObject UIObject;
    public GameObject InteractE;

   
    private bool playerInsideTrigger;
    private bool InObject;

    public FPCharacterController fpCharacterController;
    public Button Exit; 

    // Start is called before the first frame update
    void Start()
    {
      //  Exit.onClick.AddListener(() => SelectOption(1));
        UIObject.SetActive(false);
        InteractE.SetActive(false);
        InObject = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractE.SetActive(true);
            playerInsideTrigger = true;
            Debug.Log("Collider"); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIObject.SetActive(false);
            InteractE.SetActive(false);
            playerInsideTrigger = false;
            Debug.Log("Exit Collider"); 
        }
    }

    void Update()
    {

        if (playerInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            UIObject.SetActive(true);
            Debug.Log("E is Pressed");
            InObject = true;
            if (fpCharacterController != null)
            {
                fpCharacterController.SetMovementEnabled(false);
                fpCharacterController.SetRotationEnabled(false);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
       
    }
}
