using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.VR;
public class NameComputer : MonoBehaviour
{
    public string PlayerName;
    private string PN;
    public TextMeshPro NameText;
    void Start()
    {
        PlayerName = PlayerPrefs.GetString("Name", "Player" + Random.Range(1000,9999).ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerName == null){
            PhotonVRManager.SetUsername("Player" + Random.Range(1000,9999).ToString());
            PlayerPrefs.SetString("Name", "Player" + Random.Range(1000,9999).ToString());
        }
        if (PN != PlayerName){
            PhotonVRManager.SetUsername(PlayerName);
            PlayerPrefs.SetString("Name", PlayerName);
            PN = PlayerName;
            
        }
        NameText.text = PlayerName;
    }
}
