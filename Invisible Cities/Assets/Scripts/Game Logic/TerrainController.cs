using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {
    public static readonly Vector2Int sizeLimits = new Vector2Int (1, 1000);

    [SerializeField] GameObject chunkPrefab;

    [SerializeField] Vector2Int chunkCount;
    [SerializeField] int chunkSize = 40;
    [SerializeField] float cellSize = 0.5f;

    Chunk[,] chunks;

    void OnValidate () {
        this.chunkCount.Clamp (Vector2Int.one, Vector2Int.one * 1000);
    }

    void Awake () {
        this.chunks = new Chunk[this.chunkCount.x, this.chunkCount.y];

        Vector3 position = Vector3.zero;

        float chunkWorldSize = this.chunkSize * this.cellSize;

        for (int i = 0; i < this.chunkCount.x; i++) {
            for (int j = 0; j < this.chunkCount.y; j++) {

                GameObject chunkObj = Instantiate (this.chunkPrefab, position, Quaternion.identity);

                Chunk chunkScript = chunkObj.GetComponent<Chunk> ();
                this.chunks[i, j] = chunkScript;
                chunkScript.SetToSize (this.cellSize, this.chunkSize);

                position += Vector3.right * chunkWorldSize;
            }
            position.x = Vector3.zero.x;
            position -= Vector3.forward * chunkWorldSize;
        }

        Vector3 averagePosition = new Vector3 (chunkWorldSize * (this.chunkCount.x - 1), 0, -chunkWorldSize * (this.chunkCount.y - 1));
        averagePosition = averagePosition.DivideBy (Vector3.one * 2);
        transform.position = averagePosition;

        for (int i = 0; i < this.chunkCount.x; i++) {
            for (int j = 0; j < this.chunkCount.y; j++) {
                this.chunks[i, j].transform.SetParent (transform);
            }
        }

        transform.position = Vector3.zero;
    }
}
