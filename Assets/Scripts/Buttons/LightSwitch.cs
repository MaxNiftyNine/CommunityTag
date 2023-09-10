using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private bool on = false;
    public Transform OnState;
    public Transform OffState;
    public GameObject Light;
    public GameObject OffLight;
    public string HandTag = "HandTag";
    private bool canToggle = true;
    public float toggleCooldown = 0.1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == HandTag && canToggle)
        {
            ToggleLight();
            StartCoroutine(ToggleCooldown());
        }
    }

    private void ToggleLight()
    {
        on = !on;
    }

    private IEnumerator ToggleCooldown()
    {
        canToggle = false;
        yield return new WaitForSeconds(toggleCooldown);
        canToggle = true;
    }

    private void Update()
    {
        if (!on)
        {
            Light.SetActive(false);
            transform.rotation = OffState.rotation;
            transform.position = OffState.position;
            OffLight.SetActive(true);
        }
        else
        {
            Light.SetActive(true);
            transform.rotation = OnState.rotation;
            transform.position = OnState.position;
            OffLight.SetActive(false);
        }
    }
}
