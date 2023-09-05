using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    //spins the skybox, you spin me right round baby right round like a record baby right round right round
    public float minRotationSpeed = 1f;   
    public float maxRotationSpeed = 5f;   

    private float rotationSpeed;          
    private float currentRotation = 0f;   

    void Start()
    {

        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    void Update()
    {

        currentRotation += rotationSpeed * Time.deltaTime;
        currentRotation %= 360f;  

        RenderSettings.skybox.SetFloat("_Rotation", currentRotation);

        DynamicGI.UpdateEnvironment();
    }
}