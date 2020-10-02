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

    private void OnDrawGizmos()
    {
        int totalCorners = Corners * Rotations;
        float angle = Mathf.PI * 2 / totalCorners;
        Vector3[] TorusPositions = new Vector3[totalCorners];
        Vector3[] CirclePositions = new Vector3[totalCorners];
        Vector3[] CircleRights = new Vector3[totalCorners];
        for (int i = 0; i < totalCorners; i++)
        {

            Vector3 circlePosition = new Vector3(Mathf.Sin(angle * i) * CircleRadius, 0, Mathf.Cos(angle * i) * CircleRadius);
            CirclePositions[i] = circlePosition;
            CircleRights[i] = circlePosition.normalized;
            TorusPositions[i] = (CircleRights[i] * Mathf.Cos(angle * i * Rotations) + Vector3.up * Mathf.Sin(angle * i * Rotations)) * SpiralRadius;


        }

        Gizmos.color = Color.red;
        float lerpValue;

        //Drawing the circle and the local right angle;
        for (int i = 0; i < totalCorners; i++)
        {
            lerpValue = (float)i / totalCorners;
            Gizmos.color = Color.Lerp(Color.cyan, Color.magenta, lerpValue);
            Gizmos.DrawWireSphere(CirclePositions[i], 0.3f);
            Vector3 rightAngle = CircleRights[i];
            Gizmos.DrawLine(CirclePositions[i], CirclePositions[i] + rightAngle);


        }

        for (int i = 0; i < totalCorners; i++)
        {
            lerpValue = (float)i / totalCorners;
            Gizmos.color = Color.Lerp(Color.green, Color.blue, lerpValue);
            Gizmos.DrawWireSphere(CirclePositions[i] + TorusPositions[i], 0.2f);
            //Gizmos.DrawWireSphere(CornerPositions[i] + Vector3.forward * i + Vector3.forward * 10, 0.5f);
        }

        for (int i = 0; i < totalCorners; i++)
        {
            int start = i % totalCorners;
            int next = (i + 1) % totalCorners;
            lerpValue = (float)i / totalCorners;
            Gizmos.color = Color.Lerp(Color.red, Color.green, lerpValue);
            Gizmos.DrawLine(TorusPositions[start], TorusPositions[next]);
            Gizmos.color = Color.Lerp(Color.blue, Color.green, lerpValue);
            Gizmos.DrawLine(CirclePositions[start] + TorusPositions[start], CirclePositions[next] + TorusPositions[next]);


        }
    }
}
