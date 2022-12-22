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
    public float thrust = 2.8f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        transform.position = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(targetTransform);
        sensor.AddObservation(targetTransform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var w = actions.DiscreteActions[0];
        var a = actions.DiscreteActions[1];
        var s = actions.DiscreteActions[2];
        var d = actions.DiscreteActions[3];
        AddReward(-0.0005f);



        Debug.Log(w.ToString() + a.ToString() + s.ToString() + d.ToString());

        if (w == 1)
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce((-transform.forward) * thrust);
            rb.AddForce(transform.up * thrust);
        }
        if (a == 1)
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(transform.forward * thrust);
            rb.AddForce(transform.up * thrust);
        }
        
        if (s == 1)
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(-transform.right * thrust);
            rb.AddForce(transform.up * thrust);
        }
        if (d == 1)
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(transform.right * thrust);
            rb.AddForce(transform.up * thrust);
        }

        //float moveSpeed = 1f;
        //transform.position = new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        //Debug.Log(actions.DiscreteActions[0]);
        //base.OnActionReceived(actions);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        //continuousActions[0] = Input.GetAxisRaw("Horizontal");
        //continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            SetReward(1f);
            EndEpisode();

        }
        if (other.gameObject.layer == 9)
        {
            SetReward(-1f);
            EndEpisode();

        }
    }
}
