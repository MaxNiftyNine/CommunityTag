using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    // hey max here, made this script for snowballerr but it might be useful here (note: not networked)
    public LayerMask[] HandLayers;
    public GameObject LeftHand;
    public GameObject RightHand;
    private bool HandIsNear;
    private GameObject ClosestHand;
    private Transform CurrentParent;
    private Rigidbody rb;
    private bool isGrabbed = false;
    private Vector3 lastHandPosition;
    private Vector3 handVelocity;

    private void Start()
    {
        CurrentParent = transform.parent;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandIsNear = false;
        foreach (LayerMask layer in HandLayers)
        {
            Collider[] NearHands = Physics.OverlapSphere(transform.position, 0.15f, layer);
            if (NearHands.Length > 0)
            {
                HandIsNear = true;
                ClosestHand = NearHands[0].gameObject;
                break;
            }
        }

        if (HandIsNear && !isGrabbed)
        {
            bool grabCondition =
                (ClosestHand == LeftHand && EasyInputs.GetGripButtonDown(EasyHand.LeftHand)) ||
                (ClosestHand == RightHand && EasyInputs.GetGripButtonDown(EasyHand.RightHand));

            if (grabCondition)
            {
                isGrabbed = true;
                if (rb != null)
                {
                    rb.isKinematic = true;

                    Physics.IgnoreCollision(ClosestHand.GetComponent<Collider>(), GetComponent<Collider>(), true);
                }
                transform.parent = ClosestHand.transform;
                lastHandPosition = ClosestHand.transform.position;
            }
        }
        else if (isGrabbed)
        {

            handVelocity = (ClosestHand.transform.position - lastHandPosition) / Time.deltaTime;
            lastHandPosition = ClosestHand.transform.position;

            bool releaseCondition =
                (ClosestHand == LeftHand && !EasyInputs.GetGripButtonDown(EasyHand.LeftHand)) ||
                (ClosestHand == RightHand && !EasyInputs.GetGripButtonDown(EasyHand.RightHand));

            if (releaseCondition)
            {
                isGrabbed = false;
                transform.parent = CurrentParent;
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.velocity = handVelocity;

                    Physics.IgnoreCollision(ClosestHand.GetComponent<Collider>(), GetComponent<Collider>(), false);
                }
            }
        }
        else
        {
            if (rb != null && !isGrabbed)
            {
                rb.isKinematic = false;
            }
            transform.parent = CurrentParent;
        }
    }
}