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

    public bool isTromb;

    private float time;

    private void OnDrawGizmos()
    {



        if (isTromb)
        {
            DrawTorus();
        }
        else
        {
            DrawHelix();
        }
        

    }


    void DrawHelix()
    {
        int totalCorners = Corners * Rotations;
        float angle = Mathf.PI * 2 / Corners;
        Vector3[] CirclePositions = new Vector3[totalCorners];

        for (int i = 0; i < totalCorners; i++)
        {
            CirclePositions[i] = new Vector3(Mathf.Sin(angle * i) * CircleRadius, i * Height / Corners, Mathf.Cos(angle * i) * CircleRadius);
        }

        float lerpValue;
        for (int i = 0; i < totalCorners; i++)
        {
            lerpValue = (float)i / totalCorners;
            Gizmos.color = Color.Lerp(Color.green, Color.blue, lerpValue);
            Gizmos.DrawWireSphere(CirclePositions[i], 0.2f);
        }

        for (int i = 0; i < totalCorners - 1; i++)
        {
            int start = i % totalCorners;
            int next = (i + 1) % totalCorners;
            lerpValue = (float)i / totalCorners;

            Gizmos.color = Color.Lerp(Color.blue, Color.green, lerpValue);
            Gizmos.DrawLine(CirclePositions[start], CirclePositions[next]);
        }
    }

    private void DrawTorus()
    {
        if (time > 2 * Mathf.PI)
        {
            time %= 2 * Mathf.PI;
        }
        time += Time.deltaTime * 0.4f;
        int totalCorners = Corners * Rotations;
        float angle = Mathf.PI * 2 / totalCorners;
        Vector3[] TorusPositions = new Vector3[totalCorners];
        Vector3[] CirclePositions = new Vector3[totalCorners];
        Vector3[] CircleRights = new Vector3[totalCorners];
        for (int i = 0; i < totalCorners; i++)
        {

            Vector3 circlePosition = new Vector3(Mathf.Sin(angle * i ) * CircleRadius, 0, Mathf.Cos(angle * i) * CircleRadius);
            CirclePositions[i] = circlePosition;
            CircleRights[i] = circlePosition.normalized;
            TorusPositions[i] = (CircleRights[i] * Mathf.Cos(angle * i * Rotations + time) + Vector3.up * Mathf.Sin(angle * i * Rotations + time)) * SpiralRadius;
        }

        Gizmos.color = Color.red;
        float lerpValue;

        //Drawing the circle and the local right angle;
        for (int i = 0; i < totalCorners; i++)
        {
            lerpValue = (float)i / totalCorners;
            Gizmos.color = Color.Lerp(Color.cyan, Color.magenta, lerpValue);
            Gizmos.DrawWireSphere(CirclePositions[i], 0.1f);
            Vector3 rightAngle = CircleRights[i];
            Gizmos.DrawLine(CirclePositions[i], CirclePositions[i] + rightAngle * 0.4f);
        }

        //Draws the torus positions
        for (int i = 0; i < totalCorners; i++)
        {
            lerpValue = (float)i / totalCorners;
            Gizmos.color = Color.Lerp(Color.green, Color.blue, lerpValue);
            Gizmos.DrawWireSphere(CirclePositions[i] + TorusPositions[i], 0.2f);
        }

        //Draws the lines betwen the points
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
