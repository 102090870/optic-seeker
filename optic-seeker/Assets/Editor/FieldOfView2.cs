using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyFOVAdvanced2))]
public class FieldOfView2 : Editor
{
    private void OnSceneGUI()
    {
        EnemyFOVAdvanced2 fov = (EnemyFOVAdvanced2)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewangle01 = DirectionFormAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewangle02 = DirectionFormAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewangle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewangle02 * fov.radius);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFormAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
