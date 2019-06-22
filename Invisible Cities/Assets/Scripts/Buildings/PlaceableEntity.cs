using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaceableEntity : MonoBehaviour {
    [Header ("Object References")]
    [SerializeField] SnapToGrid snapToGrid = null;
    [SerializeField] SnapToLayer snapToLayer = null;
    [SerializeField] OccupiedTilesGenerator tileGenerator = null;
    [SerializeField] TextMeshProUGUI validText;

    [Header ("Settings")]
    [SerializeField] float tileWorldSize = 0.5f;
    [SerializeField] int totalTiles = 8;

    [Header ("Readonly")]
    [SerializeField, ReadOnly] bool valid;

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

    void Awake () {
        SetUpComponents ();
    }

    void OnValidate () {
        SetUpComponents ();
        this.tileGenerator.CalculateTiles ();
    }

    void Update () {
        this.valid = ChunkProcessor.Instance.GetTilesOverArea (this.tileGenerator.GetTopLeftTileWorldPosition (), Vector2Int.one * this.totalTiles, out Tile[,] tiles);
        this.validText.text = this.valid ? "Valid" : "Invalid";
    }

    private void SetUpComponents () {
        SetUpTileGenerator ();
        SetUpSnapToGrid ();
    }

    private void SetUpTileGenerator () {
        this.tileGenerator.TileWorldSize = this.tileWorldSize;
        this.tileGenerator.TotalTiles = this.totalTiles;
    }

    private void SetUpSnapToGrid () {
        this.snapToGrid.TileWorldSize = this.tileWorldSize;
        this.snapToGrid.TotalTiles = this.totalTiles;
    }
}
