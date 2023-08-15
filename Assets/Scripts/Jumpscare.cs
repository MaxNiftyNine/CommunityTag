using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{

public GameObject FlashyThing;

public GameObject AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){

     if (other.gameObject.CompareTag("HandTag"))
     {
    Invoke("scare", 0.0f);

    }
    }

    void scare(){


        FlashyThing.SetActive(true);
        AudioSource.SetActive(true);
        Invoke("QuitGame", 4.0f);


    }

     void QuitGame(){

    Application.Quit();

    }
}
