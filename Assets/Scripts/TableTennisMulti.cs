using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TableTennisMulti : MonoBehaviour
{

    public char hand;

    GameObject player;
    GenericVRPlayerComponents components;

    GameObject leftParent;
    GameObject rightParent;

    public bool isBeingHeld;

    public static TableTennisMulti Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        leftParent = GameObject.FindGameObjectWithTag("leftHand");
        rightParent = GameObject.FindGameObjectWithTag("rightHand");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectEnter()
    {
        isBeingHeld = true;
        player = GameObject.FindWithTag("Player");
        components = player.GetComponent<GenericVRPlayerComponents>();
        var grabInteractable = GetComponent<XRGrabInteractable>();
        string handEnter = grabInteractable.selectingInteractor.name;

        if (handEnter == "Right Base Controller")
        {

            hand = 'R';
            leftParent.SetActive(false);
            rightParent.SetActive(false);
            components.oculusTouchLeft.SetActive(true);
            components.oculusTouchRight.SetActive(false);
        }
        else if (handEnter == "Left Base Controller")
        {
            hand = 'L';
            leftParent.SetActive(false);
            rightParent.SetActive(false);
            components.oculusTouchLeft.SetActive(false);
            components.oculusTouchRight.SetActive(true); 

        }
    }

    public void OnSelectExit()
    {
        isBeingHeld = false;
        player = GameObject.FindWithTag("Player");
        components = player.GetComponent<GenericVRPlayerComponents>();

        leftParent.SetActive(true);
        rightParent.SetActive(true);
        components.oculusTouchLeft.SetActive(false);
        components.oculusTouchRight.SetActive(false);
    }
}
