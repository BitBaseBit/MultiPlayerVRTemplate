using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TableTennisMulti : MonoBehaviour
{

    public char hand;

    GameObject leftParent;
    GameObject rightParent;

    public bool isBeingHeld;



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

        var grabInteractable = GetComponent<XRGrabInteractable>();
        string handEnter = grabInteractable.selectingInteractor.name;

        if (handEnter == "Right Base Controller")
        {

            hand = 'R';
            leftParent.transform.GetChild(0).gameObject.SetActive(false);
            rightParent.transform.GetChild(0).gameObject.SetActive(false);

            rightParent.transform.GetChild(1).gameObject.SetActive(false);
            leftParent.transform.GetChild(1).gameObject.SetActive(true);

        }
        else if (handEnter == "Left Base Controller")
        {
            hand = 'L';
            leftParent.transform.GetChild(0).gameObject.SetActive(false);
            rightParent.transform.GetChild(0).gameObject.SetActive(false);

            rightParent.transform.GetChild(1).gameObject.SetActive(true);
            leftParent.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void OnSelectExit()
    {
        isBeingHeld = false;

        leftParent.transform.GetChild(0).gameObject.SetActive(true);
        rightParent.transform.GetChild(0).gameObject.SetActive(true);

        rightParent.transform.GetChild(1).gameObject.SetActive(false);
        leftParent.transform.GetChild(1).gameObject.SetActive(false);
    }
}
