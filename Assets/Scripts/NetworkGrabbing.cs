using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkGrabbing : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{

    PhotonView photonView;
    Rigidbody rb;
    public bool isBeingHeld = false;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
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
        isBeingHeld = true;
    }

    [PunRPC]
    public void StopNetworkGrabbing()
    {
        isBeingHeld = false;
    }
}
