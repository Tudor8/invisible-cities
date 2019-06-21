using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTest : MonoBehaviour {
    [SerializeField] SnapToGrid snapToGrid = null;
    [SerializeField] SnapToLayer snapToLayer = null;
    [SerializeField] bool[,] occupiedTiles;
    [SerializeField] float cellSize = 0.5f;
    [SerializeField] int totalTiles = 8;
    [SerializeField] List<BoxCollider> groundColliders;

    public SnapToGrid SnapToGrid { get => this.snapToGrid; set => this.snapToGrid = value; }
    public SnapToLayer SnapToLayer { get => this.snapToLayer; set => this.snapToLayer = value; }
    public int TotalTiles { get => this.totalTiles; set => this.totalTiles = value; }

    void Awake() {
        CalculateTiles ();
    }

    public void CalculateTiles () {
        occupiedTiles = new bool[TotalTiles, TotalTiles];

        Vector3 startingPosition = GetBottomLeftTilePosition ();

        Vector3 actualPoint = transform.TransformPoint (startingPosition);
        Debug.Log (string.Format("Starting Point = [{0}][{1}]", actualPoint.x, actualPoint.z));

        Vector3 current = startingPosition;
        for (int i = 0; i < TotalTiles; i++) {
            for (int j = 0; j < TotalTiles; j++) {
                Vector3 position = transform.TransformPoint (current);

                foreach (BoxCollider collider in groundColliders) {
                    if (collider.bounds.Contains (position)) {
                        occupiedTiles[j, i] = true;
                        break;
                    }
                }

                current.x += cellSize;
            }
            current.x = startingPosition.x;
            current.z += cellSize;
        }

        for (int i = 0; i < TotalTiles; i++) {
            string s = "";
            for (int j = 0; j < TotalTiles; j++) {
                s += occupiedTiles[i, j];
            }
            //Debug.Log (s);
        }
    }

    public Vector3 GetBottomLeftTilePosition() {
        Vector3 tile = -Vector3.one * (TotalTiles - 1) / 2 * cellSize;
        tile.y = 0;

        return tile;
    }

    public void Activate () {
        this.snapToGrid.enabled = true;
        this.snapToLayer.enabled = true;
    }

    public void Deactivate () {
        this.snapToGrid.enabled = false;
        this.snapToLayer.enabled = false;
    }
}
