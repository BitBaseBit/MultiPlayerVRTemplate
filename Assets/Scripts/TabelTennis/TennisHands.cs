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

                    //leftParent.transform.GetChild(0).gameObject.SetActive(false);
                    //rightParent.transform.GetChild(0).gameObject.SetActive(false);
                    //rightParent.transform.GetChild(1).gameObject.SetActive(true);
                    //leftParent.transform.GetChild(1).gameObject.SetActive(false);

                    photonView.RPC("ShowRightController", RpcTarget.AllBuffered, 1);
                    TTBat1.handsVisible = false;
                    break;
                case 'R':
                    //3
                    Debug.Log("Got to 3");
                    Debug.Log(TTBat1.bat1View);
                    Debug.Log(photonView);

                    //leftParent.transform.GetChild(0).gameObject.SetActive(false);
                    //rightParent.transform.GetChild(0).gameObject.SetActive(false);
                    //rightParent.transform.GetChild(1).gameObject.SetActive(false);
                    //leftParent.transform.GetChild(1).gameObject.SetActive(true);

                    photonView.RPC("ShowLeftController", RpcTarget.AllBuffered, 1);
                    TTBat1.handsVisible = false;
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
                    photonView.RPC("ShowRightController", RpcTarget.AllBuffered, 2);
                    TTBat2.handsVisible = false;
                    break;
                case 'R':
                    //6
                    Debug.Log("Got to 6");
                    photonView.RPC("ShowLeftController", RpcTarget.AllBuffered,2);
                    TTBat2.handsVisible = false;
                    break;
                default:
                    Debug.LogError("TTBat2.hand was neither L nor R");
                    break;
            }
        }
        else if (!TTBat2.handsVisible && !TTBat2.hasSelected && TTBat2.bat2ViewInit  &&
                 TTBat2.bat2View.IsMine)
        {
            //7
            Debug.Log("Got to 7");
            photonView.RPC("ShowHands", RpcTarget.AllBuffered, 2);
            TTBat2.handsVisible = true;
        }
        else if (!TTBat1.handsVisible && !TTBat1.hasSelected && TTBat1.bat1ViewInit && 
                 TTBat1.bat1View.IsMine)
        {
            //8
            Debug.Log("Got to 8");

            photonView.RPC("ShowHands", RpcTarget.AllBuffered, 1);
            TTBat1.handsVisible = true;
        }

    }

    [PunRPC]
    public void ShowLeftController(int batID)
    {
        switch (batID)
        {
            case 1:
                TTBat1.leftParent.transform.GetChild(0).gameObject.SetActive(false);
                TTBat1.rightParent.transform.GetChild(0).gameObject.SetActive(false);

                TTBat1.rightParent.transform.GetChild(1).gameObject.SetActive(false);
                TTBat1.leftParent.transform.GetChild(1).gameObject.SetActive(true);
                //9
                Debug.Log("Got to 9");
                break;
            case 2:
                TTBat2.leftParent.transform.GetChild(0).gameObject.SetActive(false);
                TTBat2.rightParent.transform.GetChild(0).gameObject.SetActive(false);

                TTBat2.rightParent.transform.GetChild(1).gameObject.SetActive(true);
                TTBat2.leftParent.transform.GetChild(1).gameObject.SetActive(false);
                break;
            default:
                Debug.LogError("Something went wrong in ShowLeftController,  batID was neither 1 nor 2");
                break;
        }
       
    }

    [PunRPC]
    public void ShowRightController(int batID)
    {
        switch (batID)
        {
            case 1:
                TTBat1.leftParent.transform.GetChild(0).gameObject.SetActive(false);
                TTBat1.rightParent.transform.GetChild(0).gameObject.SetActive(false);

                TTBat1.rightParent.transform.GetChild(1).gameObject.SetActive(true);
                TTBat1.leftParent.transform.GetChild(1).gameObject.SetActive(false);
                //9
                Debug.Log("Got to 9");
                break;
            case 2:
                TTBat2.leftParent.transform.GetChild(0).gameObject.SetActive(false);
                TTBat2.rightParent.transform.GetChild(0).gameObject.SetActive(false);

                TTBat2.rightParent.transform.GetChild(1).gameObject.SetActive(true);
                TTBat2.leftParent.transform.GetChild(1).gameObject.SetActive(false);
                break;
            default:
                Debug.LogError("Something went wrong in ShowRightController,  batID was neither 1 nor 2");
                break;
        }
    }

    [PunRPC]
    public void ShowHands(int batID)
    {
        switch (batID)
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
            default:
                Debug.LogError("Something went wrong in ShowHands, batID was neither 1 nor 2");
                break;
        }
        
    }
}
