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
    public GameObject[] AvatarModelPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            // The player is local
            localXRRigGameObj.SetActive(true);
            gameObject.GetComponent<MovementController>().enabled = true;
            gameObject.GetComponent<AvatarInput>().enabled = true;


            // Get the avatar selection data so we can spawn the correct avatar
            object avatarSelectionNumber;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER, 
                                                                       out avatarSelectionNumber))
            {
                photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int)avatarSelectionNumber);
            }

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

    [PunRPC]
    public void InitializeSelectedAvatarModel(int avatarSelectionNumber)
    {
        GameObject selectedAvatarGameobject = Instantiate(AvatarModelPrefabs[avatarSelectionNumber], localXRRigGameObj.transform);

        AvatarInput avatarInputConverter = transform.GetComponent<AvatarInput>();
        AvatarHolder avatarHolder = selectedAvatarGameobject.GetComponent<AvatarHolder>();
        SetUpAvatarGameobject(avatarHolder.HeadTransform, avatarInputConverter.AvatarHead);
        SetUpAvatarGameobject(avatarHolder.BodyTransform, avatarInputConverter.AvatarBody);
        SetUpAvatarGameobject(avatarHolder.HandLeftTransform, avatarInputConverter.AvatarHand_Left);
        SetUpAvatarGameobject(avatarHolder.HandRightTransform, avatarInputConverter.AvatarHand_Right);
    }

    void SetUpAvatarGameobject(Transform avatarModelTransform, Transform mainAvatarTransform)
    {
        avatarModelTransform.SetParent(mainAvatarTransform);
        avatarModelTransform.localPosition = Vector3.zero;
        avatarModelTransform.localRotation = Quaternion.identity;
    }
}
