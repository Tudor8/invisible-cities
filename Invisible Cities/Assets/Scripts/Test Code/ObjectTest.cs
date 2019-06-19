using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTest : MonoBehaviour {
    [SerializeField] SnapToGrid snapToGrid = null;
    [SerializeField] SnapToLayer snapToLayer = null;

    public SnapToGrid SnapToGrid { get => this.snapToGrid; set => this.snapToGrid = value; }
    public SnapToLayer SnapToLayer { get => this.snapToLayer; set => this.snapToLayer = value; }

    public void Activate () {
        this.snapToGrid.enabled = true;
        this.snapToLayer.enabled = true;
    }

    public void Deactivate () {
        this.snapToGrid.enabled = false;
        this.snapToLayer.enabled = false;
    }
}
