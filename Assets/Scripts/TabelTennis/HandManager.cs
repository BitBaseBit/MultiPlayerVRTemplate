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

    GameObject leftParent;
    GameObject rightParent;

    bool isHovering = false;
    bool hasSelected = false;
    bool canSelect = true;
    bool handsVisible;

    PhotonView batView;



    Transform root;
    XRBaseInteractor selector;
    // Start is called before the first frame update
    void Start()
    {
        var interactable = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
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
        var grabInteractable = GetComponent<XRGrabInteractable>();
	    batView = grabInteractable.selectingInteractor.transform.root.gameObject.GetComponent<PhotonView>();
        PhotonView view = this.GetComponent<PhotonView>();
        bool test = view == batView;

        Debug.Log("Is the photon view on the bat somehow the same on on the player: " + test);

        if (batView.IsMine)
        {
	        if (canSelect)
	        {
		        hasSelected = true;
	            canSelect = false;
		
	            selector = grabInteractable.selectingInteractor;
		        root = grabInteractable.selectingInteractor.gameObject.transform.root;
		        leftParent = root.transform.GetChild(2).GetChild(1).gameObject;
		        rightParent = root.transform.GetChild(2).GetChild(2).gameObject;
		
		        string handEnter = grabInteractable.selectingInteractor.name;
		
		
		        if (handEnter == "Right Base Controller")
		        {
                    view.RPC("ShowLeftController", RpcTarget.AllBuffered);
		        }
		        else if (handEnter == "Left Base Controller")
		        {
                    view.RPC("ShowRightController", RpcTarget.AllBuffered);
		        }
	            grabInteractable.selectingInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
	            grabInteractable.interactionLayerMask = LayerMask.GetMask("InHand");
	            root.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<XRDirectInteractor>().interactionLayerMask = LayerMask.GetMask("InHand");
	        }
        }
    }

    public void OnSelectExit()
    {
        hasSelected = false;
        canSelect = true;
        var grabInteractable = GetComponent<XRGrabInteractable>();
        selector.interactionLayerMask = LayerMask.GetMask("Interactable", "UI");
        grabInteractable.interactionLayerMask = LayerMask.GetMask("Interactable");
        root.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<XRDirectInteractor>().interactionLayerMask = LayerMask.GetMask("Interactable", "UI");

        PhotonView view = this.GetComponent<PhotonView>();
        if (batView.IsMine)
        {
            view.RPC("ShowHands", RpcTarget.AllBuffered);
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
