using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class TTBat2 : MonoBehaviour
{
    public Transform leftTransform;
    public Transform rightTransform;
    public GameObject bat;

    Rigidbody rb;

    public static GameObject leftParent;
    public static GameObject rightParent;

    public static PhotonView bat2View;
    public static int batID = 2;
    public bool isHovering = false;
    public static bool hasSelected = false;
    public bool canSelect = true;
    public static bool bat2ViewInit = false;

    public static char hand;
    Transform root;
    XRBaseInteractor selector;
    // Start is called before the first frame update
    void Start()
    {
        
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
            canSelect = false;
	        hasSelected = true;
	        var grabInteractable = GetComponent<XRGrabInteractable>();
	
	        root = grabInteractable.selectingInteractor.gameObject.transform.root;
            selector = grabInteractable.selectingInteractor;
	        leftParent = root.transform.GetChild(2).GetChild(1).gameObject;
	        rightParent = root.transform.GetChild(2).GetChild(2).gameObject;
	
	        bat2View = grabInteractable.selectingInteractor.transform.root.gameObject.GetComponent<PhotonView>();
	        bat2ViewInit = true;
	
	        string handEnter = grabInteractable.selectingInteractor.name;
	
	
	        if (handEnter == "Right Base Controller")
	        {
	            hand = 'R';
	        }
	        else if (handEnter == "Left Base Controller")
	        {
	            hand = 'L';
	        }
            grabInteractable.selectingInteractor.interactionLayerMask = LayerMask.GetMask("InHandBat2");
            grabInteractable.interactionLayerMask = LayerMask.GetMask("InHandBat2");
            root.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<XRDirectInteractor>().interactionLayerMask = LayerMask.GetMask("InHandBat2");
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
