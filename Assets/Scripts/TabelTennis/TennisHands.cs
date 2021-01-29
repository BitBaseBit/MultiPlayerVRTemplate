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
       if (TTBat1.hasSelected)
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
                    photonView.RPC("ShowRightController", RpcTarget.AllBuffered, 1);
                    break;
                case 'R':
                    //3
                    Debug.Log("Got to 3");
                    Debug.Log(TTBat1.bat1View);
                    Debug.Log(photonView);
                    photonView.RPC("ShowLeftController", RpcTarget.AllBuffered, 1);
                    break;
                default:
                    Debug.LogError("TTBat1.hand was neither L nor R");
                    break;
            }
       }
       else if (TTBat2.hasSelected)
       {
            // 4
            Debug.Log("Got to 4");
            switch (TTBat2.hand)
            {
                case 'L':
                    //5
                    Debug.Log("Got to 5");
                    photonView.RPC("ShowRightController", RpcTarget.AllBuffered, 2);
                    break;
                case 'R':
                    //6
                    Debug.Log("Got to 6");
                    photonView.RPC("ShowLeftController", RpcTarget.AllBuffered, 2);
                    break;
                default:
                    Debug.LogError("TTBat2.hand was neither L nor R");
                    break;
            }

       }
       else if (!TTBat1.hasSelected && TTBat1.bat1ViewInit &&
                !TTBat1.rightParent.transform.GetChild(0).gameObject.activeSelf)
       {
            //8
            Debug.Log("Got to 8");
            photonView.RPC("ShowHands", RpcTarget.AllBuffered, 1);
       }
       else if (!TTBat2.hasSelected && TTBat2.bat2ViewInit &&
                !TTBat2.rightParent.transform.GetChild(0).gameObject.activeSelf)
       {
            //8
            Debug.Log("Got to 8");
            photonView.RPC("ShowHands", RpcTarget.AllBuffered, 2);
       }

    }

    [PunRPC]
    public void ShowLeftController(int whichBat)        
    {
        switch (whichBat)
        { 
            case 1:
                TTBat1.leftParent.transform.GetChild(0).gameObject.SetActive(false);
                TTBat1.rightParent.transform.GetChild(0).gameObject.SetActive(false);

                TTBat1.rightParent.transform.GetChild(1).gameObject.SetActive(false);
                TTBat1.leftParent.transform.GetChild(1).gameObject.SetActive(true);
                break;

            case 2:
                TTBat2.leftParent.transform.GetChild(0).gameObject.SetActive(false);
                TTBat2.rightParent.transform.GetChild(0).gameObject.SetActive(false);

                TTBat2.rightParent.transform.GetChild(1).gameObject.SetActive(false);
                TTBat2.leftParent.transform.GetChild(1).gameObject.SetActive(true);
                break;

        }
        
    }

    [PunRPC]
    public void ShowRightController(int whichBat)
    {
        switch (whichBat)
        { 
            case 1:
                TTBat1.leftParent.transform.GetChild(0).gameObject.SetActive(false);
                TTBat1.rightParent.transform.GetChild(0).gameObject.SetActive(false);

                TTBat1.rightParent.transform.GetChild(1).gameObject.SetActive(true);
                TTBat1.leftParent.transform.GetChild(1).gameObject.SetActive(false);
                break;

            case 2:
                TTBat2.leftParent.transform.GetChild(0).gameObject.SetActive(false);
                TTBat2.rightParent.transform.GetChild(0).gameObject.SetActive(false);

                TTBat2.rightParent.transform.GetChild(1).gameObject.SetActive(true);
                TTBat2.leftParent.transform.GetChild(1).gameObject.SetActive(false);
                break;
        }

    }

    [PunRPC]
    public void ShowHands(int whichBat)
    {
        switch (whichBat)
        { 
            case 1:
                TTBat1.leftParent.transform.GetChild(0).gameObject.SetActive(true);
                TTBat1.rightParent.transform.GetChild(0).gameObject.SetActive(true);

                TTBat1.rightParent.transform.GetChild(1).gameObject.SetActive(false);
                TTBat1.leftParent.transform.GetChild(1).gameObject.SetActive(false);
                break;

            case 2:
                TTBat2.leftParent.transform.GetChild(0).gameObject.SetActive(true);
                TTBat2.rightParent.transform.GetChild(0).gameObject.SetActive(true);

                TTBat2.rightParent.transform.GetChild(1).gameObject.SetActive(false);
                TTBat2.leftParent.transform.GetChild(1).gameObject.SetActive(false);
                break;
        }
    }
}
