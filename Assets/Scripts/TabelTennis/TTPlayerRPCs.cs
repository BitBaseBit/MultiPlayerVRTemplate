using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class TTPlayerRPCs : MonoBehaviourPun
{
    XRBaseInteractor grabInteractor;
    XRGrabInteractable batInteractable;
    XRRayInteractor grabberLeft;
    XRDirectInteractor grabberRight;

    XRBaseInteractable batXR;

    public GameObject genericVRPlayerGameObj;
    public GameObject leftParent;
    public GameObject rightParent;
    public GameObject bat1;
    public GameObject bat2;
    bool isBeingHeld;

    bool bat1Selected;
    bool bat2Selected;
    bool firstTake;
    bool canSeeHands = true;


    GameObject bat;
    // Start is called before the first frame update
    void Start()
    {
        firstTake = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnSelectEnter()
    {
        //grabberLeft = genericVRPlayerGameObj.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<XRRayInteractor>();
        //grabberRight = genericVRPlayerGameObj.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<XRDirectInteractor>();

        bat1 = GameObject.FindGameObjectWithTag("bat1");
        bat2 = GameObject.FindGameObjectWithTag("bat2");

        bat1Selected = bat1.GetComponent<XRGrabInteractable>().selectingInteractor == null;
        bat2Selected = bat2.GetComponent<XRGrabInteractable>().selectingInteractor == null;

        Debug.Log("Got to 1");

        Debug.Log(photonView.IsMine);
        Debug.Log(!bat1Selected);
        Debug.Log(!bat2Selected);


        if (photonView.IsMine && (!bat1Selected || !bat2Selected))
        {
            Debug.Log("Got to 2");
            photonView.RPC("ShowController", RpcTarget.AllBuffered);
            canSeeHands = false;
        }

        isBeingHeld = true;

        //if (batXR.gameObject.CompareTag("bat1") && photonView.IsMine)
        //{
        //    //gameobject.findgameobjectswithtag("bat1");
        //    //var grabinteractable = getcomponent<xrgrabinteractable>();
        //    //batview = grabinteractable.selectinginteractor.transform.root.gameobject.getcomponent<photonview>();
        //    photonView.RPC("ShowController", RpcTarget.AllBuffered, 1);
        //}
        //else if (batXR.gameObject.CompareTag("bat2") && photonView.IsMine)
        //{
        //    photonView.RPC("ShowController", RpcTarget.AllBuffered, 2);
        //}
    }

    public void OnSelectExit()
    {
        isBeingHeld = false;

        //if (batXR.gameObject.CompareTag("bat1") && photonView.IsMine)
        //{
        //    photonView.RPC("ShowHands", RpcTarget.AllBuffered, 1);
        //}
        //else if (batXR.gameObject.CompareTag("bat2") && photonView.IsMine)
        //{
        //    photonView.RPC("ShowHands", RpcTarget.AllBuffered, 2);
        //}
        Debug.Log("Got to 3");
        Debug.Log(!bat1Selected);
        Debug.Log(!bat2Selected);
        if (photonView.IsMine && !canSeeHands)
        {
            Debug.Log("Got to 4");
            photonView.RPC("ShowHands",RpcTarget.AllBuffered);
            canSeeHands = true;
        }
    }

    [PunRPC]
    public void ShowController()
    {
        bat1 = GameObject.FindGameObjectWithTag("bat1");
        bat2 = GameObject.FindGameObjectWithTag("bat2");

        if (bat1.GetComponent<XRGrabInteractable>().selectingInteractor == null)
        {
            batInteractable = bat2.GetComponent<XRGrabInteractable>();
            grabInteractor = batInteractable.selectingInteractor;

            string handEnter1 = grabInteractor.name;
            Debug.Log(handEnter1);

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

        }
        else if (bat2.GetComponent<XRGrabInteractable>().selectingInteractor == null)
        {
            batInteractable = bat1.GetComponent<XRGrabInteractable>();
            grabInteractor = batInteractable.selectingInteractor;

            string handEnter1 = grabInteractor.name;
            Debug.Log(handEnter1);

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

        }

        ////switch (batTag)
        ////{
        ////    case 1:
        ////        bat = GameObject.FindGameObjectWithTag("bat1");
        ////        batInteractable = bat.GetComponent<XRGrabInteractable>();
        ////        grabInteractor = batInteractable.selectingInteractor;

        ////        // Person who is selecting the bat


        ////        //genericVRPlayerGameObj = grabInteractor.gameObject.transform.root.gameObject;
        ////        //leftParent = genericVRPlayerGameObj.transform.GetChild(2).GetChild(1).gameObject;
        ////        //rightParent = genericVRPlayerGameObj.transform.GetChild(2).GetChild(2).gameObject;

        ////        //grabInteractor.selectingInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
        ////        //grabInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
        ////        //root.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<XRDirectInteractor>().interactionLayerMask = LayerMask.GetMask("InHand");

        ////        string handEnter1 = grabInteractor.name;
        ////        Debug.Log(handEnter1);

        ////        if (handEnter1 == "Right Base Controller")
        ////        {
        ////            leftParent.transform.GetChild(0).gameObject.SetActive(false);
        ////            rightParent.transform.GetChild(0).gameObject.SetActive(false);

        ////            rightParent.transform.GetChild(1).gameObject.SetActive(false);
        ////            leftParent.transform.GetChild(1).gameObject.SetActive(true);
        ////        }
        ////        else if (handEnter1 == "Left Base Controller")
        ////        {
        ////            leftParent.transform.GetChild(0).gameObject.SetActive(false);
        ////            rightParent.transform.GetChild(0).gameObject.SetActive(false);

        ////            rightParent.transform.GetChild(1).gameObject.SetActive(true);
        ////            leftParent.transform.GetChild(1).gameObject.SetActive(false);
        ////        }
        ////        break;

        ////    case 2:
        ////        bat = GameObject.FindGameObjectWithTag("bat2");
        ////        batInteractable = bat.GetComponent<XRGrabInteractable>();
        ////        grabInteractor = batInteractable.selectingInteractor;


        ////        // Person who is selecting the bat

        ////        //genericVRPlayerGameObj = grabInteractor.gameObject.transform.root.gameObject;
        ////        //leftParent = genericVRPlayerGameObj.transform.GetChild(2).GetChild(1).gameObject;
        ////        //rightParent = genericVRPlayerGameObj.transform.GetChild(2).GetChild(2).gameObject;

        ////        //grabInteractor.selectingInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
        ////        //grabInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
        ////        //root.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<XRDirectInteractor>().interactionLayerMask = LayerMask.GetMask("InHand");

        ////        string handEnter = grabInteractor.name;
        ////        Debug.Log(handEnter);

        ////        if (handEnter == "Right Base Controller")
        ////        {
        ////            leftParent.transform.GetChild(0).gameObject.SetActive(false);
        ////            rightParent.transform.GetChild(0).gameObject.SetActive(false);

        ////            rightParent.transform.GetChild(1).gameObject.SetActive(false);
        ////            leftParent.transform.GetChild(1).gameObject.SetActive(true);
        ////        }
        ////        else if (handEnter == "Left Base Controller")
        ////        {
        ////            leftParent.transform.GetChild(0).gameObject.SetActive(false);
        ////            rightParent.transform.GetChild(0).gameObject.SetActive(false);

        ////            rightParent.transform.GetChild(1).gameObject.SetActive(true);
        ////            leftParent.transform.GetChild(1).gameObject.SetActive(false);
        ////        }
        ////        break;
        ////}
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
