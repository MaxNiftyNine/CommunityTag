using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;

public class ChangeCosmetic : MonoBehaviour
{
    public string HandTag = "HandTag";
    public string Cosmetic;

    public enum CosmeticType
    {
        LeftHand,
        RightHand,
        Head,
        Face,
        Body,
        Model
    }

    public CosmeticType cosmeticType;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == HandTag) {
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
