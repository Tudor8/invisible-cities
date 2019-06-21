using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
    [Header ("Object References")]
    [SerializeField] GridDrawer gridDrawer;
    [SerializeField] Transform ground;

    [Header ("Read Only")]
    [SerializeField] Tile[,] tiles;

    public void SetToSize(float tileWorldSize, int tilesPerChunk) {
        ground.transform.localScale = Vector3.one * tileWorldSize * tilesPerChunk;

        tiles = new Tile[tilesPerChunk, tilesPerChunk];

        gridDrawer.TileWorldSize = tileWorldSize;
        gridDrawer.TilesPerChunk = tilesPerChunk;
        gridDrawer.Generate ();
    }
}
