using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{

    public GameObject InteractE;


    public void Start()
    {
        InteractE.SetActive(false); 
    }
    private void OnTriggerEnter(Collider other)
    {
        InteractE.SetActive(true); 
    }

    private void OnTriggerExit(Collider other)
    {
        InteractE.SetActive(false); 
    }
}
