using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{

    public GameObject localXRRigGameObj;
    public GameObject avatarHeadGameObj;
    public GameObject avatarBodyGameObj;
    

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            // The player is local
            localXRRigGameObj.SetActive(true);
            gameObject.GetComponent<MovementController>().enabled = true;
            gameObject.GetComponent<AvatarInput>().enabled = true;
            SetLayerRecursively(avatarBodyGameObj, 12);
            SetLayerRecursively(avatarHeadGameObj, 11);

            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();

            if (teleportationAreas.Length > 0)
            {
                Debug.Log("we have a floot to teleport on ");

                foreach ( var item in teleportationAreas)
                {
                    item.teleportationProvider = localXRRigGameObj.GetComponent<TeleportationProvider>();
                }
            }
        }
        else
        {
            // The player is a remote player
            localXRRigGameObj.SetActive(false);
            gameObject.GetComponent<MovementController>().enabled = false;
            gameObject.GetComponent<AvatarInput>().enabled = false;
            SetLayerRecursively(avatarBodyGameObj, 0);
            SetLayerRecursively(avatarHeadGameObj, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
