using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
using Photon.VR.Saving;


public class ChangeCosmetic : MonoBehaviour
{
    public string HandTag = "HandTag";
    public string Cosmetic;
    private bool Pressed = false;
    public Color PressedColor = Color.red;
    public Color UnpressedColor = Color.white;
    public enum CosmeticType
    {
        LeftHand,
        RightHand,
        Head,
        Face,
        Body,
        Model
    }

    private void Start() {
        
    }
    private void Update(){
        
    }
    public CosmeticType cosmeticType;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == HandTag) {
            if (Pressed){
                Pressed = false;
                GetComponent<Renderer>().material.color = UnpressedColor;
                switch(cosmeticType)
            {
                case CosmeticType.LeftHand:
                    PhotonVRManager.SetCosmetic("LeftHand", "");
                    break;
                case CosmeticType.RightHand:
                    PhotonVRManager.SetCosmetic("RightHand", "");
                    break;
                case CosmeticType.Head:
                    PhotonVRManager.SetCosmetic("Head", "");
                    break;
                case CosmeticType.Face:
                    PhotonVRManager.SetCosmetic("Face", "");
                    break;
                case CosmeticType.Body:
                    PhotonVRManager.SetCosmetic("Body", "");
                    break;
                case CosmeticType.Model:
                    PhotonVRManager.SetCosmetic("HeadModel", "");
                    PhotonVRManager.SetCosmetic("BodyModel", "");
                    break;
                default:
                    Debug.LogError("cosmetic error");
                    break;
            }
            }
            else{
                Pressed = true;
                GetComponent<Renderer>().material.color = PressedColor;
            switch(cosmeticType)
            {
                
                case CosmeticType.LeftHand:
                    PhotonVRManager.SetCosmetic("LeftHand", Cosmetic);
                    break;
                case CosmeticType.RightHand:
                    PhotonVRManager.SetCosmetic("RightHand", Cosmetic);
                    break;
                case CosmeticType.Head:
                    PhotonVRManager.SetCosmetic("Head", Cosmetic);
                    break;
                case CosmeticType.Face:
                    PhotonVRManager.SetCosmetic("Face", Cosmetic);
                    break;
                case CosmeticType.Body:
                    PhotonVRManager.SetCosmetic("Body", Cosmetic);
                    break;
                case CosmeticType.Model:
                    PhotonVRManager.SetCosmetic("HeadModel", Cosmetic);
                    PhotonVRManager.SetCosmetic("BodyModel", Cosmetic);
                    break;
                default:
                    Debug.LogError("cosmetic error");
                    break;
            }
            }
        }
    }
}
