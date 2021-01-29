using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class TTBat1 : MonoBehaviour
{
    public Transform leftTransform;
    public Transform rightTransform;
    public GameObject bat;

    GameObject leftParent;
    GameObject rightParent;

    public static int batID = 1;
    public static bool isHovering = false;
    public static bool hasSelected = false;
    public static PhotonView bat1View;
    public static bool bat1ViewInit = false;

    public static char hand;
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
        hasSelected = true;

        var grabInteractable = GetComponent<XRGrabInteractable>();
        var root = grabInteractable.selectingInteractor.gameObject.transform.root;
        leftParent = root.transform.GetChild(2).GetChild(1).gameObject;
        rightParent = root.transform.GetChild(2).GetChild(2).gameObject;

        string handEnter = grabInteractable.selectingInteractor.name;

        bat1View = grabInteractable.transform.root.gameObject.GetComponent<PhotonView>();
        bat1ViewInit = true;

        if (handEnter == "Right Base Controller")
        {
            hand = 'R';
        }
        else if (handEnter == "Left Base Controller")
        {
            hand = 'L';
        }
    }

    public void OnSelectExit()
    {
        hasSelected = false;
    }

}
