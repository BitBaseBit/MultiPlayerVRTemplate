using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{

    private float maxSpeed = 31.0f;

    private float magnusConstant;
    private float C = 0.3f;
    [SerializeField]
    private float rbRadius = 0.02558121f;
    private float airDensity = 1.225f;

    private Rigidbody rb;
    private Vector3 magnusForcePrev;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        magnusConstant = 0.5f * C * 3.14159f * Mathf.Pow(rbRadius, 2) * airDensity;
        magnusForcePrev = Vector3.zero;
    }

    private void FixedUpdate()
    {
        ApplyMagnusForce();
    }

    private void ApplyMagnusForce()
    {
        Vector3 velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        Vector3 angularVelocity = rb.angularVelocity;
        Vector3 magnusForce = magnusConstant * Vector3.Cross(angularVelocity, velocity);
        Vector3 magnusForceDelta = magnusForce;
        rb.AddForceAtPosition(magnusForceDelta, rb.centerOfMass, ForceMode.Force);
        magnusForcePrev = magnusForce;
        Debug.Log("Magnus force has been applied to ball: " + magnusForceDelta);
    }
}
