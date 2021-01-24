using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks
{

    public TMP_InputField playerNameInputField;
    #region UNITY Methods
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region UI Callback Methods
    public void ConnectToPhotonServer()
    {
        if (playerNameInputField != null)
        {
            PhotonNetwork.NickName = playerNameInputField.text;
        }

        PhotonNetwork.ConnectUsingSettings(); 
    }

    #endregion

    #region Photon Callback Methods

    public override void OnConnected()
    {
        Debug.Log("OnConnected called, The server is available");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server");
        PhotonNetwork.LoadLevel("HomeScene");
    }

    #endregion
}
