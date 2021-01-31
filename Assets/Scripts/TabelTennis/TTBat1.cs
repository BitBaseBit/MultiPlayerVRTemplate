using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class TTBat1 : MonoBehaviourPunCallbacks
{
    public Transform leftTransform;
    public Transform rightTransform;
    public GameObject bat;

    Rigidbody rb;

    public static GameObject leftParent;
    public static GameObject rightParent;

    public static int batID = 1;
    public bool isHovering = false;
    public static bool hasSelected = false;
    public bool canSelect = true;
    public static PhotonView bat1View;
    public static bool bat1ViewInit = false;
    public static bool handsVisible;

    public static char hand;

    public Transform root;
    public XRBaseInteractor selector;
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
        if (canSelect)
        {
	        hasSelected = true;
            canSelect = false;
	
	        var grabInteractable = GetComponent<XRGrabInteractable>();
            selector = grabInteractable.selectingInteractor;
	        root = grabInteractable.selectingInteractor.gameObject.transform.root;
	        leftParent = root.transform.GetChild(2).GetChild(1).gameObject;
	        rightParent = root.transform.GetChild(2).GetChild(2).gameObject;
	
	        string handEnter = grabInteractable.selectingInteractor.name;
	
	        bat1View = grabInteractable.selectingInteractor.transform.root.gameObject.GetComponent<PhotonView>();
	        bat1ViewInit = true;
	
	        if (handEnter == "Right Base Controller")
	        {
	            hand = 'R';
	        }
	        else if (handEnter == "Left Base Controller")
	        {
	            hand = 'L';
	        }
            grabInteractable.selectingInteractor.interactionLayerMask = LayerMask.GetMask("InHand");
            grabInteractable.interactionLayerMask = LayerMask.GetMask("InHand");
            root.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<XRDirectInteractor>().interactionLayerMask = LayerMask.GetMask("InHand");
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
    }

}
