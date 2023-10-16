using UnityEngine;
using UnityEngine.XR;
using System.Collections;

    public class CDScript : MonoBehaviour
    {
        public float GrabRadius = 0.15f;
        public Transform LeftHandAnchor;
        public Transform RightHandAnchor;
        private Rigidbody rb;
        private bool isBeingHeld = false;
        private Vector3 LeftlastPosition;
        private Vector3 RightlastPosition;
        private float timeDelta;
        private Vector3 Leftvelocity;
        private Vector3 Rightvelocity;

        public Transform DriveAnchor;

        private void Start()
        {
            LeftlastPosition = LeftHandAnchor.position;
            RightlastPosition = RightHandAnchor.position;
            timeDelta = Time.deltaTime;
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 LeftcurrentPosition = LeftHandAnchor.position;
            Vector3 RightcurrentPosition = RightHandAnchor.position;
            Vector3 Leftdisplacement = LeftcurrentPosition - LeftlastPosition;
            Vector3 Rightdisplacement = RightcurrentPosition - RightlastPosition;
            Leftvelocity = Leftdisplacement / timeDelta;
            Rightvelocity = Rightdisplacement / timeDelta;

            LeftlastPosition = LeftcurrentPosition;
            RightlastPosition = RightcurrentPosition;
            timeDelta = Time.deltaTime;
            HandType nearHand = NearHand(GrabRadius);

            GetInputBool(HandType.RightHand, CommonUsages.gripButton, out bool RightButtonDown);
            GetInputBool(HandType.LeftHand, CommonUsages.gripButton, out bool LeftButtonDown);

            if ((nearHand == HandType.RightHand && RightButtonDown) || (isBeingHeld && RightButtonDown))
            {
                HandleGrab(RightHandAnchor);
                isBeingHeld = true;
            }
            else if ((nearHand == HandType.LeftHand && LeftButtonDown) || (isBeingHeld && LeftButtonDown))
            {
                HandleGrab(LeftHandAnchor);
                isBeingHeld = true;
            }
            else if (isBeingHeld)
            {
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
                transform.parent = null;
                if (nearHand == HandType.RightHand)
                {
                    rb.velocity = Rightvelocity;
                }
                else if (nearHand == HandType.LeftHand)
                {
                    rb.velocity = Leftvelocity;
                }
                Collider col = GetComponent<Collider>();
                if (col)
                {
                    col.enabled = true;
                }
                isBeingHeld = false;
            }
            else
            {
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
        }

        private void HandleGrab(Transform handAnchor)
        {
            Collider col = GetComponent<Collider>();
            if (col)
            {
                col.enabled = false;
            }

            if (rb != null)
            {
                rb.isKinematic = true;
            }

            transform.parent = handAnchor;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        private HandType NearHand(float grabrad)
        {
            float rightHandDistance = Vector3.Distance(RightHandAnchor.position, transform.position);
            float leftHandDistance = Vector3.Distance(LeftHandAnchor.position, transform.position);

            if (rightHandDistance <= grabrad) return HandType.RightHand;
            if (leftHandDistance <= grabrad) return HandType.LeftHand;

            return HandType.None;
        }

        static bool GetInputBool(HandType hand, InputFeatureUsage<bool> usage, out bool value)
        {
            XRNode node = hand == HandType.LeftHand ? XRNode.LeftHand : XRNode.RightHand;
            InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(usage, out value);
            return value;
        }

        public enum HandType
        {
            LeftHand,
            RightHand,
            None
        }
    }
