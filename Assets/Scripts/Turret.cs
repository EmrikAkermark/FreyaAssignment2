using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float TurretRightMagnitude;

    private void OnDrawGizmos()
    {
        DrawTurretPlacer(transform.position, transform.forward);
    }

    void DrawTurretPlacer(Vector3 origin, Vector3 angle)
    {
        RaycastHit hitThing;
        if (!Physics.Raycast(origin, angle, out hitThing, 1000f))
        {
            DrawMiss(origin, angle);
            return;
        }
        Vector3 turretUp = hitThing.normal;
        Vector3 hitPoint = hitThing.point;
        //I can feel my brain growing
        Vector3 thatWeirdDotAngle = Vector3.Dot(turretUp, angle) * turretUp;
        Vector3 turretForward = Vector3.Normalize(angle - thatWeirdDotAngle);
        Vector3 turretRight = Vector3.Cross(turretUp, turretForward);
        DrawPrep(origin, hitPoint, turretRight, turretUp, turretForward);

        Matrix4x4 turretArea = new Matrix4x4(turretRight, turretUp, turretForward, hitPoint);

        Vector3[] corners = new Vector3[]
        {
 	    // bottom 4 positions:
	    new Vector3( 1, 0, 1 ),
        new Vector3( -1, 0, 1 ),
        new Vector3( -1, 0, -1 ),
        new Vector3( 1, 0, -1 ),
	    // top 4 positions:
	    new Vector3( 1, 2, 1 ),
        new Vector3( -1, 2, 1 ),
        new Vector3( -1, 2, -1 ),
        new Vector3( 1, 2, -1 )
        };

        DrawBox(turretArea, corners);
        DrawTurret(turretArea, new Vector3(1.5f, 1f, 0f));
        DrawTurret(turretArea, new Vector3(-1.5f, 1f, 0f));

    }

    private void DrawTurret(Matrix4x4 turretArea, Vector3 localPosition)
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawLine(turretArea.MultiplyPoint3x4(localPosition), turretArea.MultiplyPoint3x4(localPosition + Vector3.forward * 1.4f));
    }



    private void DrawPrep(Vector3 origin, Vector3 RayHitPosition, Vector3 turretRight, Vector3 TurretUp, Vector3 TurretForward)
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(origin, RayHitPosition);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(RayHitPosition, RayHitPosition + TurretForward);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(RayHitPosition, RayHitPosition + TurretUp);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(RayHitPosition, RayHitPosition + turretRight);
    }

    private void DrawBox(Matrix4x4 turretPlacement, Vector3[] corners)
    {
        for (int i = 0; i < corners.Length; i++)
        {
            Gizmos.DrawSphere(turretPlacement.MultiplyPoint3x4(corners[i]), 0.1f);

        }
    }

    void DrawMiss(Vector3 startPosition, Vector3 Angle)
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(startPosition, startPosition + Angle * 10);
    }
}
