using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierGUI : MonoBehaviour
{
    public List<Transform> nodes;
    public bool closed;
    public GameObject point;
    public float circleDiameter;
    public float lineLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nodes = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            nodes.Add(transform.GetChild(i));
        }
    }

    
    public void createNode()
    {
        GameObject node = Instantiate(point, transform);
        node.transform.position = (nodes[0].position + nodes[nodes.Count - 1].position) / 2;
        //nodes.Add(node.transform);
    }

    private void clear()
    {
        foreach (Transform tr in transform.GetComponentsInChildren<Transform>())
        {
            try
            {
                if (tr != transform) DestroyImmediate(tr.gameObject);
            }
            catch (System.Exception e) { }
        }
        //nodes.Clear();
    }

    public void createCircle()
    {
        clear();
        GameObject node = Instantiate(point, transform);
        node.transform.localPosition = new Vector3(0, 0, circleDiameter / 2);
        //nodes.Add(node.transform);
        node.transform.GetChild(1).localPosition = new Vector3(circleDiameter / 2, 0, 0);
        node = Instantiate(point, transform);
        node.transform.localPosition = new Vector3(circleDiameter / 2, 0, 0);
        //nodes.Add(node.transform);
        node.transform.GetChild(1).localPosition = new Vector3(0, 0, 0);
        node = Instantiate(point, transform);
        node.transform.localPosition = new Vector3(0, 0, -circleDiameter / 2);
        //nodes.Add(node.transform);
        node.transform.GetChild(1).localPosition = new Vector3(-circleDiameter / 2, 0, 0);
        node = Instantiate(point, transform);
        node.transform.localPosition = new Vector3(-circleDiameter / 2, 0, 0);
        //nodes.Add(node.transform);
        node.transform.GetChild(1).localPosition = new Vector3(0, 0, 0);
        closed = true;
    }

    public void createLine()
    {
        clear();
        nodes.Clear();
        GameObject node = Instantiate(point, transform);
        node.transform.localPosition = new Vector3(0, 0, -lineLength/2);
        //nodes.Add(node.transform);
        node.transform.GetChild(1).localPosition = new Vector3(0, 0, 0);
        node = Instantiate(point, transform);
        node.transform.localPosition = new Vector3(0, 0, lineLength / 2);
        //nodes.Add(node.transform);
        node.transform.GetChild(1).localPosition = new Vector3(0, 0, 0);
    }

    private void OnDrawGizmos()
    {
        if (nodes.Count >= 2)
        {
            nodes = new List<Transform>();
            for(int i = 0; i<transform.childCount; i++)
            {
                nodes.Add(transform.GetChild(i));
            }
            Transform node1 = nodes[0];
            //while (node1 == null)
            //{
            //    nodes.RemoveAt(0);
            //    node1 = nodes[0];
            //}
            Transform node2 = nodes[1];
            //while (node2 == null)
            //{
            //    nodes.RemoveAt(1);
            //    node2 = nodes[1];
            //}
            int k = 0;

            while (((node1 != nodes[0] || k == 0) && closed) || ((node2 != nodes[0]) && !closed))
            {
                k++;
                Gizmos.color = Color.white;
                Vector3 previousPoint = node1.position;
                for (float i = 0; i <= 30; i++)
                {
                    Vector3 point = Beizer.Coordinates(node1.position, node1.GetChild(node1.childCount - 1).transform.position,
                                                        node2.GetChild(0).transform.position, node2.position, i / 30);
                    Gizmos.DrawLine(previousPoint, point);
                    previousPoint = point;
                }

                DrawAuxiliaryLines(node1);
                node1 = node2;
                node2 = nodes[(k + 1) % nodes.Count];
                //while (node2 == null)
                //{
                //    nodes.RemoveAt((k + 1) % nodes.Count);
                //    node2 = nodes[(k + 1) % nodes.Count];
                //}
            }
            DrawAuxiliaryLines(node1);
        }
    }

    private void DrawAuxiliaryLines(Transform node)
    {
        Gizmos.color = Color.yellow;
        foreach (Transform child in node.GetComponentsInChildren<Transform>())
        {
            Gizmos.DrawLine(node.position, child.position);
        }
    }
}
