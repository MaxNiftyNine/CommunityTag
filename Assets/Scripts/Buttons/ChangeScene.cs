using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    /// Changes scene on collison with hand tag
    public string HandTag = "HandTag";
    public string SceneName;
    public bool KeepRig; //if you dont know what this is then leave it false

    private void OnTriggerEnter(Collider other) {
        if(other.tag == HandTag) {
            GameObject rig = FindObjectOfType<Unity.XR.CoreUtils.XROrigin>().gameObject;
            if (KeepRig) {
                DontDestroyOnLoad(rig);
            }
            else {
                Destroy(rig);
            }
            SceneManager.LoadScene(SceneName);
        }
    }
    
}
