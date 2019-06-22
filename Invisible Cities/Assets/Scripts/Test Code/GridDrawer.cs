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
    [SerializeField] Vector2Int tilesPerChunk;
    [SerializeField] float tileWorldSize = 1;
    [SerializeField] Color color = Color.black;

    public float TileWorldSize { get => this.tileWorldSize; set => this.tileWorldSize = value; }
    public Vector2Int TilesPerChunk { get => this.tilesPerChunk; set => this.tilesPerChunk = value; }

    public void Generate () {
        Vector3 worldSize = ((Vector2) this.tilesPerChunk).ToHorizontalVector3 () * this.tileWorldSize;
        worldSize.y = 0;

        Mesh mesh = new Mesh ();
        List<Vector3> verticies = new List<Vector3> ();

        List<int> indicies = new List<int> ();
        int i;
        for (i = 0; i <= TilesPerChunk.x; i++) {
            verticies.Add (new Vector3 (i * TileWorldSize, 0, 0) - worldSize / 2);
            verticies.Add (new Vector3 (i * TileWorldSize, 0, TileWorldSize * TilesPerChunk.y) - worldSize / 2);

            indicies.Add (2 * i + 0);
            indicies.Add (2 * i + 1);
        }

        for (int j = 0; j <= TilesPerChunk.y; j++) {
            verticies.Add (new Vector3 (0, 0, j * TileWorldSize) - worldSize / 2);
            verticies.Add (new Vector3 (TileWorldSize * TilesPerChunk.x, 0, j * TileWorldSize) - worldSize / 2);

            indicies.Add (2 * i + 2 * j + 0);
            indicies.Add (2 * i + 2 * j + 1);
        }

        mesh.vertices = verticies.ToArray ();
        mesh.SetIndices (indicies.ToArray (), MeshTopology.Lines, 0);
        this.filter.mesh = mesh;

        this.meshRenderer.material.color = this.color;
    }
}