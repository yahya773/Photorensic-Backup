using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyThorpeCorruption : MonoBehaviour
{
    [Header("OpenUI")]
    public GameObject UIObject;
    public GameObject InteractE;

   
    private bool playerInsideTrigger;

    // Start is called before the first frame update
    void Start()
    {
        UIObject.SetActive(false);
        InteractE.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractE.SetActive(true);
            playerInsideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIObject.SetActive(false);
            InteractE.SetActive(false);
            playerInsideTrigger = false;
        }
    }

    void Update()
    {
       
        if (playerInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            UIObject.SetActive(true);
        }
    }
}
