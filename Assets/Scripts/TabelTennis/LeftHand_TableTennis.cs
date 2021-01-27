using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeftHand_TableTennis : MonoBehaviourPun
{
    public GameObject leftHand;
    PhotonView photonView;
    bool isHandVisible = true;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (NetworkGrabbingBat.isBeingHeld)
            {
                photonView.RPC("HideLeftHand", RpcTarget.AllBuffered);
                isHandVisible = false;
            }
            else if (!NetworkGrabbingBat.isBeingHeld && !isHandVisible)
            {
                photonView.RPC("ShowLeftHand", RpcTarget.AllBuffered);
            }
        }


    }

    [PunRPC]
    void HideLeftHand()
    {
        leftHand.SetActive(false);
    }

    [PunRPC]
    void ShowLeftHand()
    {
        leftHand.SetActive(true);
    }
}
