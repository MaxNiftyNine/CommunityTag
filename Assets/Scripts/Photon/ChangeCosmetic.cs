using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
using Photon.VR.Saving;
using TMPro;

public class ChangeCosmetic : MonoBehaviour
{
    public string HandTag = "HandTag";
    public string Cosmetic;
    private bool Pressed = false;
    public Color PressedColor = Color.red;
    public Color UnpressedColor = Color.white;

    public TextMeshPro EnabledText;

    public enum CosmeticType
    {
        LeftHand,
        RightHand,
        Head,
        Face,
        Body,
        HeadModel,
        BodyModel,
        BothModels
    }

    public CosmeticType cosmeticType;
   
    private void Update(){
        if (cosmeticType != CosmeticType.BothModels){
        if (PhotonVRManager.GetCosmetic(cosmeticType.ToString()) == Cosmetic){
            
            Pressed = true;
        }
        else{
            Pressed = false;
        }}
        else {
            if (PhotonVRManager.GetCosmetic("HeadModel") == Cosmetic || PhotonVRManager.GetCosmetic("BodyModel") == Cosmetic){
            
            Pressed = true;
        }
        else{
            Pressed = false;
        }
        }
        if (Pressed){
            EnabledText.text = "Disable";
            GetComponent<Renderer>().material.color = PressedColor;
        }
        else{
            EnabledText.text = "Enable";
            GetComponent<Renderer>().material.color = UnpressedColor;
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == HandTag) {
            if (Pressed){
                Pressed = false;
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
                case CosmeticType.HeadModel:
                    PhotonVRManager.SetCosmetic("HeadModel", "");
                    break;
                case CosmeticType.BodyModel:
                    PhotonVRManager.SetCosmetic("BodyModel", "");
                    break;
                
                case CosmeticType.BothModels:
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
                case CosmeticType.HeadModel:
                    PhotonVRManager.SetCosmetic("HeadModel", Cosmetic);
                    break;
                case CosmeticType.BodyModel:
                    PhotonVRManager.SetCosmetic("BodyModel", Cosmetic);
                    break;
                
                case CosmeticType.BothModels:
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
