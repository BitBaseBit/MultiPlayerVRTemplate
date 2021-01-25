using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TableTennisMulti : MonoBehaviour
{

    char hand;
    GenericVRPlayerComponents avatar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectEnter()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        string handEnter = grabInteractable.m_SelectingInteractor.name;

        if (handEnter == "Right Base Controller")
        {
            hand = 'R';
            avatar.rightHand.SetActive(false);
            avatar.leftHand.SetActive(false);
            avatar.oculusTouchLeft.SetActive(true);
            avatar.oculusTouchRight.SetActive(false);
        }
        else if (handEnter == "Left Base Controller")
        {
            hand = 'L';
            avatar.rightHand.SetActive(false);
            avatar.leftHand.SetActive(false);
            avatar.oculusTouchLeft.SetActive(false);
            avatar.oculusTouchRight.SetActive(true); 

        }
    }

    public void OnSelectExit()
    {
        avatar.rightHand.SetActive(true);
        avatar.leftHand.SetActive(true);
        avatar.oculusTouchLeft.SetActive(false);
        avatar.oculusTouchRight.SetActive(false);
    }
}
