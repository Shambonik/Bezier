using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class NodeScript : MonoBehaviour
{
    Vector3 pointPosition;
    Transform point0;

    // Start is called before the first frame update
    void Start()
    {
        point0 = transform.GetChild(0);
        pointPosition = point0.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 2)
        {
            if (pointPosition != point0.position)
            {
                transform.GetChild(1).position = 2 * transform.position - point0.position;
            }
            else
            {
                point0.position = 2 * transform.position - transform.GetChild(1).position;
            }
            pointPosition = point0.position;
        }
    }
}
