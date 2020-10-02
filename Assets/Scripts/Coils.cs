using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coils : MonoBehaviour
{
    [Range(1, 10)]
    public int Rotations;
    [Range(1, 10)]
    public float Height;
    public float CircleRadius;
    public float SpiralRadius;
    [Min(2)]
    public int Corners;

    public Vector3[] CornerPositions, CirclePositions, CircleRights;

    private void OnDrawGizmos()
    {
        int totalCorners = Corners * Rotations;
        float angle = Mathf.PI * 2 / totalCorners;
        CornerPositions = new Vector3[totalCorners];
        CirclePositions = new Vector3[totalCorners];
        CircleRights = new Vector3[totalCorners];
        for (int i = 0; i < totalCorners; i++)
        {

            Vector3 circlePosition = new Vector3(Mathf.Sin(angle * i) * CircleRadius, 0, Mathf.Cos(angle * i) * CircleRadius);
            CirclePositions[i] = circlePosition;
            CircleRights[i] = circlePosition.normalized;
            CornerPositions[i] = new Vector3(Mathf.Sin(angle * i * Rotations) * SpiralRadius, Mathf.Cos(angle * i * Rotations) * SpiralRadius, 0);


        }

        Gizmos.color = Color.red;
        float lerpValue;

        //Drawing the circle and the local right angle;
        for (int i = 0; i < totalCorners; i++)
        {
            lerpValue = (float)i / totalCorners;
            Gizmos.color = Color.Lerp(Color.cyan, Color.magenta, lerpValue);
            Gizmos.DrawWireSphere(CirclePositions[i], 0.5f);
            Vector3 rightAngle = CircleRights[i];
            Gizmos.DrawLine(CirclePositions[i], CirclePositions[i] + rightAngle);


        }

        for (int i = 0; i < totalCorners; i++)
        {
            lerpValue = (float)i / totalCorners;
            Gizmos.color = Color.Lerp(Color.green, Color.blue, lerpValue);
            Vector3 x = CircleRights[i] * Mathf.Cos(angle * i * Rotations) * SpiralRadius;
            Vector3 y = Vector3.up * Mathf.Sin(angle * i * Rotations) * SpiralRadius;
            Gizmos.DrawWireSphere(CirclePositions[i] + x + y, 0.2f);
            //Gizmos.DrawWireSphere(CornerPositions[i] + Vector3.forward * i + Vector3.forward * 10, 0.5f);
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
