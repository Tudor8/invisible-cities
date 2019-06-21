using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour {
    public static readonly Vector3Int GRID_AXIS = new Vector3Int (1, 0, 1);

    [Header ("Object References")]
    [SerializeField] Transform targetObject = null;

    [Header ("Settings")]
    [SerializeField] float tileWorldSize = 0.5f;
    [SerializeField] int totalTiles = 8;

    public float TileWorldSize { get => this.tileWorldSize; set => this.tileWorldSize = value; }
    public int TotalTiles { get => this.totalTiles; set => this.totalTiles = value; }

    void LateUpdate () {
        this.targetObject.position = GetGridPositionAt (transform.position);
    }

    public Vector3 GetGridPositionAt (Vector3 location) {
        Vector3 position = location;

        Vector3 offset = ((Vector3) GRID_AXIS) * (this.tileWorldSize / 2f);

        if (this.totalTiles % 2 == 1)
            position += offset;

        position = position.RoundToMultipleOf (this.tileWorldSize, GRID_AXIS);

        if (this.totalTiles % 2 == 1)
            position -= offset;

        return position;
    }
}
