using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GorillaLocomotion;
using easyInputs;
public class NoClip : MonoBehaviour
{
    private bool Leftplaced;
    private bool Rightplaced;
    private GameObject RightPlatform;
    private GameObject LeftPlatform;
    private Transform Left;
    private Transform Right;
    public Player Gorilla;
    private List<MeshCollider> meshColliders;
    // Start is called before the first frame update
    void Start()
    {
        RightPlatform = new GameObject("RightPlatform",typeof(BoxCollider));
        LeftPlatform = new GameObject("LeftPlatform",typeof(BoxCollider));
        RightPlatform.GetComponent<BoxCollider>().size = new Vector3 (0.15f, 0.1f, 0.15f);
        LeftPlatform.GetComponent<BoxCollider>().size = new Vector3 (0.15f, 0.1f, 0.15f);
        Left = Gorilla.leftHandTransform;
        Right = Gorilla.rightHandTransform;
        RightPlatform.SetActive(false);
        LeftPlatform.SetActive(false);



        meshColliders = new List<MeshCollider>();

        foreach (MeshCollider collider in FindObjectsOfType<MeshCollider>())
        {
            if (collider.enabled)
            {
                meshColliders.Add(collider);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EasyInputs.GetGripButtonDown(EasyHand.RightHand)){
            if (!Rightplaced){
                RightPlatform.SetActive(true);
                RightPlatform.transform.position = Right.position + new Vector3 (0f,-0.15f,0f);
                Rightplaced = true;
            }

        }
        else {
                RightPlatform.SetActive(false);
                Rightplaced = false;
        }
        if (EasyInputs.GetGripButtonDown(EasyHand.LeftHand)){
            if (!Leftplaced){
                LeftPlatform.SetActive(true);
                LeftPlatform.transform.position = Left.position + new Vector3 (0f,-0.15f,0f);
                Leftplaced = true;
            }

        }
        else {
                LeftPlatform.SetActive(false);
                Leftplaced = false;
        }

        if (EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand)){
            foreach (MeshCollider collider in meshColliders)
        {
            collider.enabled = false;
            
        }
        }
        else {
            foreach (MeshCollider collider in meshColliders)
        {
            collider.enabled = true;
            
        }
        
        }
    }
}
