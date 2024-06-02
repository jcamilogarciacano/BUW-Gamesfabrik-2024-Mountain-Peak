using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropeSegmentPrefab;
    public int segmentCount = 10;
    private List<GameObject> ropeSegments = new List<GameObject>();
    private LineRenderer lineRenderer;

    // add a variable for the ropesegment script
    //public RopeSegment ropeSegment;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        GenerateRope();
    }

    void GenerateRope()
    {
        GameObject previousSegment = this.gameObject;
        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 segmentPosition = new Vector3(this.transform.position.x, this.transform.position.y - (i * ropeSegmentPrefab.transform.localScale.y), this.transform.position.z);
            GameObject segment = Instantiate(ropeSegmentPrefab, segmentPosition, Quaternion.identity, this.transform);
            HingeJoint2D joint = segment.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousSegment.GetComponent<Rigidbody2D>();
            //add the script to the rope segment
            //ropeSegment = segment.GetComponent<RopeSegment>();
            
            // Configure the motor
            JointMotor2D motor = new JointMotor2D();
            motor.motorSpeed = 10; // Adjust this value to change the speed of the swing
            motor.maxMotorTorque = 1; // Adjust this value to change the force of the swing
            joint.motor = motor;
            joint.useMotor = true; // Enable the motor

            // Set the motor speed for the last segment
/*             if (i == segmentCount - 1)
            {
               // motor.motorSpeed = 10; // Adjust this value to change the speed of the last segment
                joint.motor = motor;
            } */

            // Set the joint limits
            JointAngleLimits2D limits = joint.limits;
            limits.min = -90; // Adjust this value to change the minimum angle
            limits.max = 90; // Adjust this value to change the maximum angle
            joint.limits = limits;
            joint.useLimits = true; // Enable the limits

            ropeSegments.Add(segment);
            previousSegment = segment;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Implement player interaction with the rope here
        DrawRope();

        // Iterate over all segments
        for (int i = 0; i < ropeSegments.Count; i++)
        {
            GameObject segment = ropeSegments[i];

            // Get the HingeJoint2D component of the segment
            HingeJoint2D joint = segment.GetComponent<HingeJoint2D>();

            // Get the current angle
            float angle = joint.jointAngle;

            // Adjust the motor speed based on the current angle
            JointMotor2D motor = joint.motor;
            if (angle < -90)
            {
                motor.motorSpeed = 100; // Swing to the right
            }
            else if (angle > 90)
            {
                motor.motorSpeed = -100; // Swing to the left
            }
            joint.motor = motor;
        }
    }
    void DrawRope()
    {
        lineRenderer.positionCount = ropeSegments.Count;
        for (int i = 0; i < ropeSegments.Count; i++)
        {
            lineRenderer.SetPosition(i, ropeSegments[i].transform.position);
        }
    }
}