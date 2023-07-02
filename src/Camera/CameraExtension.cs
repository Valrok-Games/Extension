using UnityEngine;

namespace Valrok.Extension.Camera
{
    public static class CameraExtension
    {
        /// <summary>
        /// Returns true if object is within camera view plane
        /// </summary>
        /// <param name="this">The camera</param>
        /// <param name="renderer"></param>
        public static bool IsObjectVisible(this UnityEngine.Camera @this, Renderer renderer)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(@this), renderer.bounds);
        }
    }
}
