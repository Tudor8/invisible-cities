using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class GridDrawer : MonoBehaviour {
    [SerializeField] MeshFilter filter = null;
    [SerializeField] MeshRenderer meshRenderer = null;

    [SerializeField] int totalCells = 100;
    [SerializeField] float cellWorldSize = 1;
    [SerializeField] Color color = Color.black;

    void Awake() {
        var mesh = new Mesh();
        var verticies = new List<Vector3>();

        var indicies = new List<int>();
        for (int i = 0; i <= totalCells; i++) {
            verticies.Add(new Vector3(i * cellWorldSize, 0, 0));
            verticies.Add(new Vector3(i * cellWorldSize, 0, cellWorldSize * totalCells));

            indicies.Add(4 * i + 0);
            indicies.Add(4 * i + 1);

            verticies.Add(new Vector3(0, 0, i * cellWorldSize));
            verticies.Add(new Vector3(cellWorldSize * totalCells, 0, i * cellWorldSize));

            indicies.Add(4 * i + 2);
            indicies.Add(4 * i + 3);
        }

        mesh.vertices = verticies.ToArray();
        mesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
        filter.mesh = mesh;

        meshRenderer.material.color = color;
    }
}