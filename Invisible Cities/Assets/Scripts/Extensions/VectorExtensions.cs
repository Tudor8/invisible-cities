using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions {
    /// <summary>
    /// Returns the closest (rounded) vector that is a multiple of the argument.
    /// </summary>
    public static Vector3 RoundToMultipleOf(this Vector3 vector, float multiple) {
        return new Vector3(
            vector.x.RoundToMultipleOf(multiple),
            vector.y.RoundToMultipleOf(multiple),
            vector.z.RoundToMultipleOf(multiple)
        );
    }

    /// <summary>
    /// Returns the closest (rounded) vector that is a multiple of the argument.
    /// </summary>
    /// <param name="shouldRound"> Allows you to specify which axis should be rounded. A value != 0 means that it should be rounded. </param>
    public static Vector3 RoundToMultipleOf(this Vector3 vector, float multiple, Vector3Int shouldRound) {
        Vector3 result = vector;

        if (shouldRound[0] != 0) result.x = vector.x.RoundToMultipleOf(multiple);
        if (shouldRound[1] != 0) result.y = vector.y.RoundToMultipleOf(multiple);
        if (shouldRound[2] != 0) result.z = vector.z.RoundToMultipleOf(multiple);
        
        return result;
    }
}
