using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    public float thrust = 1.3f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialAngularRotation;
    private Vector3 initialCenterOfMass;
    Rigidbody initialRb;

    private ParticleSystem thruster_w;
    private ParticleSystem thruster_a;
    private ParticleSystem thruster_s;
    private ParticleSystem thruster_d;

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

        thruster_w = GameObject.Find("Thruster W").transform.GetChild(0).GetComponent<ParticleSystem>();
        thruster_a = GameObject.Find("Thruster A").transform.GetChild(0).GetComponent<ParticleSystem>();
        thruster_s = GameObject.Find("Thruster S").transform.GetChild(0).GetComponent<ParticleSystem>();
        thruster_d = GameObject.Find("Thruster D").transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            thruster_w.Play();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            thruster_w.Stop();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            thruster_a.Play();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            thruster_a.Stop();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            thruster_s.Play();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            thruster_s.Stop();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            thruster_d.Play();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            thruster_d.Stop();
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

    // Update is called once per frame
    void FixedUpdate()
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
    }
}
