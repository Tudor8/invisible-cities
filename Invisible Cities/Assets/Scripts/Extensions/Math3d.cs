using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math3d : MonoBehaviour {
    /// <summary>
    /// Taken from https://wiki.unity3d.com/index.php/3d_Math_functions
    /// Create a vector of direction "vector" with length "size"
    /// </summary>
    public static Vector3 SetVectorLength (Vector3 vector, float size) {

        //normalize the vector
        Vector3 vectorNormalized = Vector3.Normalize (vector);

        //scale the vector
        return vectorNormalized *= size;
    }

    /// <summary>
    /// Taken from https://wiki.unity3d.com/index.php/3d_Math_functions
    /// Calculate the intersection between a line and a plane. 
    /// </summary>
    /// <param name="linePoint"> The origin of the line. </param>
    /// <param name="lineVec"> The direction of the line. </param>
    /// <param name="planePoint"> Any point on the plane. </param>
    /// <param name="planeNormal"> The direction (normal) of the plane. </param>
    /// <returns> True if the line and plane are not parallel, otherwise false. </returns>
    public static bool IntersectLineAndPlane (Vector3 linePoint, Vector3 lineVec, Vector3 planePoint, Vector3 planeNormal, out Vector3 intersection) {
        float length;
        float dotNumerator;
        float dotDenominator;
        Vector3 vector;
        intersection = Vector3.zero;

        // Calculate the distance between the linePoint and the line-plane intersection point
        dotNumerator = Vector3.Dot ((planePoint - linePoint), planeNormal);
        dotDenominator = Vector3.Dot (lineVec, planeNormal);

        // Line and plane are not parallel
        if (dotDenominator != 0.0f) {
            length = dotNumerator / dotDenominator;

            // Create a vector from the linePoint to the intersection point
            vector = SetVectorLength (lineVec, length);

            // Get the coordinates of the line-plane intersection point
            intersection = linePoint + vector;

            return true;
        }

        // Output not valid
        else {
            return false;
        }
    }

    /// <summary>
    /// Calculate the intersection between a ray and a plane. 
    /// See IntersectLineAndPlane for more details.
    /// </summary>
    /// <returns> True if the ray and plane are not parallel, otherwise false. </returns>
    public static bool IntersectRayAndPlane (Ray ray, Plane plane, out Vector3 intersection) {
        return IntersectLineAndPlane (ray.origin, ray.direction, plane.ClosestPointOnPlane (plane.normal), plane.normal, out intersection);
    }
}
