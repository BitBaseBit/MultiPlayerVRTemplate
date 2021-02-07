using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;

public class NetworkBallSync : MonoBehaviour, IPunObservable
{
    GameObject ball;
    private float distanceBall;
    private Vector3 directionBall;
    private Vector3 networkPositionBall;
    private Vector3 storedPositionBall;
    private Quaternion networkRotationBall;
    private float angleBall;

    public Transform ballTransform;

    public Rigidbody ballRigidBody;

    PhotonView photonView;

    private bool firstTake = false;


    private void Awake()
    {
        photonView = this.GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        firstTake = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("ball1");
        storedPositionBall = new Vector3(0.861f, 0.783408f, -3.199672f);
        networkPositionBall = new Vector3(0.861f, 0.783408f, -3.199672f);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            ballRigidBody.position = Vector3.MoveTowards(ballRigidBody.position, networkPositionBall, Time.fixedDeltaTime);
            ballRigidBody.rotation = Quaternion.RotateTowards(ballTransform.rotation, networkRotationBall, Time.fixedDeltaTime * 100f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //directionBall = ballTransform.position - storedPositionBall;
            //storedPositionBall = ballTransform.position;

            //stream.SendNext(ballTransform.position);
            //stream.SendNext(directionBall);

            //stream.SendNext(ballTransform.rotation);

            stream.SendNext(ballRigidBody.position);
            stream.SendNext(ballRigidBody.rotation);
            stream.SendNext(ballRigidBody.velocity);
        }
        else
        {
            networkPositionBall = (Vector3)stream.ReceiveNext();
            networkRotationBall = (Quaternion)stream.ReceiveNext();
            ballRigidBody.velocity = (Vector3)stream.ReceiveNext();

            float lag2 = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            networkPositionBall += (this.ballRigidBody.velocity * lag2);

            //networkPositionBall = (Vector3)stream.ReceiveNext();
            //directionBall = (Vector3)stream.ReceiveNext();

            //if (firstTake)
            //{
            //    ballTransform.position = networkPositionBall;
            //    distanceBall = 0f;
            //}
            //else 
            //{
            //    float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            //    networkPositionBall += directionBall * lag;
            //    distanceBall = Vector3.Distance(ballTransform.position, networkPositionBall);
            //}

            //networkRotationBall = (Quaternion)stream.ReceiveNext();

            //if (firstTake)
            //{
            //    angleBall = 0f;
            //    ballTransform.rotation = networkRotationBall;
            //}
            //else
            //{
            //    angleBall = Quaternion.Angle(ballTransform.rotation, networkRotationBall);
            //}

            //if (firstTake)
            //{
            //    firstTake = false;
            //}    
        }
    }
}
