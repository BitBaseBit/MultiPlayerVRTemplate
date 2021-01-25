using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TableTennisMulti : MonoBehaviour
{

    char hand;
    GameObject player;
    GenericVRPlayerComponents components;
     

    private void Awake()
    {
    }

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
        player = GameObject.FindWithTag("Player");
        components = player.GetComponent<GenericVRPlayerComponents>();
        var grabInteractable = GetComponent<XRGrabInteractable>();
        string handEnter = grabInteractable.selectingInteractor.name;
        Debug.Log(handEnter); 

        if (handEnter == "Right Base Controller")
        {
            hand = 'R';
            components.rightHand.SetActive(false);
            components.leftHand.SetActive(false);
            components.oculusTouchLeft.SetActive(true);
            components.oculusTouchRight.SetActive(false);
        }
        else if (handEnter == "Left Base Controller")
        {
            hand = 'L';
            components.rightHand.SetActive(false);
            components.leftHand.SetActive(false);
            components.oculusTouchLeft.SetActive(false);
            components.oculusTouchRight.SetActive(true); 

        }
    }

    public void OnSelectExit()
    {
        player = GameObject.FindWithTag("Player");
        components = player.GetComponent<GenericVRPlayerComponents>();
        components.rightHand.SetActive(true);
        components.leftHand.SetActive(true);
        components.oculusTouchLeft.SetActive(false);
        components.oculusTouchRight.SetActive(false);
    }
}
