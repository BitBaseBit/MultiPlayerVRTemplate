using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TableTennisManager : MonoBehaviour
{
    public GameObject oculusTouchLeft;
    public GameObject oculusTouchRight;
    public GameObject bat;

    public Transform leftTransform;
    public Transform rightTransform;

    public Rigidbody rb;
    private bool isHovering = false;

    GameObject selectorAvatar;
    AvatarHolder selectorHands;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       if (isHovering)
       {
            var distanceRightFromBat = Vector3.Distance(selectorHands.HandRightTransform.position, bat.transform.position);
            var distanceLeftFromBat = Vector3.Distance(selectorHands.HandLeftTransform.position, bat.transform.position);

            var grabInteractable = GetComponent<JitrGrabInteractable>();

            if (distanceRightFromBat < distanceLeftFromBat)
            {
                grabInteractable.attachTransform = rightTransform;               
            }
            else if (distanceRightFromBat > distanceLeftFromBat)
            {
                grabInteractable.attachTransform = leftTransform;
            }    
       }
    }

    public void OnHoverEnter()
    {
        selectorAvatar = GameObject.FindGameObjectWithTag("avatar");
        selectorHands = selectorAvatar.GetComponent<AvatarHolder>();
        isHovering = true;
    }

    public void OnHoverExited()
    {
        isHovering = false;
    }


    public void OnSelectEnter()
    {
        var grabInteractable = GetComponent<JitrGrabInteractable>();


        GameObject avatar = GameObject.FindGameObjectWithTag("avatar");

        var avatarHands = avatar.GetComponent<AvatarHolder>();

        string handEnter = grabInteractable.selectingInteractor.name;


        if (handEnter == "Right Base Controller")
        {
            avatarHands.rightHand.SetActive(false);
            avatarHands.leftHand.SetActive(false);
            oculusTouchLeft.SetActive(true);
            oculusTouchRight.SetActive(false);
        }
        else if (handEnter == "Left Base Controller")
        {
            avatarHands.rightHand.SetActive(false);
            avatarHands.leftHand.SetActive(false);
            oculusTouchLeft.SetActive(false);
            oculusTouchRight.SetActive(true); 
        }
    }

    public void OnSelectExit()
    {
        GameObject avatar = GameObject.FindGameObjectWithTag("avatar");
        var avatarHands = avatar.GetComponent<AvatarHolder>();

        avatarHands.rightHand.SetActive(true);
        avatarHands.leftHand.SetActive(true);
        oculusTouchLeft.SetActive(false);
        oculusTouchRight.SetActive(false);
    }
}
