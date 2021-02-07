using System.Collections;
using System.Collections.Generic;
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("ball1");
        storedPositionBall = new Vector3(0.861f, 0.783408f, -3.199672f);
        networkPositionBall = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
