using UnityEngine;

public class DebugVisualizer
{

    public static void DrawBoxCast(Vector3 start, Vector3 end, Vector3 size, Quaternion rotation, float maxDistance)
    {
        Gizmos.color = Color.green;

        // Cache the Gizmos matrix.
        Matrix4x4 currentMatrix = Gizmos.matrix;

        // Draw Cubes
        Gizmos.matrix = Matrix4x4.TRS(start, rotation, size);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix4x4.TRS(end, rotation, size);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        // Draw Connecting Lines
        Vector3 x = rotation * (Vector3.right * size.x * 0.5f);  // Apply object rotation
        Vector3 y = rotation * (Vector3.up * size.y * 0.5f);
        Vector3 z = rotation * (Vector3.forward * size.z * 0.5f);

        Gizmos.matrix = Matrix4x4.TRS(start, rotation, Vector3.one);
        Vector3 rayDirection = (end - start).normalized; // Adjust to follow object forward

        Gizmos.DrawRay(Vector3.zero - x - y - z, rayDirection * maxDistance);
        Gizmos.DrawRay(Vector3.zero + x - y - z, rayDirection * maxDistance);
        Gizmos.DrawRay(Vector3.zero + x + y - z, rayDirection * maxDistance);
        Gizmos.DrawRay(Vector3.zero - x + y - z, rayDirection * maxDistance);

        // Reset the Gizmos matrix.
        Gizmos.matrix = currentMatrix;
    }

    public static void VisualizeHitPoint(MonoBehaviour monoBehaviour, Vector3 hitPoint, float sphereVisualizeScale, float duration = -1f)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.GetComponent<Collider>().enabled = false;
        obj.transform.localScale = new Vector3(sphereVisualizeScale, sphereVisualizeScale, sphereVisualizeScale);
        obj.transform.position = hitPoint;

        if (duration > 0)
        {
            Util.WaitForSeconds(monoBehaviour, () => Object.Destroy(obj), 1.5f);
        }
    }
}