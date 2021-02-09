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

    private Vector3 networkVelocity;

    private Quaternion networkRotationBall;
    private float angleBall;

    public Transform ballTransform;

    public Rigidbody ballRigidBody;

    PhotonView photonView;

    private bool firstTake = false;

    private Vector3 onUpdatePos;
    private Vector3 lastCorrectPos;
    private float fraction;


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
        //storedPositionBall = new Vector3(0.861f, 0.783408f, -3.199672f);
        //networkPositionBall = new Vector3(0.861f, 0.783408f, -3.199672f);
        Debug.Log("deltaTime = " + Time.deltaTime);
    }

    // Update is called once per frame
    public void Update()
    {
        if (!photonView.IsMine)
        {
            float count = 0f;
            float duration = 0.1f;
            while (count < duration)
            {
                count += Time.deltaTime;
                Vector3 currentPos = ballTransform.position;
                float time = Vector3.Distance(currentPos, networkPositionBall) / (duration - count) * Time.deltaTime;
                ballTransform.position = Vector3.MoveTowards(ballTransform.position, networkPositionBall, time);
            }
            //ballRigidBody.position = Vector3.MoveTowards(ballRigidBody.position, networkPositionBall, Mathf.Abs(Vector3.Magnitude(ballRigidBody.velocity)) * Time.fixedDeltaTime);
            //ballRigidBody.rotation = Quaternion.RotateTowards(ballTransform.rotation, networkRotationBall, Time.fixedDeltaTime * 100f);
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

            stream.SendNext(ballRigidBody.velocity);
            stream.SendNext(ballTransform.position);
            //stream.SendNext(ballRigidBody.rotation);
        }
        else
        {
            //networkRotationBall = (Quaternion)stream.ReceiveNext();
            networkVelocity = (Vector3)stream.ReceiveNext();

            networkPositionBall = (Vector3)stream.ReceiveNext();
            float lag2 = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            networkPositionBall += (this.ballRigidBody.velocity * lag2);

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
