using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Photon.Realtime;

public class NetworkGrabbingBat : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{

    PhotonView photonView;
    Rigidbody rb;

    public static char hand;
    GameObject player;


    GenericVRPlayerComponents components;

    GameObject leftParent;
    GameObject rightParent;
    public GameObject bat;

    public Transform leftTransform;
    public Transform rightTransform;

    bool isHovering = false;

    public bool isBeingHeld;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>(); 
        leftParent = GameObject.FindGameObjectWithTag("leftHand");
        rightParent = GameObject.FindGameObjectWithTag("rightHand");
    }

    // Update is called once per frame
    void Update()
    {
        if (isHovering)
        {
            var distanceRightFromBat = Vector3.Distance(rightParent.transform.position, bat.transform.position); ;
            var distanceLeftFromBat = Vector3.Distance(leftParent.transform.position, bat.transform.position);

            var grabInteractable = GetComponent<XRGrabInteractable>();

            if (distanceRightFromBat < distanceLeftFromBat)
            {
                grabInteractable.attachTransform = rightTransform;
            }
            else if (distanceRightFromBat > distanceLeftFromBat)
            {
                grabInteractable.attachTransform = leftTransform;
            }
        }
        if (isBeingHeld)
        {
            rb.isKinematic = true;
            gameObject.layer = 13;
        }
        else
        {
            rb.isKinematic = false;
            gameObject.layer = 8;
        }
    }

    [PunRPC]
    public void StartNetworkGrabbing()
    {
        isBeingHeld = true;
    }

    [PunRPC]
    public void StopNetworkGrabbing()
    {
        isBeingHeld = false;
    }
    public void OnHoverEnter()
    {
        isHovering = true;
    }

    public void OnHoverExited()
    {
        isHovering = false;
    }

    private void TransferOwnerShip()
    {
        photonView.RequestOwnership();
    }

    public void OnSelectEnter()
    {
        photonView.RPC("StartNetworkGrabbing", RpcTarget.AllBuffered);
        if (!(photonView.Owner == PhotonNetwork.LocalPlayer))
        {
            TransferOwnerShip();
        }
    }

    public void OnSelectExit()
    {
        photonView.RPC("StopNetworkGrabbing", RpcTarget.AllBuffered);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != photonView)
        {
            return;
        }
        photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
    }

}
