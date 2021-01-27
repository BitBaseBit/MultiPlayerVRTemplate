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
    public bool isBeingHeld = false;

    public char hand;
    GameObject player;

    public static NetworkGrabbingBat Instance;

    GenericVRPlayerComponents components;

    GameObject leftParent;
    GameObject rightParent;
    public GameObject bat;

    public Transform leftTransform;
    public Transform rightTransform;

    bool isHovering = false;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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

    [PunRPC]
    public void StartNetworkGrabbing()
    {

        var grabInteractable = GetComponent<XRGrabInteractable>();
        string handEnter = grabInteractable.selectingInteractor.name;

        if (handEnter == "Right Base Controller")
        {
            hand = 'R';
        }
        else if (handEnter == "Left Base Controller")
        {
            hand = 'L';
        }

        isBeingHeld = true;
    }

    [PunRPC]
    public void StopNetworkGrabbing()
    {
        isBeingHeld = false;
    }
}
