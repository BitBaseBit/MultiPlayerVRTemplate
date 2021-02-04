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

    GameObject bat1;
    GameObject bat2;
    bool isBeingHeld;

    bool bat1Selected;
    bool bat2Selected;
    bool firstTake;
    bool canSeeHands = true;

    bool isHovering = false;


    GameObject bat;
    char hand;
    // Start is called before the first frame update
    void Start()
    {
        firstTake = true;
    }


    //public void OnHoverEnter()
    //{
    //    isHovering = true;
    //}

    //public void OnHoverExit()
    //{
    //    isHovering = false;
    //}

    public void OnSelectEnter()
    {
        bat1 = GameObject.FindGameObjectWithTag("bat1");
        bat2 = GameObject.FindGameObjectWithTag("bat2");

        if (bat1 == null && bat2 == null)
        {
            return;
        }




        bat1Selected = bat1.GetComponent<JitrGrabInteractable>().selectingInteractor == null;
        bat2Selected = bat2.GetComponent<JitrGrabInteractable>().selectingInteractor == null;

        var bat1Selector = bat1.GetComponent<JitrGrabInteractable>().selectingInteractor; 
        var bat2Selector = bat2.GetComponent<JitrGrabInteractable>().selectingInteractor;

        if (!bat1Selected && bat2Selected)
        {
            if (bat1Selector.name == "Right Base Controller")
            {
                hand = 'R';
            }
            else if (bat1Selector.name == "Left Base Controller")
            {
                hand = 'L';
            }

        }
        else if (bat1Selected && !bat2Selected)
        {
            if (bat2Selector.name == "Right Base Controller")
            {
                hand = 'R';
            }
            else if (bat2Selector.name == "Left Base Controller")
            {
                hand = 'L';
            }
        }


        if (bat1Selector == null && bat2Selector == null)
        {
            Debug.Log("Both bat1Selector and bat2Selector were null");
            return;
        }


        if (photonView.IsMine && (!bat1Selected || !bat2Selected))
        {
            if (hand == 'R')
            {
                photonView.RPC("ShowLeftController", RpcTarget.AllBuffered);
                canSeeHands = false;
            }
            else if (hand == 'L')
            {
                photonView.RPC("ShowRightController", RpcTarget.AllBuffered);
                canSeeHands = false;
            }
        }

        isBeingHeld = true;
    }

    public void OnSelectExit()
    {
        isBeingHeld = false;

        if (photonView.IsMine && !canSeeHands)
        {
            photonView.RPC("ShowHands",RpcTarget.AllBuffered);
            canSeeHands = true;
        }
    }

    [PunRPC]
    public void ShowLeftController()
    {
        leftParent.transform.GetChild(0).gameObject.SetActive(false);
        rightParent.transform.GetChild(0).gameObject.SetActive(false);

        rightParent.transform.GetChild(1).gameObject.SetActive(false);
        leftParent.transform.GetChild(1).gameObject.SetActive(true);
    }

    [PunRPC]
    public void ShowRightController()
    {
        leftParent.transform.GetChild(0).gameObject.SetActive(false);
        rightParent.transform.GetChild(0).gameObject.SetActive(false);

        rightParent.transform.GetChild(1).gameObject.SetActive(true);
        leftParent.transform.GetChild(1).gameObject.SetActive(false);
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
