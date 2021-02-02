using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBallCustom : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float radius;

    float rho_air = 1.225f;
    float Cd = 0.3f;
    float C_air;

    float C_magnus = 2.0f * 0.0001f;

    float I;

    private bool grounded;
    private bool tennisBatCollision;
    private Vector3 collisionForce;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        C_air = 0.5f * rho_air * Cd * (Mathf.PI * radius * radius);

        I = (2 / 3) * rb.mass * radius * radius;

        rb.useGravity = true;
        grounded = false;
        tennisBatCollision = false;
        collisionForce = Vector3.zero;
    }
    void FixedUpdate()
    {
        if (rb.IsSleeping())
        {
            return;
        }

        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, 5.0f);

        //if (rb.velocity.magnitude < 0.001f)
        //{
        //    return;
        //}

        if (!grounded)
        { 
            //Air drag force
            rb.AddForce(- C_air * rb.velocity.magnitude * rb.velocity);

            //Magnus force
            rb.AddForce(C_magnus * Vector3.Cross(rb.angularVelocity, rb.velocity));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*Vector3 collisionForce = collision.impulse;

        Vector3 direction = (collision.contacts[0].point - rb.centerOfMass);

        Debug.Log("collision Impulse: " + collisionForce);
        Debug.Log("direction: " + direction);

        //rb.AddTorque(Vector3.Cross(direction, collisionForce) / I, ForceMode.Force);
        Vector3 angularVelocityDelta = Vector3.ClampMagnitude((Vector3.Cross(direction, collisionForce) / I), 7.0f);
        Debug.Log("Angular velocity delta: " + angularVelocityDelta);

        rb.angularVelocity += angularVelocityDelta;*/

        //Debug.Log("Collision force: " + collisionForce);

        //rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, 7.0f);
        if (collision.collider.tag == "TennisBat")
        {
            //rb.velocity = Vector3.ClampMagnitude(rb.velocity, 5.0f);

            //Debug.Log("Angular velocity enter: " + rb.angularVelocity);
            //Debug.Log("Velocity enter: " + rb.velocity);
            //rb.isKinematic = true;
            tennisBatCollision = true;

            //float factor = 1f / (0.001f + rb.velocity.magnitude);
            //rb.AddForce(collision.collider.transform.forward * Time.fixedDeltaTime * 675f * factor, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        grounded = false;

        //rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, 7.0f);
        if (tennisBatCollision)
        {
            //rb.velocity = Vector3.ClampMagnitude(rb.velocity, 5.0f);

            //Debug.Log("Angular velocity stay: " + rb.angularVelocity);
            //Debug.Log("Velocity stay: " + rb.velocity);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionForce = Vector3.zero;
        if (tennisBatCollision)
        {
            //rb.velocity = Vector3.ClampMagnitude(rb.velocity, 5.0f);
            //rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, 7.0f);
            //Debug.Log("Angular velocity exit: " + rb.angularVelocity);
            //Debug.Log("Velocity exit: " + rb.velocity);
        }

        grounded = false;
        tennisBatCollision = false;
        //rb.isKinematic = false;
    }
}
