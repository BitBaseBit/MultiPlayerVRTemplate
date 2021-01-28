using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


// We know that they are playing table tennis if one of the controller is active,
// that is how we know that we should send data about the left or right controller
// We can also tell which handedness they are by telling which controller is active 

public class MultiplayerVRSynchronization : MonoBehaviour, IPunObservable
{

    private PhotonView m_PhotonView;


    //Main VRPlayer Transform Synch
    [Header("Main VRPlayer Transform Synch")]
    public Transform generalVRPlayerTransform;

    //Position
    private float m_Distance_GeneralVRPlayer;
    private Vector3 m_Direction_GeneralVRPlayer;
    private Vector3 m_NetworkPosition_GeneralVRPlayer;
    private Vector3 m_StoredPosition_GeneralVRPlayer;

    //Rotation
    private Quaternion m_NetworkRotation_GeneralVRPlayer;
    private float m_Angle_GeneralVRPlayer;


    //Main Avatar Transform Synch
    [Header("Main Avatar Transform Synch")]
    public Transform mainAvatarTransform;



    //Position
    private float m_Distance_MainAvatar;
    private Vector3 m_Direction_MainAvatar;
    private Vector3 m_NetworkPosition_MainAvatar;
    private Vector3 m_StoredPosition_MainAvatar;

    //Rotation
    private Quaternion m_NetworkRotation_MainAvatar;
    private float m_Angle_MainAvatar;

    //Head  Synch
    //Rotation
    [Header("Avatar Head Transform Synch")]
    public Transform headTransform;

    private Quaternion m_NetworkRotation_Head;
    private float m_Angle_Head;

    //Body Synch
    //Rotation
    [Header("Avatar Body Transform Synch")]
    public Transform bodyTransform;

    private Quaternion m_NetworkRotation_Body;
    private float m_Angle_Body;


    //Hands Synch
    [Header("Hands Transform Synch")]
    public Transform leftHandTransform;
    private Transform leftControllerTransform;

    public Transform rightHandTransform;
    private Transform rightControllerTransform;

    //Left Hand Sync
    private GameObject leftController;
    //Position Hand
    private float m_Distance_LeftHand;
    private Vector3 m_Direction_LeftHand;
    private Vector3 m_NetworkPosition_LeftHand;
    private Vector3 m_StoredPosition_LeftHand;

    //Rotation Hand
    private Quaternion m_NetworkRotation_LeftHand;
    private float m_Angle_LeftHand;

    //Position Controller
    private float m_Distance_LeftController;
    private Vector3 m_Direction_LeftController;
    private Vector3 m_NetworkPosition_LeftController;
    private Vector3 m_StoredPosition_LeftController;

    //Rotation Controller
    private Quaternion m_NetworkRotation_LeftController;
    private float m_Angle_LeftController;

    //Right Hand Synch
    public GameObject rightController;

    //Position Hand
    private float m_Distance_RightHand;
    private Vector3 m_Direction_RightHand;
    private Vector3 m_NetworkPosition_RightHand;
    private Vector3 m_StoredPosition_RightHand;

    //Rotation Hand
    private Quaternion m_NetworkRotation_RightHand;
    private float m_Angle_RightHand;

    // Position Controller
    private float m_Distance_RightController;
    private Vector3 m_Direction_RightController;
    private Vector3 m_NetworkPosition_RightController;
    private Vector3 m_StoredPosition_RightController;

    // Rotation Controller
    private Quaternion m_NetworkRotation_RightController;
    private float m_Angle_RightController;



    bool m_firstTake = false;

    // To check if they are playing table tennis, 
    // only need to see if their hand is active,
    // also only need to know which controller is active
    bool isLeftHandActive = true;
    bool isLeftControllerActive = false;
    bool isRightControllerActive = false;

    public void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();

        //Main VRPlayer Synch Init
        m_StoredPosition_GeneralVRPlayer = generalVRPlayerTransform.position;
        m_NetworkPosition_GeneralVRPlayer = Vector3.zero;
        m_NetworkRotation_GeneralVRPlayer = Quaternion.identity;

        //Main Avatar Synch Init
        m_StoredPosition_MainAvatar = mainAvatarTransform.localPosition;
        m_NetworkPosition_MainAvatar = Vector3.zero;
        m_NetworkRotation_MainAvatar = Quaternion.identity;

        //Head Synch Init
        m_NetworkRotation_Head = Quaternion.identity;

        //Body Synch Init
        m_NetworkRotation_Body = Quaternion.identity;

        //Left Hand Synch Init
        m_StoredPosition_LeftHand = leftHandTransform.localPosition;
        m_NetworkPosition_LeftHand = Vector3.zero;
        m_NetworkRotation_LeftHand = Quaternion.identity;

        // Left Controller sync Init

        //Right Hand Synch Init
        m_StoredPosition_RightHand = rightHandTransform.localPosition;
        m_NetworkPosition_RightHand = Vector3.zero;
        m_NetworkRotation_RightHand = Quaternion.identity;

    }

    public void Start()
    {
        while (!PlayerNetworkSetup.hasActivated) continue;


        // Right Controller sync init
        rightControllerTransform = rightHandTransform.GetChild(1).transform;
        rightController = rightControllerTransform.gameObject;
        m_StoredPosition_RightController = rightHandTransform.localPosition;
        m_NetworkPosition_RightController = Vector3.zero;
        m_NetworkRotation_RightController = Quaternion.identity;

        leftControllerTransform = leftHandTransform.GetChild(1).transform;
        leftController = leftControllerTransform.gameObject;
        m_StoredPosition_LeftController = leftControllerTransform.localPosition;
        m_NetworkPosition_LeftController = Vector3.zero;
        m_NetworkRotation_LeftController = Quaternion.identity;
    }

    void OnEnable()
    {
        m_firstTake = true;
    }

    public void Update()
    {
        if (!this.m_PhotonView.IsMine)
        {

            generalVRPlayerTransform.position = Vector3.MoveTowards(generalVRPlayerTransform.position, this.m_NetworkPosition_GeneralVRPlayer, this.m_Distance_GeneralVRPlayer * (1.0f / PhotonNetwork.SerializationRate));
            generalVRPlayerTransform.rotation = Quaternion.RotateTowards(generalVRPlayerTransform.rotation, this.m_NetworkRotation_GeneralVRPlayer, this.m_Angle_GeneralVRPlayer * (1.0f / PhotonNetwork.SerializationRate));

            mainAvatarTransform.localPosition = Vector3.MoveTowards(mainAvatarTransform.localPosition, this.m_NetworkPosition_MainAvatar, this.m_Distance_MainAvatar * (1.0f / PhotonNetwork.SerializationRate));
            mainAvatarTransform.localRotation = Quaternion.RotateTowards(mainAvatarTransform.localRotation, this.m_NetworkRotation_MainAvatar, this.m_Angle_MainAvatar * (1.0f / PhotonNetwork.SerializationRate));

            headTransform.localRotation = Quaternion.RotateTowards(headTransform.localRotation, this.m_NetworkRotation_Head, this.m_Angle_Head * (1.0f / PhotonNetwork.SerializationRate));

            bodyTransform.localRotation = Quaternion.RotateTowards(bodyTransform.localRotation, this.m_NetworkRotation_Body, this.m_Angle_Body * (1.0f / PhotonNetwork.SerializationRate));

            leftHandTransform.localPosition = Vector3.MoveTowards(leftHandTransform.localPosition, this.m_NetworkPosition_LeftHand, this.m_Distance_LeftHand * (1.0f / PhotonNetwork.SerializationRate));
            leftHandTransform.localRotation = Quaternion.RotateTowards(leftHandTransform.localRotation, this.m_NetworkRotation_LeftHand, this.m_Angle_LeftHand * (1.0f / PhotonNetwork.SerializationRate));

            leftControllerTransform.localPosition = Vector3.MoveTowards(leftControllerTransform.localPosition, this.m_NetworkPosition_LeftController, this.m_Distance_LeftController * (1.0f / PhotonNetwork.SerializationRate));
            leftControllerTransform.localRotation = Quaternion.RotateTowards(leftControllerTransform.localRotation, this.m_NetworkRotation_LeftController, this.m_Angle_LeftController * (1.0f / PhotonNetwork.SerializationRate));

            rightHandTransform.localPosition = Vector3.MoveTowards(rightHandTransform.localPosition, this.m_NetworkPosition_RightHand, this.m_Distance_RightHand * (1.0f / PhotonNetwork.SerializationRate));
            rightHandTransform.localRotation = Quaternion.RotateTowards(rightHandTransform.localRotation, this.m_NetworkRotation_RightHand, this.m_Angle_RightHand * (1.0f / PhotonNetwork.SerializationRate));

            rightControllerTransform.localPosition = Vector3.MoveTowards(rightControllerTransform.localPosition, this.m_NetworkPosition_RightController, this.m_Distance_RightController * (1.0f / PhotonNetwork.SerializationRate));
            rightControllerTransform.localRotation = Quaternion.RotateTowards(rightControllerTransform.localRotation, this.m_NetworkRotation_RightController, this.m_Angle_RightController * (1.0f / PhotonNetwork.SerializationRate));

            if (isLeftHandActive)
            {
                leftHandTransform.gameObject.SetActive(true);
                rightHandTransform.gameObject.SetActive(true);
            }    
            else if (isLeftControllerActive)
            {
                Debug.Log("Got Here: left Controller Active!!!");
                leftController.SetActive(true);

                leftHandTransform.gameObject.SetActive(false);
                rightHandTransform.gameObject.SetActive(false);
            } 
            else if(isRightControllerActive)
            {
                rightController.SetActive(true);
                leftHandTransform.gameObject.SetActive(false);
                rightHandTransform.gameObject.SetActive(false);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //////////////////////////////////////////////////////////////////
            //General VRPlayer Transform Synch

            //Send Main Avatar position data
            this.m_Direction_GeneralVRPlayer = generalVRPlayerTransform.position - this.m_StoredPosition_GeneralVRPlayer;
            this.m_StoredPosition_GeneralVRPlayer = generalVRPlayerTransform.position;

            stream.SendNext(generalVRPlayerTransform.position);
            stream.SendNext(this.m_Direction_GeneralVRPlayer);

            //Send Main Avatar rotation data
            stream.SendNext(generalVRPlayerTransform.rotation);


            //////////////////////////////////////////////////////////////////
            //Main Avatar Transform Synch

            //Send Main Avatar position data
            this.m_Direction_MainAvatar = mainAvatarTransform.localPosition - this.m_StoredPosition_MainAvatar;
            this.m_StoredPosition_MainAvatar = mainAvatarTransform.localPosition;

            stream.SendNext(mainAvatarTransform.localPosition);
            stream.SendNext(this.m_Direction_MainAvatar);

            //Send Main Avatar rotation data
            stream.SendNext(mainAvatarTransform.localRotation);


            ///////////////////////////////////////////////////////////////////
            //Head rotation synch

            //Send Head rotation data
            stream.SendNext(headTransform.localRotation);


            ///////////////////////////////////////////////////////////////////
            //Body rotation synch

            //Send Body rotation data
            stream.SendNext(bodyTransform.localRotation);


            ///////////////////////////////////////////////////////////////////
            //Hands Transform Synch

            //Left Hand

            //Send Left Hand position data
            this.m_Direction_LeftHand = leftHandTransform.localPosition - this.m_StoredPosition_LeftHand;
            this.m_StoredPosition_LeftHand = leftHandTransform.localPosition;

            stream.SendNext(leftHandTransform.localPosition);
            stream.SendNext(this.m_Direction_LeftHand);

            //Send Left Hand rotation data
            stream.SendNext(leftHandTransform.localRotation);
            stream.SendNext(leftHandTransform.gameObject.activeSelf);

            //Send Left Controller position data
            this.m_Direction_LeftController = leftControllerTransform.localPosition - this.m_StoredPosition_LeftController;
            this.m_StoredPosition_LeftController = leftControllerTransform.localPosition;

            stream.SendNext(leftControllerTransform.localPosition);
            stream.SendNext(this.m_Direction_LeftController);

            //Send Left Controller rotation data
            stream.SendNext(leftControllerTransform.localRotation);
            // if left Controller is active
            stream.SendNext(leftController.activeSelf);
            Debug.Log("From sending stream (leftcontroller.activeSelf: " + leftController.activeSelf);

            //Right Hand
            //Send Right Hand position data
            this.m_Direction_RightHand = rightHandTransform.localPosition - this.m_StoredPosition_RightHand;
            this.m_StoredPosition_RightHand = rightHandTransform.localPosition;

            stream.SendNext(rightHandTransform.localPosition);
            stream.SendNext(this.m_Direction_RightHand);

            //Send Right Hand rotation data
            stream.SendNext(rightHandTransform.localRotation);
            
        
            //Right Controller
            //Send Right Controller position data
            this.m_Direction_RightController = rightControllerTransform.localPosition - this.m_StoredPosition_RightController;
            this.m_StoredPosition_RightController = rightControllerTransform.localPosition;

            stream.SendNext(rightControllerTransform.localPosition);
            stream.SendNext(this.m_Direction_RightController);

            //Send Right Controller rotation data
            stream.SendNext(rightControllerTransform.localRotation);
            stream.SendNext(rightController.activeSelf);
            Debug.Log("From Sending Stream (rightController.ActiveSelf): " + rightController.activeSelf);
        }
        else
        {
            ///////////////////////////////////////////////////////////////////
            //Ganeral VR Player Transform Synch

            //Get VR Player position data
            this.m_NetworkPosition_GeneralVRPlayer = (Vector3)stream.ReceiveNext();
            this.m_Direction_GeneralVRPlayer = (Vector3)stream.ReceiveNext();

            if (m_firstTake)
            {
                generalVRPlayerTransform.position = this.m_NetworkPosition_GeneralVRPlayer;
                this.m_Distance_GeneralVRPlayer = 0f;
            }
            else
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                this.m_NetworkPosition_GeneralVRPlayer += this.m_Direction_GeneralVRPlayer * lag;
                this.m_Distance_GeneralVRPlayer = Vector3.Distance(generalVRPlayerTransform.position, this.m_NetworkPosition_GeneralVRPlayer);
            }

            //Get Main Avatar rotation data
            this.m_NetworkRotation_GeneralVRPlayer = (Quaternion)stream.ReceiveNext();
            if (m_firstTake)
            {
                this.m_Angle_GeneralVRPlayer = 0f;
                generalVRPlayerTransform.rotation = this.m_NetworkRotation_GeneralVRPlayer;
            }
            else
            {
                this.m_Angle_GeneralVRPlayer = Quaternion.Angle(generalVRPlayerTransform.rotation, this.m_NetworkRotation_GeneralVRPlayer);
            }

            ///////////////////////////////////////////////////////////////////
            //Main Avatar Transform Synch

            //Get Main Avatar position data
            this.m_NetworkPosition_MainAvatar = (Vector3)stream.ReceiveNext();
            this.m_Direction_MainAvatar = (Vector3)stream.ReceiveNext();

            if (m_firstTake)
            {
                mainAvatarTransform.localPosition = this.m_NetworkPosition_MainAvatar;
                this.m_Distance_MainAvatar = 0f;
            }
            else
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                this.m_NetworkPosition_MainAvatar += this.m_Direction_MainAvatar * lag;
                this.m_Distance_MainAvatar = Vector3.Distance(mainAvatarTransform.localPosition, this.m_NetworkPosition_MainAvatar);
            }

            //Get Main Avatar rotation data
            this.m_NetworkRotation_MainAvatar = (Quaternion)stream.ReceiveNext();
            if (m_firstTake)
            {
                this.m_Angle_MainAvatar = 0f;
                mainAvatarTransform.rotation = this.m_NetworkRotation_MainAvatar;
            }
            else
            {
                this.m_Angle_MainAvatar = Quaternion.Angle(mainAvatarTransform.rotation, this.m_NetworkRotation_MainAvatar);
            }


            ///////////////////////////////////////////////////////////////////
            //Head rotation synch
            //Get Head rotation data 
            this.m_NetworkRotation_Head = (Quaternion)stream.ReceiveNext();

            if (m_firstTake)
            {
                this.m_Angle_Head = 0f;
                headTransform.localRotation = this.m_NetworkRotation_Head;
            }
            else
            {
                this.m_Angle_Head = Quaternion.Angle(headTransform.localRotation, this.m_NetworkRotation_Head);
            }

            ///////////////////////////////////////////////////////////////////
            //Body rotation synch
            //Get Body rotation data 
            this.m_NetworkRotation_Body = (Quaternion)stream.ReceiveNext();

            if (m_firstTake)
            {
                this.m_Angle_Body = 0f;
                bodyTransform.localRotation = this.m_NetworkRotation_Body;
            }
            else
            {
                this.m_Angle_Body = Quaternion.Angle(bodyTransform.localRotation, this.m_NetworkRotation_Body);
            }

            ///////////////////////////////////////////////////////////////////
            //Hands Transform Synch
            //Get Left Hand position data

            this.m_NetworkPosition_LeftHand = (Vector3)stream.ReceiveNext();
            this.m_Direction_LeftHand = (Vector3)stream.ReceiveNext();

            if (m_firstTake)
            {
                leftHandTransform.localPosition = this.m_NetworkPosition_LeftHand;
                this.m_Distance_LeftHand = 0f;
            }
            else
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                this.m_NetworkPosition_LeftHand += this.m_Direction_LeftHand * lag;
                this.m_Distance_LeftHand = Vector3.Distance(leftHandTransform.localPosition, this.m_NetworkPosition_LeftHand);
            }

            //Get Left Hand rotation data
            this.m_NetworkRotation_LeftHand = (Quaternion)stream.ReceiveNext();
            isLeftHandActive = (bool)stream.ReceiveNext();
            Debug.Log("From Recieving Stream (isLeftHandActive): " + isLeftHandActive);
            if (m_firstTake)
            {
                this.m_Angle_LeftHand = 0f;
                leftHandTransform.localRotation = this.m_NetworkRotation_LeftHand;
            }
            else
            {
                this.m_Angle_LeftHand = Quaternion.Angle(leftHandTransform.localRotation, this.m_NetworkRotation_LeftHand);
            }
            //Get Left Controller position data

            this.m_NetworkPosition_LeftController = (Vector3)stream.ReceiveNext();
            this.m_Direction_LeftController = (Vector3)stream.ReceiveNext();

            if (m_firstTake)
            {
                leftControllerTransform.localPosition = this.m_NetworkPosition_LeftController;
                this.m_Distance_LeftController = 0f;
            }
            else
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                this.m_NetworkPosition_LeftController += this.m_Direction_LeftController * lag;
                this.m_Distance_LeftController = Vector3.Distance(leftControllerTransform.localPosition, this.m_NetworkPosition_LeftController);
            }

            //Get Left Controller rotation data
            this.m_NetworkRotation_LeftController = (Quaternion)stream.ReceiveNext();
            isLeftControllerActive = (bool)stream.ReceiveNext();
            Debug.Log("From Recieving Stream (isLeftControllerActive): " + isLeftControllerActive);
            if (m_firstTake)
            {
                this.m_Angle_LeftController = 0f;
                leftControllerTransform.localRotation = this.m_NetworkRotation_LeftController;
            }
            else
            {
                this.m_Angle_LeftController = Quaternion.Angle(leftControllerTransform.localRotation, this.m_NetworkRotation_LeftController);
            }


            //Get Right Hand position data
            this.m_NetworkPosition_RightHand = (Vector3)stream.ReceiveNext();
            this.m_Direction_RightHand = (Vector3)stream.ReceiveNext();

            if (m_firstTake)
            {
                rightHandTransform.localPosition = this.m_NetworkPosition_RightHand;
                this.m_Distance_RightHand = 0f;
            }
            else
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                this.m_NetworkPosition_RightHand += this.m_Direction_RightHand * lag;
                this.m_Distance_RightHand = Vector3.Distance(rightHandTransform.localPosition, this.m_NetworkPosition_RightHand);
            }

            //Get Right Hand rotation data
            this.m_NetworkRotation_RightHand = (Quaternion)stream.ReceiveNext();
            if (m_firstTake)
            {
                this.m_Angle_RightHand = 0f;
                rightHandTransform.localRotation = this.m_NetworkRotation_RightHand;
            }
            else
            {
                this.m_Angle_RightHand = Quaternion.Angle(rightHandTransform.localRotation, this.m_NetworkRotation_RightHand);
            }

            //Get Right Controller position data
            this.m_NetworkPosition_RightController = (Vector3)stream.ReceiveNext();
            this.m_Direction_RightController = (Vector3)stream.ReceiveNext();

            if (m_firstTake)
            {
                rightControllerTransform.localPosition = this.m_NetworkPosition_RightController;
                this.m_Distance_RightController = 0f;
            }
            else
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                this.m_NetworkPosition_RightController += this.m_Direction_RightController * lag;
                this.m_Distance_RightController = Vector3.Distance(rightControllerTransform.localPosition, this.m_NetworkPosition_RightController);
            }

            //Get Right Controller rotation data
            this.m_NetworkRotation_RightController = (Quaternion)stream.ReceiveNext();
            isRightControllerActive = (bool)stream.ReceiveNext();
            Debug.Log("From Recieving Stream (isRightControllerActive): " + isRightControllerActive);
            if (m_firstTake)
            {
                this.m_Angle_RightController = 0f;
                rightControllerTransform.localRotation = this.m_NetworkRotation_RightController;
            }
            else
            {
                this.m_Angle_RightController = Quaternion.Angle(rightControllerTransform.localRotation, this.m_NetworkRotation_RightController);
            }
            if (m_firstTake)
            {
                m_firstTake = false;
            }
        }
    }
}
