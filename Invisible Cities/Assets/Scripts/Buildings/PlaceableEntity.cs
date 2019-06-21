using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableEntity : MonoBehaviour {
    [Header("Object References")]
    [SerializeField] SnapToGrid snapToGrid = null;
    [SerializeField] SnapToLayer snapToLayer = null;
    [SerializeField] OccupiedTilesGenerator tileGenerator = null;

    [Header("Settings")]
    [SerializeField] float tileWorldSize = 0.5f;
    [SerializeField] int totalTiles = 8;

    public SnapToGrid SnapToGrid { get => this.snapToGrid; set => this.snapToGrid = value; }
    public SnapToLayer SnapToLayer { get => this.snapToLayer; set => this.snapToLayer = value; }
    public OccupiedTilesGenerator TileGenerator { get => this.tileGenerator; set => this.tileGenerator = value; }
    public int TotalTiles { get => this.totalTiles; set => this.totalTiles = value; }

    public void Activate () {
        this.snapToGrid.enabled = true;
        this.snapToLayer.enabled = true;
    }

    public void Deactivate () {
        this.snapToGrid.enabled = false;
        this.snapToLayer.enabled = false;
    }

    void Awake() {
        SetUpComponents ();
    }

    void OnValidate() {
        SetUpComponents ();
        tileGenerator.CalculateTiles ();
    }

    private void SetUpComponents() {
        SetUpTileGenerator ();
        SetUpSnapToGrid ();
    }

    private void SetUpTileGenerator() {
        tileGenerator.TileWorldSize = tileWorldSize;
        tileGenerator.TotalTiles = totalTiles;
    }

    private void SetUpSnapToGrid() {
        snapToGrid.TileWorldSize = tileWorldSize;
        snapToGrid.TotalTiles = totalTiles;
    }
}
