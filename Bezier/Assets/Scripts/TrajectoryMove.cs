using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryMove : MonoBehaviour
{

    private List<Transform> nodes;
    private bool closed;
    private int i = 0;
    private float t = 0;
    public float speed = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        //nodes = GetComponentInChildren<BezierGUI>().nodes;
        nodes = new List<Transform>();
        for (int i = 0; i < transform.parent.GetComponentInChildren<BezierGUI>().transform.childCount; i++)
        {
            Transform tr = transform.parent.GetComponentInChildren<BezierGUI>().transform.GetChild(i);
            if (tr!=transform) nodes.Add(tr);
        }
        closed = transform.parent.GetComponentInChildren<BezierGUI>().closed;
    }

    // Update is called once per frame
    void Update()
    {
        if (closed)
        {
            Debug.Log(nodes.Count);
            transform.position = Beizer.Coordinates(nodes[i].position, nodes[i].GetChild(nodes[i].childCount - 1).transform.position, 
                                               nodes[(i+1)%nodes.Count].GetChild(0).transform.position, nodes[(i + 1) % nodes.Count].position, t);
            transform.rotation = Quaternion.LookRotation(Beizer.firstDerivative(nodes[i].position, nodes[i].GetChild(nodes[i].childCount - 1).transform.position,
                                               nodes[(i + 1) % nodes.Count].GetChild(0).transform.position, nodes[(i + 1) % nodes.Count].position, t));
            t += speed;
            if (t > 1)
            {
                t--;
                i = (i + 1) % nodes.Count;
            }
        }
    }
}
