using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {
    public static readonly Vector2Int sizeLimits = new Vector2Int (1, 1000);

    [Header ("Object References")]
    [SerializeField] ChunkProcessor chunkProcessor = null;

    [Header ("Prefabs")]
    [SerializeField] GameObject chunkPrefab = null;

    [Header ("Settings")]
    [SerializeField] Vector2Int chunkCount;
    [SerializeField] Vector2Int tilesPerChunk;
    [SerializeField] float tileWorldSize = 0.5f;

    [Header ("Read Only")]
    [SerializeField, ReadOnly] Vector2 chunkWorldSize;
    [SerializeField, ReadOnly] Chunk[,] chunks;

    public PlaceableEntity testObj;

    public ChunkProcessor ChunkProcessor { get => this.chunkProcessor; set => this.chunkProcessor = value; }

    void Awake () {
        this.chunks = new Chunk[this.chunkCount.x, this.chunkCount.y];

        this.chunkWorldSize = (Vector2) this.tilesPerChunk * this.tileWorldSize;

        GenerateChunks ();
    }

    void Start () {
        SetPositionToMiddleOfChunks ();

        ParentChunksToSelf ();

        transform.position = Vector3.zero;

        SetUpProcessor ();
    }

    void OnValidate () {
        this.chunkCount.Clamp (Vector2Int.one, Vector2Int.one * 100);
        this.tilesPerChunk.Clamp (Vector2Int.one * 10, Vector2Int.one * 1000);
    }

    private void SetUpProcessor () {
        ChunkProcessor.Chunks = this.chunks;
        ChunkProcessor.ChunkCount = this.chunkCount;
        ChunkProcessor.ChunkWorldSize = this.chunkWorldSize;
        ChunkProcessor.TileWorldSize = this.tileWorldSize;
    }

    private void GenerateChunks () {
        Vector3 position = Vector3.zero;

        for (int i = 0; i < this.chunkCount.x; i++) {
            for (int j = 0; j < this.chunkCount.y; j++) {
                this.chunks[i, j] = GenerateNewChunk (position);
                this.chunks[i, j].gameObject.name = string.Format ("Chunk [{0}][{1}]", i, j);

                position += Vector3.right * this.chunkWorldSize.x;
            }
            position.x = Vector3.zero.x;
            position += Vector3.forward * this.chunkWorldSize.y;
        }
    }

    private Chunk GenerateNewChunk (Vector3 position) {
        GameObject chunkObj = Instantiate (this.chunkPrefab, position, Quaternion.identity);

        Chunk chunkScript = chunkObj.GetComponent<Chunk> ();

        chunkScript.SetToSize (this.tileWorldSize, this.tilesPerChunk);

        return chunkScript;
    }

    private void SetPositionToMiddleOfChunks () {
        Vector3 averagePosition = ((this.chunkCount - Vector2.one) * this.chunkWorldSize).ToHorizontalVector3 ();
        averagePosition = averagePosition.DivideBy (Vector3.one * 2);
        transform.position = averagePosition;
    }

    private void ParentChunksToSelf () {
        for (int i = 0; i < this.chunkCount.x; i++) {
            for (int j = 0; j < this.chunkCount.y; j++) {
                this.chunks[i, j].transform.SetParent (transform);
            }
        }
    }
}
