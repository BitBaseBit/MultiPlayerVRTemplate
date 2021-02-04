using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBallCustom : MonoBehaviour
{
    Rigidbody rb;

    float rho_air = 1.225f;
    float Cd = 0.3f;
    float C_air;

    float C_magnus = 2.7f * 0.0001f;

    bool isColliding = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        float radius = GetComponent<SphereCollider>().radius;

        rb.sleepThreshold = 0.01f;

        C_air = 0.5f * rho_air * Cd * (Mathf.PI * radius * radius);
    }
    void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 50.0f);

        //Magnus force
        rb.AddForce(C_magnus * Vector3.Cross(rb.angularVelocity, rb.velocity));

        if (true)
        {
            //Air drag force
            rb.AddForce(- C_air * rb.velocity.magnitude * rb.velocity);
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
