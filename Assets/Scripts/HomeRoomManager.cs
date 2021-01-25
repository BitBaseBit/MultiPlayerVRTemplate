using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class HomeRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TextMeshProUGUI occupancyRateText_School;

    [SerializeField]
    TextMeshProUGUI occupancyRateText_Outdoor;

    string mapType;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;    

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI Callback Methods

    public void  JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterRoomButtonClicked_Outdoor()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_OUTDOOR;
        ExitGames.Client.Photon.Hashtable expecteCustomRoomProperties =
            new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expecteCustomRoomProperties, 0);
    }

    public void OnEnterRoomButtonClicked_School()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_SCHOOL;
        ExitGames.Client.Photon.Hashtable expecteCustomRoomProperties =
            new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expecteCustomRoomProperties, 0);
    }

    #endregion

    #region Photon Callback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
        {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType))
            {
                if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_SCHOOL)
                {
                    PhotonNetwork.LoadLevel("World_School");
                }
                
                else if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_OUTDOOR)
                {
                    PhotonNetwork.LoadLevel("World_Outdoor");
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            occupancyRateText_School.text = "0 / 20 ";
            occupancyRateText_School.text = "0 / 20 ";
        }

        foreach (RoomInfo room in roomList)
        {
            if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_OUTDOOR))
            {
                occupancyRateText_Outdoor.text = room.PlayerCount.ToString() + " / 20";
            }
            else if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_OUTDOOR))
            {
                occupancyRateText_School.text = room.PlayerCount.ToString() + " / 20";
            }
        }
    }

    public override void OnJoinedLobby()
    {
    }
    #endregion

    #region Private Methods

    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room: " + mapType;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() {
            {MultiplayerVRConstants.MAP_TYPE_KEY, mapType}
        };

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties; 

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

    }

    #endregion
}
