using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LaserReflections : MonoBehaviour
{
	public int MaxBounces = 4;

	private void OnDrawGizmos()
	{

		if (Physics.Raycast(transform.position, transform.forward, 1000f))
		{
			DrawLaser(transform.position, transform.forward, 0);
		}
		else
		{
			DrawMiss();
		}
		
	}

	void DrawMiss()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 10));
	}
	void DrawLaser(Vector3 position, Vector3 rotation, int maxBounces)
	{
		if(maxBounces >= MaxBounces)
		{
			return;
		}
        //for (int i = 0; i < maxBounces; i++)
        //{

        //}
		RaycastHit hitThing;
		Physics.Raycast(position, rotation, out hitThing, 1000f);
		Vector3 normal = hitThing.normal;
		Vector3 hitPoint = hitThing.point;
		//I can feel my brain growing
		Vector3 thatWeirdDotAngle = Vector3.Dot(normal, rotation) * normal;

		//Vector3 isThisTheReflection = thatWeirdDotAngle * -2;

		Gizmos.color = Color.red;
		Gizmos.DrawLine(position, hitThing.point);
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(hitPoint, hitPoint + normal);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(hitPoint + rotation, (hitPoint - thatWeirdDotAngle * 2 + rotation));
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(hitPoint, hitPoint + rotation - thatWeirdDotAngle * 2);
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(hitPoint, hitPoint + thatWeirdDotAngle);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(hitPoint, hitPoint + rotation);
	}
}