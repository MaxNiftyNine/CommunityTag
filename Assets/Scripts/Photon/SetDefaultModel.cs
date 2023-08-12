using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
using Photon.Pun;

public class SetDefaultModel : MonoBehaviour
{
    // if all childs are disabled, enable the one with this name
    public string ModelName;
    public PhotonView photonView;
    void Update()
    {
        bool allChildrenDisabled = true;

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                allChildrenDisabled = false;
                break;
            }
        }

        if (allChildrenDisabled)
        {
            if (photonView.IsMine){
                PhotonVRManager.SetCosmetic("HeadModel", ModelName);
                PhotonVRManager.SetCosmetic("BodyModel", ModelName);
            }
        }
    }

}