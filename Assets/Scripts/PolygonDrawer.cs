using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonDrawer : MonoBehaviour
{
    [Range(2, 10)]
    public int Corners = 2;
    [Range(1, 10)]
    public int skip;

    public float size = 2f;

    private void OnDrawGizmos()
    {
        float angle = Mathf.PI * 2 / Corners;
        Vector2[] CornerPositions = new Vector2[Corners];
        for (int i = 0; i < Corners; i++)
        {

            Vector2 cornerPosition = new Vector2(Mathf.Sin(angle * i) * size, Mathf.Cos(angle * i) * size);
            CornerPositions[i] = cornerPosition;
        }

        for (int i = 0; i < Corners; i++)
        {
            Gizmos.color = NewColor(i);
            Gizmos.DrawSphere(CornerPositions[i], 0.1f);
        }
        for (int i = 0; i < Corners; i++)
        {
            int start = i % Corners;
            int next = (i + skip) % Corners;

            Gizmos.color = NewColor(i);
            Gizmos.DrawLine(CornerPositions[start], CornerPositions[next]);
        }

    }

    private Color NewColor(int step)
    {


        switch (step % 5)
        {
            case 0:
                return Color.red;
            case 1:
                return Color.green;
            case 2:
                return Color.blue;
            case 3:
                return Color.magenta;
            case 4:
                return Color.yellow;
            default:
                return Color.white;
        }
    }
}
