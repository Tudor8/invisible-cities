using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtensions {
    /// <summary>
    /// Returns the closest (rounded) number that is a multiple of the argument.
    /// </summary>
    public static float RoundToMultipleOf(this float number, float multiple) {
        return Mathf.Round(number / multiple) * multiple;
    }
}
