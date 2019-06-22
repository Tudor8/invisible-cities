using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaceableEntity : MonoBehaviour {
    [Header ("Object References")]
    [SerializeField] SnapToGrid snapToGrid = null;
    [SerializeField] SnapToLayer snapToLayer = null;
    [SerializeField] OccupiedTilesGenerator tileGenerator = null;
    [SerializeField] List<MeshRenderer> meshRenderers = null;

    [Header ("Prefabs")]
    [SerializeField] Material validMaterial = null;
    [SerializeField] Material invalidMaterial = null;

    [Header ("Settings")]
    [SerializeField] float tileWorldSize = 0.5f;
    [SerializeField] int totalTiles = 8;

    [Header ("Readonly")]
    [SerializeField, ReadOnly] bool valid;
    [SerializeField, ReadOnly] bool fixated;
    [SerializeField, ReadOnly] Tile[,] currentFrameTiles;

    public SnapToGrid SnapToGrid { get => this.snapToGrid; set => this.snapToGrid = value; }
    public SnapToLayer SnapToLayer { get => this.snapToLayer; set => this.snapToLayer = value; }
    public OccupiedTilesGenerator TileGenerator { get => this.tileGenerator; set => this.tileGenerator = value; }
    public int TotalTiles { get => this.totalTiles; set => this.totalTiles = value; }
    public bool Fixated { get => this.fixated; set => this.fixated = value; }

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
        if (!this.fixated)
            CheckForValidity ();

        foreach (MeshRenderer renderer in this.meshRenderers) {
            renderer.sharedMaterial = IsValid () ? this.validMaterial : this.invalidMaterial;
        }
    }

    private bool IsValid () {
        return this.valid || this.fixated;
    }

    public void Fixate () {
        if (Fixated || !this.valid) {
            return;
        }

        for (int i = 0; i < this.currentFrameTiles.GetLength (0); i++) {
            for (int j = 0; j < this.currentFrameTiles.GetLength (1); j++) {
                if (TileGenerator.OccupiedTiles[i, j]) {
                    this.currentFrameTiles[i, j].Occupied = true;
                }
            }
        }

        this.currentFrameTiles = null;
        Fixated = true;
    }

    private void CheckForValidity () {
        this.valid = false;

        if (ChunkProcessor.Instance.GetTilesOverArea (this.tileGenerator.GetTopLeftTileWorldPosition (), Vector2Int.one * this.totalTiles, out this.currentFrameTiles)) {
            if (this.currentFrameTiles.GetLength (0) != TileGenerator.OccupiedTiles.GetLength (0) || this.currentFrameTiles.GetLength (1) != TileGenerator.OccupiedTiles.GetLength (1)) {
                Debug.LogError ("Tile Generator has an invalid size for tiles.");
            }

            this.valid = true;

            for (int i = 0; i < this.currentFrameTiles.GetLength (0); i++) {
                for (int j = 0; j < this.currentFrameTiles.GetLength (1); j++) {
                    if (TileGenerator.OccupiedTiles[i, j] && this.currentFrameTiles[i, j].Occupied) {
                        this.valid = false;
                        goto LoopEnd;
                    }
                }
            }
        LoopEnd:
            { }
        }
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
