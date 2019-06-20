using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
    [SerializeField] Tile[,] tiles;
    [SerializeField] GridDrawer gridDrawer;
    [SerializeField] Transform ground;

    public void SetToSize(float cellSize, int totalCells) {
        ground.transform.localScale = Vector3.one * cellSize * totalCells;

        tiles = new Tile[totalCells, totalCells];

        gridDrawer.CellWorldSize = cellSize;
        gridDrawer.TotalCells = totalCells;
        gridDrawer.Generate ();
    }
}
