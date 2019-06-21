using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
    [Header ("Object References")]
    [SerializeField] GridDrawer gridDrawer = null;
    [SerializeField] Transform ground = null;

    [Header ("Read Only")]
    [SerializeField] Tile[,] tiles;

    public Tile[,] Tiles { get => this.tiles; set => this.tiles = value; }

    public void SetToSize (float tileWorldSize, int tilesPerChunk) {
        this.ground.transform.localScale = Vector3.one * tileWorldSize * tilesPerChunk;

        Tiles = new Tile[tilesPerChunk, tilesPerChunk];
        for (int i = 0; i < tilesPerChunk; i++) {
            for (int j = 0; j < tilesPerChunk; j++) {
                this.tiles[i, j] = new Tile ();
            }
        }

        this.gridDrawer.TileWorldSize = tileWorldSize;
        this.gridDrawer.TilesPerChunk = tilesPerChunk;
        this.gridDrawer.Generate ();
    }
}
