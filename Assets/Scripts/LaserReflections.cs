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
	void DrawLaser(Vector3 position, Vector3 rotation, int bounces)
	{
		if(bounces >= MaxBounces)
		{
			return;
		}
		RaycastHit hitThing;
		Physics.Raycast(position, rotation, out hitThing, 1000f);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(position, hitThing.point);



	}
}