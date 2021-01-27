using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RightController_TableTennis : MonoBehaviourPun
{
    public GameObject rightController;
    PhotonView photonView;

    bool isRightControllerHidden = true;

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
            Debug.Log(NetworkGrabbingBat.hand);
            if (NetworkGrabbingBat.isBeingHeld)
            {
                if (NetworkGrabbingBat.hand == 'R')
                {
                    photonView.RPC("HideRightController", RpcTarget.AllBuffered);
                    isRightControllerHidden = true;
                }
                else if (NetworkGrabbingBat.hand == 'L')
                {
                    photonView.RPC("ShowRightController", RpcTarget.AllBuffered);
                    isRightControllerHidden = false;
                }
            }
            else if (!isRightControllerHidden)
            {
                photonView.RPC("HideRightController", RpcTarget.AllBuffered);
            }
        }
    }
    void HideRightController()
    {
        rightController.SetActive(false);
    }

    [PunRPC]
    void ShowRightHand()
    {
        rightController.SetActive(true);
    }
}
