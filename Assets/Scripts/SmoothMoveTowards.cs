using UnityEngine;

public class SmoothMoveTowards : MonoBehaviour
{
    public Transform target; 
    public float Speed = 1.0f;

    void Update()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Speed * Time.deltaTime);

            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Speed * Time.deltaTime);
        }
    }
}
