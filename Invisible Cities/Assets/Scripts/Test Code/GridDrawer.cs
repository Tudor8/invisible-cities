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
    [SerializeField] int totalCells = 100;
    [SerializeField] float cellWorldSize = 1;
    [SerializeField] Color color = Color.black;

    void Awake () {
        Mesh mesh = new Mesh ();
        List<Vector3> verticies = new List<Vector3> ();

        List<int> indicies = new List<int> ();
        for (int i = 0; i <= this.totalCells; i++) {
            verticies.Add (new Vector3 (i * this.cellWorldSize, 0, 0));
            verticies.Add (new Vector3 (i * this.cellWorldSize, 0, this.cellWorldSize * this.totalCells));

            indicies.Add (4 * i + 0);
            indicies.Add (4 * i + 1);

            verticies.Add (new Vector3 (0, 0, i * this.cellWorldSize));
            verticies.Add (new Vector3 (this.cellWorldSize * this.totalCells, 0, i * this.cellWorldSize));

            indicies.Add (4 * i + 2);
            indicies.Add (4 * i + 3);
        }

        mesh.vertices = verticies.ToArray ();
        mesh.SetIndices (indicies.ToArray (), MeshTopology.Lines, 0);
        this.filter.mesh = mesh;

        this.meshRenderer.material.color = this.color;
    }
}