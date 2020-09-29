using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflections : MonoBehaviour
{
	public int MaxBounces = 4;
	public LineRenderer LaserLines;
	public Vector3[] Laserpoints = new Vector3[5];


  //  private void Start()
  //  {
		//DrawLaser(transform.position, transform.forward);
		//LaserLines.SetPositions(Laserpoints);
  //  }
	private void OnDrawGizmos()
	{

		DrawLaser(transform.position, transform.forward);

		
	}

	void DrawLaser(Vector3 position, Vector3 angle)
	{
		RaycastHit hitThing;
		Laserpoints[4] = position;
		for (int i = 0; i < MaxBounces; i++)
        {
			if (!Physics.Raycast(position, angle, out hitThing, 1000f))
            {
				DrawMiss(position, angle);
				return;
            }
			Vector3 normal = hitThing.normal;
			Vector3 hitPoint = hitThing.point;
			//I can feel my brain growing
			Vector3 thatWeirdDotAngle = Vector3.Dot(normal, angle) * normal;
			Vector3 theReflection = thatWeirdDotAngle * -2;
			DrawThemGizmos(position, hitPoint, hitThing.normal, angle, theReflection, thatWeirdDotAngle);
			Laserpoints[3 - i] = hitPoint;
			position = hitPoint;
			angle += theReflection;

		}
	}

	void DrawMiss(Vector3 startPosition, Vector3 Angle)
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(startPosition, startPosition + Angle * 10);
	}

	//Gaze upon my pride and joy.
	void DrawThemGizmos(Vector3 originPosition, Vector3 rayHitPosition, Vector3 normal, Vector3 initialAngle, Vector3 ReflectionAngle, Vector3 DotProductAngle)
    {
		Gizmos.color = Color.red;
		Gizmos.DrawLine(originPosition, rayHitPosition);
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(rayHitPosition, rayHitPosition + normal);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(rayHitPosition + initialAngle, (rayHitPosition + ReflectionAngle + initialAngle));
		Gizmos.color = Color.red;
		Gizmos.DrawLine(rayHitPosition, rayHitPosition + initialAngle + ReflectionAngle);
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(rayHitPosition, rayHitPosition + DotProductAngle);
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(rayHitPosition, rayHitPosition + initialAngle);
	}
}