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
    public Transform Ground { get => this.ground; set => this.ground = value; }

    public void SetToSize (float tileWorldSize, Vector2Int tilesPerChunk) {
        Ground.transform.localScale = new Vector3 (tileWorldSize * tilesPerChunk.x, tileWorldSize * tilesPerChunk.y, 1);

        Tiles = new Tile[tilesPerChunk.y, tilesPerChunk.x];
        for (int i = 0; i < tilesPerChunk.y; i++) {
            for (int j = 0; j < tilesPerChunk.x; j++) {
                this.tiles[i, j] = new Tile ();
            }
        }

        this.gridDrawer.TileWorldSize = tileWorldSize;
        this.gridDrawer.TilesPerChunk = tilesPerChunk;
        this.gridDrawer.Generate ();
    }
}
