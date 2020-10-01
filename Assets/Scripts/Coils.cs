using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coils : MonoBehaviour
{
    [Range(1, 10)]
    public int Rotations;
    [Range(1, 10)]
    public float Height;
    public float Radius;
    [Min(2)]
    public int Corners;

    private void OnDrawGizmos()
    {
        int totalCorners = Corners * Rotations;
        float angle = Mathf.PI * 2 / Corners;
        Vector3[] CornerPositions = new Vector3[totalCorners];
        for (int i = 0; i < totalCorners; i++)
        {

            Vector3 cornerPosition = new Vector3(Mathf.Sin(angle * i) * Radius, Mathf.Cos(angle * i) * Radius, Height / Corners * i);
            CornerPositions[i] = cornerPosition;
        }

        Gizmos.color = Color.red;
        float lerpValue;

        for (int i = 0; i < totalCorners; i++)
        {
            lerpValue = (float)i / totalCorners;
            Gizmos.color = Color.Lerp(Color.green, Color.blue, lerpValue);
            Gizmos.DrawWireSphere(CornerPositions[i], 0.5f);
        }

        for (int i = 0; i < totalCorners - 1; i++)
        {
            int start = i % totalCorners;
            int next = (i + 1) % totalCorners;
            lerpValue = (float)i / totalCorners;
            Gizmos.color = Color.Lerp(Color.red, Color.green, lerpValue);

            Gizmos.DrawLine(CornerPositions[start], CornerPositions[next]);
        }

    }
}
