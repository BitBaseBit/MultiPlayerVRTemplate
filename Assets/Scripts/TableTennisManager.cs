using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TableTennisManager : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject oculusTouchLeft;
    public GameObject oculusTouchRight;
    public GameObject bat;

    char hand;

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
            rightHand.SetActive(false);
            leftHand.SetActive(false);
            oculusTouchLeft.SetActive(true);
            oculusTouchRight.SetActive(false);
        }
        else if (handEnter == "Left Base Controller")
        {
            hand = 'L';
            rightHand.SetActive(false);
            leftHand.SetActive(false);
            oculusTouchLeft.SetActive(false);
            oculusTouchRight.SetActive(true); 

        }
    }

    public void OnSelectExit()
    {
        rightHand.SetActive(true);
        leftHand.SetActive(true);
        oculusTouchLeft.SetActive(false);
        oculusTouchRight.SetActive(false);
    }
}
