using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TennisHands : MonoBehaviourPunCallbacks
{

    public GameObject leftParent;
    public GameObject rightParent;

    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TTBat1.hasSelected && TTBat1.bat1View.IsMine)
        {
            //1 
            Debug.Log("Got to 1");
            switch (TTBat1.hand)
            {
                case 'L':
                    // 2
                    Debug.Log("Got to 2");
                    Debug.Log(TTBat1.bat1View);
                    Debug.Log(photonView);

                    leftParent.transform.GetChild(0).gameObject.SetActive(false);
                    rightParent.transform.GetChild(0).gameObject.SetActive(false);
                    rightParent.transform.GetChild(1).gameObject.SetActive(true);
                    leftParent.transform.GetChild(1).gameObject.SetActive(false);

                    photonView.RPC("ShowRightController", RpcTarget.AllBuffered);
                    break;
                case 'R':
                    //3
                    Debug.Log("Got to 3");
                    Debug.Log(TTBat1.bat1View);
                    Debug.Log(photonView);

                    leftParent.transform.GetChild(0).gameObject.SetActive(false);
                    rightParent.transform.GetChild(0).gameObject.SetActive(false);
                    rightParent.transform.GetChild(1).gameObject.SetActive(false);
                    leftParent.transform.GetChild(1).gameObject.SetActive(true);

                    photonView.RPC("ShowLeftController", RpcTarget.AllBuffered);
                    break;
                default:
                    Debug.LogError("TTBat1.hand was neither L nor R");
                    break;
            }
        }
        else if (TTBat2.hasSelected && TTBat2.bat2View.IsMine)
        {
            // 4
            Debug.Log("Got to 4");
            switch (TTBat2.hand)
            {
                case 'L':
                    //5
                    Debug.Log("Got to 5");
                    photonView.RPC("ShowRightController", RpcTarget.AllBuffered);
                    break;
                case 'R':
                    //6
                    Debug.Log("Got to 6");
                    photonView.RPC("ShowLeftController", RpcTarget.AllBuffered);
                    break;
                default:
                    Debug.LogError("TTBat2.hand was neither L nor R");
                    break;
            }
        }
        else if (!TTBat2.hasSelected && TTBat2.bat2ViewInit && !leftParent.transform.GetChild(0).gameObject.activeSelf &&
                 TTBat2.bat2View.IsMine)
        {
            //7
            Debug.Log("Got to 7");
            photonView.RPC("ShowHands", RpcTarget.AllBuffered);
        }
        else if (!TTBat1.hasSelected && TTBat1.bat1ViewInit && !leftParent.transform.GetChild(0).gameObject.activeSelf &&
                 TTBat1.bat1View.IsMine)
        {
            //8
            Debug.Log("Got to 8");

            photonView.RPC("ShowHands", RpcTarget.AllBuffered);
        }

    }

    [PunRPC]
    public void ShowLeftController()
    {
        PhotonView myView = GetComponent<PhotonView>();
        if (!myView.IsMine)
        {
            leftParent.transform.GetChild(0).gameObject.SetActive(false);
            rightParent.transform.GetChild(0).gameObject.SetActive(false);

            rightParent.transform.GetChild(1).gameObject.SetActive(false);
            leftParent.transform.GetChild(1).gameObject.SetActive(true);
        }    
    }

    [PunRPC]
    public void ShowRightController()
    {
        PhotonView myView = GetComponent<PhotonView>();
        if (!myView.IsMine)
        {
            leftParent.transform.GetChild(0).gameObject.SetActive(false);
            rightParent.transform.GetChild(0).gameObject.SetActive(false);

            rightParent.transform.GetChild(1).gameObject.SetActive(true);
            leftParent.transform.GetChild(1).gameObject.SetActive(false);
        }    
        //9
        Debug.Log("Got to 9");
    }

    [PunRPC]
    public void ShowHands()
    {
        leftParent.transform.GetChild(0).gameObject.SetActive(true);
        rightParent.transform.GetChild(0).gameObject.SetActive(true);

        rightParent.transform.GetChild(1).gameObject.SetActive(false);
        leftParent.transform.GetChild(1).gameObject.SetActive(false);
    }
}
