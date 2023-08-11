using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    /// Changes scene on collison with hand tag
    public string HandTag = "HandTag";
    public string SceneName;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == HandTag) {
            SceneManager.LoadScene(SceneName);
        }
    }
    
}
