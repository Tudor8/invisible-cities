using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTest : MonoBehaviour {
    [SerializeField] SnapToGrid snapToGrid = null;

    public SnapToGrid SnapToGrid { get => snapToGrid; set => snapToGrid = value; }

}
