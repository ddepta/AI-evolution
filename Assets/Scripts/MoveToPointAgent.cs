using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MoveToPointAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    private Rigidbody rb;
    private float thrust = 2.7f;

    public Material positiveMaterial;
    public Material negativeMaterial;
    public MeshRenderer floorMeshRenderer;

    private float xRotation;
    private float yRotation;
    private float zRotation;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialAngularRotation;
    private Vector3 initialCenterOfMass;

    private ParticleSystem thruster_w;
    private ParticleSystem thruster_a;
    private ParticleSystem thruster_s;
    private ParticleSystem thruster_d;

    private SpawnManager spawnManager;

    private GameObject collectible;

    private float level = 0f;
    private int collectMultiple = 5;
    private int collectCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        xRotation = this.transform.rotation.x;
        yRotation = this.transform.rotation.y;
        zRotation = this.transform.rotation.z;


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

        spawnManager = this.transform.parent.transform.parent.GetComponent<SpawnManager>();
        spawnManager.SpawnCollectible();

        collectible = spawnManager.GetCollectible();

    }
    public override void OnEpisodeBegin()
    {
        ResetAgent();
    }


    private void Update()
    {
        //xRotation = this.transform.rotation.x;
        //yRotation = this.transform.rotation.y;
        //zRotation = this.transform.rotation.z;
        //var reward = (Mathf.Abs(xRotation) + Mathf.Abs(yRotation) + Mathf.Abs(zRotation));

        //Debug.Log("rotation reward: " + -reward);
        //Debug.Log("reward: " + GetCumulativeReward());
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.localRotation.x);
        sensor.AddObservation(transform.localRotation.y);
        sensor.AddObservation(transform.localRotation.z);
        sensor.AddObservation(rb.angularVelocity.y);
        sensor.AddObservation(collectible.transform.localPosition);

        // Distance to collectible point
       // sensor.AddObservation(Vector3.Distance(collectible.transform.position, gameObject.transform.position));

        // Direction to collectible point
        sensor.AddObservation(collectible.transform.position - gameObject.transform.position);
    }

    private void MoveAgent(ActionBuffers actions)
    {
        AddReward(-0.0005f);

        var w = actions.DiscreteActions[0];
        var a = actions.DiscreteActions[1];
        var s = actions.DiscreteActions[2];
        var d = actions.DiscreteActions[3];
        //var shift = actions.DiscreteActions[4];

        thruster_w.Stop();
        thruster_a.Stop();
        thruster_s.Stop();
        thruster_d.Stop();

        var boost = thrust;

        //if (shift == 1)
        //{
        //    boost = thrust * 1.3f;
        //}

        //Debug.Log(w.ToString() + a.ToString() + s.ToString() + d.ToString());

        if (w == 1)
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce((-transform.forward) * boost);
            rb.AddForce(transform.up * boost);
            thruster_w.Play();
        }

        if (a == 1)
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(transform.forward * boost);
            rb.AddForce(transform.up * boost);
            thruster_a.Play();
        }

        if (s == 1)
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(-transform.right * boost);
            rb.AddForce(transform.up * boost);
            thruster_s.Play();
        }

        if (d == 1)
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(transform.right * boost);
            rb.AddForce(transform.up * boost);
            thruster_d.Play();
        }

        var reward = (Mathf.Abs(rb.angularVelocity.y / 100f));
        AddReward(-reward);

        //var distance = (collectible.transform.position - gameObject.transform.position);
        //float distanceToTarget = Vector3.Distance(transform.localPosition, collectible.transform.localPosition);
        //
        //if(bestDistance == null || distanceToTarget < bestDistance)
        //{
        //    bestDistance = distanceToTarget;
        //    AddReward(0.0005f);
        //
        //}
        //
        //Debug.Log(distanceToTarget);


        //float moveSpeed = 1f;
        //transform.position = new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        //Debug.Log(actions.DiscreteActions[0]);
        //base.OnActionReceived(actions);

    }

    private void ResetAgent()
    {
        transform.rotation = initialRotation;
        transform.position = initialPosition;
        rb.angularVelocity = initialAngularRotation;
        rb.centerOfMass = initialCenterOfMass;
        rb.velocity = new Vector3(0, 0, 0);
    }



    public override void OnActionReceived(ActionBuffers actions)
    {
        MoveAgent(actions);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        //continuousActions[0] = Input.GetAxisRaw("Horizontal");
        //continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    void OnTriggerEnter(Collider other)
    {
        // collect point
        if (other.gameObject.layer == 8)
        {
            if (floorMeshRenderer != null)
            {
                floorMeshRenderer.material = positiveMaterial;
            }

            spawnManager.MoveCollectible();

            if (level != 11f)
            {
                level = Academy.Instance.EnvironmentParameters.GetWithDefault("collectible_height", 1f);
                SetReward(1f);
                EndEpisode();
            }
            else
            {
                if(collectMultiple == collectCount)
                {
                    AddReward(1f);
                    collectCount = 0;
                    EndEpisode();
                }
                else
                {
                    AddReward((1f / collectMultiple));
                    collectCount++;
                }
            }

        }

        // collision with wall
        if (other.gameObject.layer == 9)
        {
            if (floorMeshRenderer != null)
            {
                floorMeshRenderer.material = negativeMaterial;
            }

            SetReward(-1f);
            EndEpisode();
        }
    }
}
