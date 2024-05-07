using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    public GameObject FadeIn2;

    public float Timer = 3.0f; 

    // Start is called before the first frame update
    void Start()
    {
        FadeIn2.SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            FadeIn2.SetActive(true);
            Debug.Log("FadeIn"); 
        }
        else
        {
            FadeIn2.SetActive(false); 
        }
    }
}
