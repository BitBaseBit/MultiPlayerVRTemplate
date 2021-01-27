using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RightHand_TableTennis : MonoBehaviourPun
{
    public GameObject rightHand;
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
            if (NetworkGrabbingBat.Instance.isBeingHeld)
            {
                photonView.RPC("HideRightHand", RpcTarget.AllBuffered);
                isHandVisible = false;
            }
            else if (!NetworkGrabbingBat.Instance.isBeingHeld && !isHandVisible)
            {
                photonView.RPC("ShowRightHand", RpcTarget.AllBuffered);
            }
        }
        
    }

    [PunRPC]
    void HideRightHand()
    {
        rightHand.SetActive(false);
    }

    [PunRPC]
    void ShowRightHand()
    {
        rightHand.SetActive(true);
    }
}
