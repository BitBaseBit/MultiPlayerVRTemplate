using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeftController_TableTennis : MonoBehaviourPun
{
    public GameObject leftController;
    PhotonView photonView;

    bool isLeftControllerHidden = true;

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
                if (NetworkGrabbingBat.hand == 'R')
                {
                    photonView.RPC("ShowLeftController", RpcTarget.AllBuffered);
                    isLeftControllerHidden = false;
                }
                else if (NetworkGrabbingBat.hand == 'L')
                {
                    photonView.RPC("HideLeftController", RpcTarget.AllBuffered);
                    isLeftControllerHidden = true;
                }
            }
            else if (!isLeftControllerHidden)
            {
                photonView.RPC("HideLeftController", RpcTarget.AllBuffered);
            }

        }
    }

    [PunRPC]
    void HideLeftController()
    {
        leftController.SetActive(false);
    }

    [PunRPC]
    void ShowLeftController()
    {
        leftController.SetActive(true);
    }
}
