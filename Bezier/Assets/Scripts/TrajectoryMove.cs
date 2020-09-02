﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryMove : MonoBehaviour
{

    private List<Transform> nodes;
    private bool closed;
    private int i = 0;
    private float t = 0;
    public float speed = 0.001f;

    /*
     * Равномерное движение
    public bool uniformMotion;
    private float firstDeltaPosition = 0;
    private float deltaPosition = 0;
    private Vector3 oldPosition;
    private Vector3 newPosition;
    */

    // Start is called before the first frame update
    void Start()
    {
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

            transform.position = Beizer.Coordinates(nodes[i].position, nodes[i].GetChild(nodes[i].childCount - 1).transform.position, 
                                               nodes[(i+1)%nodes.Count].GetChild(0).transform.position, nodes[(i + 1) % nodes.Count].position, t);
            transform.rotation = Quaternion.LookRotation(Beizer.firstDerivative(nodes[i].position, nodes[i].GetChild(nodes[i].childCount - 1).transform.position,
                                               nodes[(i + 1) % nodes.Count].GetChild(0).transform.position, nodes[(i + 1) % nodes.Count].position, t));

            /*
            * Равномерное движение
            if (uniformMotion)
            {
                oldPosition = transform.position;
                newPosition = Beizer.Coordinates(nodes[i].position, nodes[i].GetChild(nodes[i].childCount - 1).transform.position,
                                                    nodes[(i + 1) % nodes.Count].GetChild(0).transform.position, nodes[(i + 1) % nodes.Count].position, t + speed);
                deltaPosition = Mathf.Sqrt(Mathf.Pow(newPosition.x - oldPosition.x, 2) +
                                            Mathf.Pow(newPosition.y - oldPosition.y, 2) +
                                            Mathf.Pow(newPosition.z - oldPosition.z, 2));
                if (firstDeltaPosition != 0)
                {
                    Debug.Log("DELTA " + deltaPosition);
                    Debug.Log("DELTA FIRST " + firstDeltaPosition);
                    if (deltaPosition > 1)
                    {
                        Debug.Log("!!!" + deltaPosition);
                    }
                    speed = (firstDeltaPosition / deltaPosition) * speed;
                }
                else firstDeltaPosition = deltaPosition;
            }
            */

            t += speed;
            
            while (t > 1)
            {
                t--;
                i = (i + 1) % nodes.Count;
            }
        }
    }
}
