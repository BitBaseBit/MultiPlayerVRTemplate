using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandSynchronization : MonoBehaviour, IPunObservable
{

    public Transform leftHandTransform;
    private PhotonView photonView;

    private float distanceLeftHand;
    private Vector3 directionLeftHand;
    private Vector3 networkPositionLeftHand;
    private Vector3 storedPositionLeftHand;

    private Quaternion networkRotationLeftHand;
    private float angleLeftHand;

    bool firstTake = false;

    private void OnEnable()
    {
        firstTake = true;
    }
    private void Awake()
    {
        photonView.GetComponent<PhotonView>();
        storedPositionLeftHand = leftHandTransform.localPosition;
        networkPositionLeftHand = Vector3.zero;
        networkRotationLeftHand = Quaternion.identity;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!photonView.IsMine)
        {
            leftHandTransform.localPosition = Vector3.MoveTowards(leftHandTransform.localPosition,
                                                                  networkPositionLeftHand,
                                                                  distanceLeftHand * (1 / PhotonNetwork.SerializationRate));

            leftHandTransform.localRotation = Quaternion.RotateTowards(leftHandTransform.localRotation,
                                                                       networkRotationLeftHand,
                                                                       angleLeftHand * (1 / PhotonNetwork.SerializationRate));
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // The player is me
            directionLeftHand = leftHandTransform.localPosition - storedPositionLeftHand;
            storedPositionLeftHand = leftHandTransform.localPosition;

            stream.SendNext(leftHandTransform.localPosition);
            stream.SendNext(directionLeftHand);
            stream.SendNext(leftHandTransform.localRotation);
        }
        else 
        {
            // What I look like to other players
            // Now lets get the data that we sent

            networkPositionLeftHand = (Vector3)stream.ReceiveNext();
            directionLeftHand = (Vector3)stream.ReceiveNext();

            if (firstTake)
            {
                leftHandTransform.localPosition = networkPositionLeftHand;
                distanceLeftHand = 0;
            }
            else
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                networkPositionLeftHand += directionLeftHand * lag;
                distanceLeftHand = Vector3.Distance(leftHandTransform.localPosition , networkPositionLeftHand);
            }

            // Get left hand rotation data
            networkRotationLeftHand = (Quaternion)stream.ReceiveNext(); 

            if (firstTake)
            {
                angleLeftHand = 0;
                leftHandTransform.localRotation = networkRotationLeftHand;
            }
            else
            {
                angleLeftHand = Quaternion.Angle(leftHandTransform.localRotation, networkRotationLeftHand);
            }

            if (firstTake)
            {
                firstTake = false;
            }    
        }
    }
}
