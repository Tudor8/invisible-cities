using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour {
    public static readonly Vector3Int GRID_AXIS = new Vector3Int (1, 0, 1);

    [Header ("Object References")]
    [SerializeField] Transform targetObject = null;

    [Header ("Settings")]
    [SerializeField] float cellSize = 1;
    [SerializeField] int spaceTaken = 1;

    public float CellSize { get => this.cellSize; set => this.cellSize = value; }
    public int SpaceTaken { get => this.spaceTaken; set => this.spaceTaken = value; }

    void LateUpdate () {
        this.targetObject.position = GetGridPositionAt (transform.position);
    }

    public Vector3 GetGridPositionAt (Vector3 location) {
        Vector3 position = location;

        Vector3 offset = ((Vector3) GRID_AXIS) * (this.cellSize / 2f);

        if (this.spaceTaken % 2 == 1)
            position += offset;

        position = position.RoundToMultipleOf (this.cellSize, GRID_AXIS);

        if (this.spaceTaken % 2 == 1)
            position -= offset;

        return position;
    }

    private void test() {
        //position = position.RoundToMultipleOf (this.cellSize, GRID_AXIS);
    }
}
