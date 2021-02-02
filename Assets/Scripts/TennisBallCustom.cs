using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBallCustom : MonoBehaviour
{
    Rigidbody rb;

    float rho_air = 1.225f;
    float Cd = 0.5f;
    float C_air;

    float C_magnus = 3.0f * 0.0001f;

    bool isColliding = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        float radius = GetComponent<SphereCollider>().radius;

        //rb.sleepVelocity = 0.001f;
        //rb.sleepAngularVelocity = 0.001f;

        C_air = 0.5f * rho_air * Cd * (Mathf.PI * radius * radius);
    }
    void FixedUpdate()
    {
        if (true)
        {
            //Air drag force
            rb.AddForce(- C_air * rb.velocity.magnitude * rb.velocity);

            //Magnus force
            rb.AddForce(C_magnus * Vector3.Cross(rb.angularVelocity, rb.velocity));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
}
