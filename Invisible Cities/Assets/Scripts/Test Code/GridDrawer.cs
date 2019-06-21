using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (MeshRenderer))]
[RequireComponent (typeof (MeshFilter))]
public class GridDrawer : MonoBehaviour {
    [Header ("Object References")]
    [SerializeField] MeshFilter filter = null;
    [SerializeField] MeshRenderer meshRenderer = null;

    [Header ("Settings")]
    [SerializeField] int tilesPerChunk = 100;
    [SerializeField] float tileWorldSize = 1;
    [SerializeField] Color color = Color.black;

    public float TileWorldSize { get => this.tileWorldSize; set => this.tileWorldSize = value; }
    public int TilesPerChunk { get => this.tilesPerChunk; set => this.tilesPerChunk = value; }

    public void Generate () {
        Vector3 worldSize = Vector3.one * this.tilesPerChunk * this.tileWorldSize;
        worldSize.y = 0;

        Mesh mesh = new Mesh ();
        List<Vector3> verticies = new List<Vector3> ();

        List<int> indicies = new List<int> ();
        for (int i = 0; i <= TilesPerChunk; i++) {
            verticies.Add (new Vector3 (i * TileWorldSize, 0, 0) - worldSize / 2);
            verticies.Add (new Vector3 (i * TileWorldSize, 0, TileWorldSize * TilesPerChunk) - worldSize / 2);

            indicies.Add (4 * i + 0);
            indicies.Add (4 * i + 1);

            verticies.Add (new Vector3 (0, 0, i * TileWorldSize) - worldSize / 2);
            verticies.Add (new Vector3 (TileWorldSize * TilesPerChunk, 0, i * TileWorldSize) - worldSize / 2);

            indicies.Add (4 * i + 2);
            indicies.Add (4 * i + 3);
        }

        mesh.vertices = verticies.ToArray ();
        mesh.SetIndices (indicies.ToArray (), MeshTopology.Lines, 0);
        this.filter.mesh = mesh;

        this.meshRenderer.material.color = this.color;
    }
}