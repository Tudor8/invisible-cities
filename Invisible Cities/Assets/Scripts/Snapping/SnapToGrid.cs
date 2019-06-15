using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour {
    public static readonly Vector3Int gridAxis = new Vector3Int(1, 0, 1);

    [SerializeField] Transform targetObject = null;

    [SerializeField] float cellSize = 1;
    [SerializeField] int spaceTaken = 1;

    public float CellSize { get => cellSize; set => cellSize = value; }
    public int SpaceTaken { get => spaceTaken; set => spaceTaken = value; }

    void LateUpdate() {
        targetObject.position = GetGridPositionAt(transform.position);
    }

    public Vector3 GetGridPositionAt(Vector3 location) {
        Vector3 position = location;

        if (spaceTaken % 2 == 1) position += (Vector3)gridAxis * (cellSize / 2f);
        
        position = position.RoundToMultipleOf(cellSize, gridAxis);

        if (spaceTaken % 2 == 1) position -= (Vector3)gridAxis * (cellSize / 2f);

        return position;
    }
}
