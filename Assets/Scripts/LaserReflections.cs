using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LaserReflections : MonoBehaviour
{
	public int MaxBounces = 4;

	private void OnDrawGizmos()
	{

		DrawLaser(transform.position, transform.forward);
		
	}

	void DrawMiss(Vector3 startPosition, Vector3 Angle)
	{
		Gizmos.color = Color.white;
		Gizmos.DrawLine(startPosition, startPosition + Angle * 10);
	}
	void DrawLaser(Vector3 position, Vector3 angle)
	{
		RaycastHit hitThing;

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

			//Vector3 isThisTheReflection = thatWeirdDotAngle * -2;

			Gizmos.color = Color.red;
			Gizmos.DrawLine(position, hitThing.point);
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(hitPoint, hitPoint + normal);
			Gizmos.color = Color.green;
			Gizmos.DrawLine(hitPoint + angle, (hitPoint - thatWeirdDotAngle * 2 + angle));
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(hitPoint, hitPoint + angle - thatWeirdDotAngle * 2);
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(hitPoint, hitPoint + thatWeirdDotAngle);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(hitPoint, hitPoint + angle);
			position = hitPoint;
			angle = angle + thatWeirdDotAngle * -2;

		}
	}
}