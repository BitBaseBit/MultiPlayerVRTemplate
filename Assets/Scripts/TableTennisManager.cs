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


    public void OnSelectEnter()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();


        GameObject avatar = GameObject.FindGameObjectWithTag("avatar");

        var avatarHands = avatar.GetComponent<AvatarHolder>();

        string handEnter = grabInteractable.selectingInteractor.name;


        if (handEnter == "Right Base Controller")
        {
            grabInteractable.attachTransform = rightTransform;
            avatarHands.rightHand.SetActive(false);
            avatarHands.leftHand.SetActive(false);
            Debug.Log(oculusTouchLeft.name);
            oculusTouchLeft.SetActive(true);
            oculusTouchRight.SetActive(false);
            Debug.Log(oculusTouchRight.name);
        }
        else if (handEnter == "Left Base Controller")
        {
            grabInteractable.attachTransform = leftTransform;
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