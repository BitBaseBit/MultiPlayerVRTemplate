using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class HandManager : MonoBehaviourPunCallbacks
{    
    Transform leftTransform;
    Transform rightTransform;
    GameObject bat;

    Rigidbody rb;

    public static GameObject leftParent;
    public static GameObject rightParent;

    bool isHovering = false;
    bool hasSelected = false;
    bool canSelect = true;
    bool handsVisible;
    bool isBeingHeld = false;

    PhotonView batView;



    Transform root;
    XRBaseInteractor selector;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
       if (isBeingHeld)
        {
            rb.isKinematic = true;
            gameObject.layer = 13;
        }
        else
        {
            rb.isKinematic = false;
            gameObject.layer = 8;
        }
        //if (isHovering)
        //{
        //    var distanceRightFromBat = Vector3.Distance(rightParent.transform.position, bat.transform.position);
        //    var distanceLeftFromBat = Vector3.Distance(leftParent.transform.position, bat.transform.position);

        //    var grabInteractable = GetComponent<XRGrabInteractable>();

        //    if (distanceRightFromBat < distanceLeftFromBat)
        //    {
        //        grabInteractable.attachTransform = rightTransform;
        //    }
        //    else if (distanceRightFromBat > distanceLeftFromBat)
        //    {
        //        grabInteractable.attachTransform = leftTransform;
        //    }
        //}

    }
    public void OnHoverEnter()
    {
        isHovering = true;
    }

    public void OnHoverExited()
    {
        isHovering = false;
    }
    public void OnSelectEnter()
    {
        isBeingHeld = true;
        var grabInteractable = GetComponent<XRGrabInteractable>();
	    batView = grabInteractable.selectingInteractor.transform.root.gameObject.GetComponent<PhotonView>();

        if (this.gameObject.CompareTag("bat1"))
        {
            batView.RPC("ShowController", RpcTarget.AllBuffered, 1);
        }
        else if (this.gameObject.CompareTag("bat2"))
        {
            batView.RPC("ShowController", RpcTarget.AllBuffered, 2);
        }
    }

    public void OnSelectExit()
    {
        isBeingHeld = false;

        if (this.gameObject.CompareTag("bat1"))
        {
            batView.RPC("ShowHands", RpcTarget.AllBuffered, 1);
        }
        else if (this.gameObject.CompareTag("bat2"))
        {
            batView.RPC("ShowHands", RpcTarget.AllBuffered, 2);
        }
    }

}
