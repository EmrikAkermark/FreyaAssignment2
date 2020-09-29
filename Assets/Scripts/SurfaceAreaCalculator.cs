using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceAreaCalculator : MonoBehaviour
{
    public MeshFilter MeshExample;
    private Mesh theMesh;
    public float SurfaceArea;

    public Vector3 Offset;

    //This one was a bust

    private void OnValidate()
    {
        theMesh = MeshExample.sharedMesh;
    }

    private void OnDrawGizmos()
    {
        Vector3[] verts = theMesh.vertices;
        int[] tris = theMesh.triangles;
        SurfaceArea = 0f;
        for (int i = 0; i < tris.Length; i += 3)
        {
            Vector3 a = verts[tris[i]];
            Vector3 b = verts[tris[i + 1]];
            Vector3 c = verts[tris[i + 2]];

            
            
            float surfaceArea = Vector3.Cross(b - a, c - a).magnitude;
            SurfaceArea += surfaceArea;
            a += Offset;
            b += Offset;
            c += Offset;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(a, b);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(b, c);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(c, a);
        }
        SurfaceArea /= 2;
    }

}
