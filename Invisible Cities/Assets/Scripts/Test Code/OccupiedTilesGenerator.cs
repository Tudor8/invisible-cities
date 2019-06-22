using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupiedTilesGenerator : MonoBehaviour {
    [Header ("Object References")]
    [SerializeField] List<BoxCollider> groundColliders = null;

    [Header ("Settings")]
    [SerializeField] float tileWorldSize = 0.5f;
    [SerializeField] int totalTiles = 8;

    [Header ("Read Only")]
    [SerializeField] bool[,] occupiedTiles;

    public float TileWorldSize { get => this.tileWorldSize; set => this.tileWorldSize = value; }
    public int TotalTiles { get => this.totalTiles; set => this.totalTiles = value; }
    public bool[,] OccupiedTiles { get => this.occupiedTiles; set => this.occupiedTiles = value; }

    public Vector3 GetTopLeftTileLocalPosition () {
        float position = (TotalTiles - 1) / 2f * TileWorldSize;

        Vector3 tile = new Vector3 (-position, 0, position);

        return tile;
    }

    public Vector3 GetTopLeftTileWorldPosition () {
        return transform.TransformPoint (GetTopLeftTileLocalPosition ());
    }

    public void CalculateTiles () {
        this.OccupiedTiles = new bool[TotalTiles, TotalTiles];

        Vector3 startingPosition = GetTopLeftTileLocalPosition ();

        Vector3 current = startingPosition;
        for (int i = 0; i < TotalTiles; i++) {
            for (int j = 0; j < TotalTiles; j++) {
                Vector3 position = transform.TransformPoint (current);

                this.OccupiedTiles[i, j] = DoCollidersContainPoint (position);

                current.x += TileWorldSize;
            }
            current.x = startingPosition.x;
            current.z -= TileWorldSize;
        }
    }

    private bool DoCollidersContainPoint (Vector3 position) {
        foreach (BoxCollider collider in this.groundColliders) {
            if (collider.bounds.Contains (position)) {
                return true;
            }
        }
        return false;
    }
}
