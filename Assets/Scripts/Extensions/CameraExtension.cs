using UnityEngine;

/// <summary author="Koen Sparreboom">
/// Extra methods for the Camera class
/// </summary>
public static class CameraExtension {
    public static bool IsColliderInView(this Camera cam, Collider col) {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        
        return GeometryUtility.TestPlanesAABB(planes, col.bounds);
    }
}