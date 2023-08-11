using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameKeyBoardLetter : MonoBehaviour
{
    public NameComputer CScript;
    private string Letter;
    public string HandTag;
    private Renderer rend;
    private Vector3 originalPosition;
    private Vector3 offset = new Vector3(0,-0.049f,0);

    public Material Pressed;
    public Material NotPressed;
    private bool pressed;

    private void Start() {
        Letter = gameObject.name;
        originalPosition = transform.localPosition;
    rend = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == HandTag && !pressed)
        {if (Letter == "DEL") {
                CScript.PlayerName = CScript.PlayerName.Substring(0, CScript.PlayerName.Length - 1);
            }
            else if(CScript.PlayerName.Length < 13){
            
            
            CScript.PlayerName += Letter;
            }
            StartCoroutine(MoveDownAndUp());
        }
    }

    IEnumerator MoveDownAndUp()
    {
        pressed = true;
        rend.material = Pressed;
        Vector3 targetPosition = originalPosition + offset;
        transform.localPosition = targetPosition;
        yield return new WaitForSeconds(0.75f);
        

        transform.localPosition = originalPosition;
        rend.material = NotPressed;
        pressed = false;
    }



    
}
