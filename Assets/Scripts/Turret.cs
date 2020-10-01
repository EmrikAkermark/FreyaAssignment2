using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float BarrelDistance = 1.5f;
    public float BarrelRadius = 0.1f;
    public float BarrelHeight = 1.5f;
    public float BarrelLength = 1f;
    public float SpinSpeed;
    private float speen;

    [Range(1, 10)]
    public int Barrels;

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
        if (Barrels == 1)
        {
            DrawTurret(turretArea, new Vector3(BarrelDistance / 2, BarrelHeight, 0f));
            DrawTurret(turretArea, new Vector3(BarrelDistance / -2, BarrelHeight, 0f));
        }
        else
        {
            DrawMultiBarrel(turretArea, new Vector3(BarrelDistance / 2, BarrelHeight, 0f));
            DrawMultiBarrel(turretArea, new Vector3(BarrelDistance / -2, BarrelHeight, 0f));
        }
        speen += Time.deltaTime * SpinSpeed;
        if (speen > 2 * Mathf.PI)
        {
            speen %= (Mathf.PI * 2);
        }

    }

    private void DrawTurret(Matrix4x4 turretArea, Vector3 localPosition)
    {
        Gizmos.color = Color.white;
        Vector3 barrelPosition = turretArea.MultiplyPoint3x4(localPosition);
        Gizmos.DrawLine(barrelPosition, barrelPosition + Vector3.forward * BarrelLength);
        Gizmos.DrawWireSphere(barrelPosition, 0.03f);
        Gizmos.DrawWireSphere(barrelPosition + Vector3.forward * BarrelLength, 0.03f);
        Gizmos.DrawWireSphere(turretArea.MultiplyPoint3x4(localPosition), 0.15f);

    }

    private void DrawMultiBarrel(Matrix4x4 turretArea, Vector3 localPosition)
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(turretArea.MultiplyPoint3x4(localPosition), 0.15f);

        float angle = Mathf.PI * 2 / Barrels;
        for (int i = 0; i < Barrels; i++)
        {

            Vector3 cornerPosition = new Vector3(Mathf.Sin(angle * i + speen) * BarrelRadius * 0.1f, Mathf.Cos(angle * i + speen) * BarrelRadius * 0.1f);
            Vector3 barrelPosition = localPosition + cornerPosition;
            barrelPosition = turretArea.MultiplyPoint3x4(barrelPosition);
            Gizmos.DrawLine(barrelPosition, barrelPosition + Vector3.forward * BarrelLength);
            Gizmos.DrawWireSphere(barrelPosition, 0.03f);
            Gizmos.DrawWireSphere(barrelPosition + Vector3.forward * BarrelLength, 0.03f);
        }

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
}
