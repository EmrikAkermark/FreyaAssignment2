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

        for (int i = 0; i < corners.Length; i++)
        {
            Gizmos.DrawSphere(corners[i], 0.1f);
        }
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

    void DrawMiss(Vector3 startPosition, Vector3 Angle)
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(startPosition, startPosition + Angle * 10);
    }
}
