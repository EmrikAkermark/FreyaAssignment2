using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LaserReflections : MonoBehaviour
{
	public int MaxBounces = 4;

	private void OnDrawGizmos()
	{
		RaycastHit hitThing;
		if (Physics.Raycast(transform.position, transform.forward, out hitThing, 1000f))
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, hitThing.point);
		}
		else
		{
			DrawMiss();
		}
		void DrawMiss()
		{
				Gizmos.color = Color.green;
				Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 10));
		}
	}

	void DrawLaser(Vector3 position, Vector3 rotation, int bounces)
	{
		if(bounces >= MaxBounces)
		{
			return;
		}
	}
}