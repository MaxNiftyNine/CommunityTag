using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    /// Changes scene on collison with hand tag
    public string HandTag = "HandTag";
    public string URL;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == HandTag) {
            Application.OpenURL(URL);
        }
    }
    
}
