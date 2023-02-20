using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialAngularRotation;
    private Vector3 initialCenterOfMass;

    private ParticleSystem thruster_w;
    private ParticleSystem thruster_a;
    private ParticleSystem thruster_s;
    private ParticleSystem thruster_d;

    private SpawnManager spawnManager;

    private float thrust = 2.7f;

    public GameObject instance;

    private int score = 0;

    public TextMeshProUGUI PlayerScoreText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;
        initialAngularRotation = this.rb.angularVelocity;
        initialCenterOfMass = new Vector3(0, -4f, 0);
        this.rb.centerOfMass = initialCenterOfMass;

        thruster_w = transform.Find("Thruster W").transform.GetChild(0).GetComponent<ParticleSystem>();
        thruster_a = transform.Find("Thruster A").transform.GetChild(0).GetComponent<ParticleSystem>();
        thruster_s = transform.Find("Thruster S").transform.GetChild(0).GetComponent<ParticleSystem>();
        thruster_d = transform.Find("Thruster D").transform.GetChild(0).GetComponent<ParticleSystem>();

        spawnManager = instance.GetComponent<SpawnManager>();
    }

    private void ResetRocket()
    {
        this.transform.rotation = initialRotation;
        this.transform.position = initialPosition;
        this.rb.angularVelocity = initialAngularRotation;
        this.rb.centerOfMass = initialCenterOfMass;
        this.rb.velocity = new Vector3(0, 0, 0);
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
            ResetRocket();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(initialBody);
        //Debug.Log(initialRb.rotation.ToString());

        var boost = thrust;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            boost = thrust * 2;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(transform.forward * boost);
            rb.AddForce(transform.up * boost);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(-transform.right * boost);
            rb.AddForce(transform.up * boost);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce((-transform.forward) * boost);
            rb.AddForce(transform.up * boost);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(transform.right * boost);
            rb.AddForce(transform.up * boost);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // collect point
        if (other.gameObject.layer == 8)
        {
            spawnManager.MoveCollectible();

            // Score for UI-Scoreboard
            if (PlayerScoreText != null)
            {
                score++;
                PlayerScoreText.text = "Punkte: " + score;
            }
        }

        // collision with wall
        if (other.gameObject.layer == 9)
        {
            ResetRocket();
        }
    }
}
