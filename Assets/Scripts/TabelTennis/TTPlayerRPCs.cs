using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class TTPlayerRPCs : MonoBehaviourPunCallbacks
{
    XRBaseInteractor grabInteractor;
    PhotonView batView;
    XRGrabInteractable batInteractable;

    GameObject genericVRPlayerGameObj;
    GameObject leftParent;
    GameObject rightParent;
    bool isBeingHeld;

    PhotonView photonView;

    GameObject bat;
    // Start is called before the first frame update
    void Start()
    {
        photonView = this.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSelectEnter()
    {
        isBeingHeld = true;


        if (this.gameObject.CompareTag("bat1") && photonView.IsMine)
        {
            //gameobject.findgameobjectswithtag("bat1");
            //var grabinteractable = getcomponent<xrgrabinteractable>();
            //batview = grabinteractable.selectinginteractor.transform.root.gameobject.getcomponent<photonview>();
            photonView.RPC("ShowController", RpcTarget.AllBuffered, 1);
        }
        else if (this.gameObject.CompareTag("bat2") && photonView.IsMine)
        {
            photonView.RPC("ShowController", RpcTarget.AllBuffered, 2);
        }
    }

    public void OnSelectExit()
    {
        isBeingHeld = false;

        if (this.gameObject.CompareTag("bat1") && photonView.IsMine)
        {
            photonView.RPC("ShowHands", RpcTarget.AllBuffered, 1);
        }
        else if (this.gameObject.CompareTag("bat2") && photonView.IsMine)
        {
            photonView.RPC("ShowHands", RpcTarget.AllBuffered, 2);
        }
    }

    [PunRPC]
    public void ShowController(int batTag)
    {
        switch (batTag)
        {
            case 1:
                bat = GameObject.FindGameObjectWithTag("bat1");
                batInteractable = bat.GetComponent<XRGrabInteractable>();
                grabInteractor = batInteractable.selectingInteractor;
        
                // Person who is selecting the bat


                genericVRPlayerGameObj = grabInteractor.gameObject.transform.root.gameObject;
                leftParent = genericVRPlayerGameObj.transform.GetChild(2).GetChild(1).gameObject;
                rightParent = genericVRPlayerGameObj.transform.GetChild(2).GetChild(2).gameObject;

                //grabInteractor.selectingInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
                //grabInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
                //root.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<XRDirectInteractor>().interactionLayerMask = LayerMask.GetMask("InHand");
        
                string handEnter1 = grabInteractor.name;
        
                if (handEnter1 == "Right Base Controller")
                {
                    leftParent.transform.GetChild(0).gameObject.SetActive(false);
                    rightParent.transform.GetChild(0).gameObject.SetActive(false);
        
                    rightParent.transform.GetChild(1).gameObject.SetActive(false);
                    leftParent.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (handEnter1 == "Left Base Controller")
                {
                    leftParent.transform.GetChild(0).gameObject.SetActive(false);
                    rightParent.transform.GetChild(0).gameObject.SetActive(false);
        
                    rightParent.transform.GetChild(1).gameObject.SetActive(true);
                    leftParent.transform.GetChild(1).gameObject.SetActive(false);
                }
                break;

            case 2:
                bat = GameObject.FindGameObjectWithTag("bat2");
                batInteractable = bat.GetComponent<XRGrabInteractable>();
                grabInteractor = batInteractable.selectingInteractor;
        
                // Person who is selecting the bat

                genericVRPlayerGameObj = grabInteractor.gameObject.transform.root.gameObject;
                leftParent = genericVRPlayerGameObj.transform.GetChild(2).GetChild(1).gameObject;
                rightParent = genericVRPlayerGameObj.transform.GetChild(2).GetChild(2).gameObject;

                //grabInteractor.selectingInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
                //grabInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
                //root.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<XRDirectInteractor>().interactionLayerMask = LayerMask.GetMask("InHand");
        
                string handEnter = grabInteractor.name;
        
                if (handEnter == "Right Base Controller")
                {
                    leftParent.transform.GetChild(0).gameObject.SetActive(false);
                    rightParent.transform.GetChild(0).gameObject.SetActive(false);
        
                    rightParent.transform.GetChild(1).gameObject.SetActive(false);
                    leftParent.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (handEnter == "Left Base Controller")
                {
                    leftParent.transform.GetChild(0).gameObject.SetActive(false);
                    rightParent.transform.GetChild(0).gameObject.SetActive(false);
        
                    rightParent.transform.GetChild(1).gameObject.SetActive(true);
                    leftParent.transform.GetChild(1).gameObject.SetActive(false);
                }
                break;
        }
    }

    [PunRPC]
    public void ShowHands(int batTag)
    {

        switch (batTag)
        {
            case 1:
                Debug.Log(grabInteractor.name);
        
                // Person who is selecting the bat
                //batView = grabInteractor.selectTarget.transform.root.gameObject.GetComponent<PhotonView>();


                //grabInteractor.selectingInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
                //grabInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
                //root.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<XRDirectInteractor>().interactionLayerMask = LayerMask.GetMask("InHand");
        
                leftParent.transform.GetChild(0).gameObject.SetActive(true);
                rightParent.transform.GetChild(0).gameObject.SetActive(true);
    
                rightParent.transform.GetChild(1).gameObject.SetActive(false);
                leftParent.transform.GetChild(1).gameObject.SetActive(false);
            
                break;

            case 2:
                Debug.Log(grabInteractor.name);
        
                // Person who is selecting the bat
                //batView = grabInteractor.selectTarget.transform.root.gameObject.GetComponent<PhotonView>();


                //grabInteractor.selectingInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
                //grabInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
                //root.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<XRDirectInteractor>().interactionLayerMask = LayerMask.GetMask("InHand");

                leftParent.transform.GetChild(0).gameObject.SetActive(true);
                rightParent.transform.GetChild(0).gameObject.SetActive(true);
    
                rightParent.transform.GetChild(1).gameObject.SetActive(false);
                leftParent.transform.GetChild(1).gameObject.SetActive(false);
                break;
        }
    }
}
