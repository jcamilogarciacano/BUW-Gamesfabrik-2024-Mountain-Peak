using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropeSegmentPrefab;
    public int segmentCount = 10;
    private List<GameObject> ropeSegments = new List<GameObject>();
    private LineRenderer lineRenderer;

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
        ropeSegments.Add(segment);
        previousSegment = segment;
    }
}
    // Update is called once per frame
    void Update()
    {
        // Implement player interaction with the rope here
        DrawRope();
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