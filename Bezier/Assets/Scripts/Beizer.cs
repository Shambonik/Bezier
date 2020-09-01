using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Beizer
{
    public static Vector3 Coordinates(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float oneMinusT = 1 - t;
        return Mathf.Pow(oneMinusT, 3) * p0 + 3 * Mathf.Pow(oneMinusT, 2) 
            * t * p1 + 3 * oneMinusT * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
    }

    public static Vector3 firstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float oneMinusT = 1 - t;
        return 3 * Mathf.Pow(oneMinusT, 2) * (p1 - p0) + 6 * 
            oneMinusT * t * (p2 - p1) + 3 * Mathf.Pow(t, 2) * (p3 - p2);
    }
}
