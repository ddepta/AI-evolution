using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    public float thrust = 80f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialAngularRotation;
    private Vector3 initialCenterOfMass;
    Rigidbody initialRb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;
        initialAngularRotation = this.rb.angularVelocity;
        initialCenterOfMass = this.rb.centerOfMass;
        initialCenterOfMass = new Vector3(0, -0.8f, 0);
        this.rb.centerOfMass = initialCenterOfMass;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(initialBody);
        //Debug.Log(initialRb.rotation.ToString());

        if (Input.GetKey(KeyCode.W))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(transform.forward * thrust);
            rb.AddForce(transform.up * thrust);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(-transform.right * thrust);
            rb.AddForce(transform.up * thrust);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce((-transform.forward) * thrust);
            rb.AddForce(transform.up * thrust);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(transform.right * thrust);
            rb.AddForce(transform.up * thrust);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            this.transform.rotation = initialRotation;
            this.transform.position = initialPosition;
            this.rb.angularVelocity = initialAngularRotation;
            this.rb.centerOfMass = initialCenterOfMass;
            this.rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
