using UnityEngine;
using Photon.Pun;
using UnityEngine.XR;
using System.Collections;
namespace Photon.VR
{
    public class PhotonVRObject : MonoBehaviourPunCallbacks
    {
        public string PrefabPath;
        public float GrabRadius = 0.15f;
        public Transform LeftHandAnchor;
        public Transform RightHandAnchor;
        private GameObject prefab;
        private GameObject InstantiatedPrefab;
        private Rigidbody instantiatedPrefabRigidbody;
        private PhotonView instantiatedPrefabPhotonView;
        private bool isBeingHeld = false;
        private bool PrefabFound = false;
        private Vector3 LeftlastPosition;
        private Vector3 RightlastPosition;
        private float timeDelta;
        private Vector3 Leftvelocity;
        private Vector3 Rightvelocity;
        private void Start()
        {
            LeftlastPosition = LeftHandAnchor.position;
            RightlastPosition = RightHandAnchor.position;
            timeDelta = Time.deltaTime;
            prefab = Resources.Load<GameObject>(PrefabPath);
        }

        private void Update()
        {
            if (!PrefabFound) return;
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
            else if (instantiatedPrefabPhotonView.IsMine)
            {
                if (instantiatedPrefabRigidbody != null)
                {
                    instantiatedPrefabRigidbody.isKinematic = false;
                }
                InstantiatedPrefab.transform.parent = null;
                if (isBeingHeld && nearHand == HandType.RightHand)
                {
                    instantiatedPrefabRigidbody.velocity = Rightvelocity;
                }
                else if (isBeingHeld && nearHand == HandType.LeftHand)
                {
                    instantiatedPrefabRigidbody.velocity = Leftvelocity;
                }
                Collider prefabcol = InstantiatedPrefab.GetComponent<Collider>();
                if (prefabcol){
                    prefabcol.enabled = true;
                }
                isBeingHeld = false;
            }
            else
            {
                instantiatedPrefabRigidbody.isKinematic = true;
            }
        }


        private void HandleGrab(Transform handAnchor)
        {
            if (instantiatedPrefabPhotonView.IsMine)
            {
                Collider prefabcol = InstantiatedPrefab.GetComponent<Collider>();
                if (prefabcol){
                    prefabcol.enabled = false;
                }
                if (instantiatedPrefabRigidbody != null)
                {
                    instantiatedPrefabRigidbody.isKinematic = true;
                }
                InstantiatedPrefab.transform.parent = handAnchor;
                InstantiatedPrefab.transform.localPosition = Vector3.zero;
                InstantiatedPrefab.transform.localRotation = Quaternion.identity;
            }
            else
            {
                instantiatedPrefabPhotonView.RequestOwnership();
            }
        }

        private HandType NearHand(float grabrad)
        {
            float rightHandDistance = Vector3.Distance(RightHandAnchor.position, InstantiatedPrefab.transform.position);
            float leftHandDistance = Vector3.Distance(LeftHandAnchor.position, InstantiatedPrefab.transform.position);

            if (rightHandDistance <= grabrad) return HandType.RightHand;
            if (leftHandDistance <= grabrad) return HandType.LeftHand;

            return HandType.None;
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                InstantiatedPrefab = PhotonNetwork.InstantiateRoomObject(PrefabPath, transform.position, transform.rotation);
                instantiatedPrefabRigidbody = InstantiatedPrefab.GetComponent<Rigidbody>();
                instantiatedPrefabPhotonView = InstantiatedPrefab.GetComponent<PhotonView>();
                PrefabFound = true;
            }
            else
            {
                StartCoroutine(WaitForInstantiatedPrefab());
            }
        }

        private IEnumerator WaitForInstantiatedPrefab()
        {
            while (InstantiatedPrefab == null)
            {
                InstantiatedPrefab = GameObject.FindWithTag(prefab.tag);
                yield return null;
            }

            instantiatedPrefabRigidbody = InstantiatedPrefab.GetComponent<Rigidbody>();
            instantiatedPrefabPhotonView = InstantiatedPrefab.GetComponent<PhotonView>();
            PrefabFound = true;
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
}
