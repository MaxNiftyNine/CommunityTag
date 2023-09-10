using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    //disables object 😯
    public GameObject Object;

    void Update()
    {
        Object.SetActive(false);
    }
}
