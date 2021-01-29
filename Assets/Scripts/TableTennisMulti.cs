using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

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
        if (leftParent.transform.root.gameObject.GetComponent<PhotonView>().IsMine)
        {
            isBeingHeld = true;

            var grabInteractable = GetComponent<XRGrabInteractable>();
            string handEnter = grabInteractable.selectingInteractor.name;

            if (handEnter == "Right Base Controller")
            {

                hand = 'R';

            }
            else if (handEnter == "Left Base Controller")
            {
                hand = 'L';
            }
        }
    }

    public void OnSelectExit()
    {
        if (leftParent.transform.root.gameObject.GetComponent<PhotonView>().IsMine)
        {
            isBeingHeld = false;
	        leftParent.transform.GetChild(0).gameObject.SetActive(true);
	        rightParent.transform.GetChild(0).gameObject.SetActive(true);
	
	        rightParent.transform.GetChild(1).gameObject.SetActive(false);
	        leftParent.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
