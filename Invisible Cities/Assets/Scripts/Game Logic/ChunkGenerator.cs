using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {
    public static readonly Vector2Int sizeLimits = new Vector2Int (1, 1000);

    [Header ("Object References")]
    [SerializeField] ChunkProcessor chunkProcessor = new ChunkProcessor ();

    [Header ("Prefabs")]
    [SerializeField] GameObject chunkPrefab = null;

    [Header ("Settings")]
    [SerializeField] Vector2Int chunkCount;
    [SerializeField] int tilesPerChunk = 40;
    [SerializeField] float tileWorldSize = 0.5f;

    [Header ("Read Only")]
    [SerializeField, ReadOnly] float chunkWorldSize;
    [SerializeField, ReadOnly] Chunk[,] chunks;

    public PlaceableEntity testObj;

    void Awake () {
        this.chunks = new Chunk[this.chunkCount.x, this.chunkCount.y];

        this.chunkWorldSize = this.tilesPerChunk * this.tileWorldSize;

        GenerateChunks ();
    }

    void Start () {
        SetPositionToMiddleOfChunks ();

        ParentChunksToSelf ();

        transform.position = Vector3.zero;

        SetUpProcessor ();

        Debug.Log (this.chunkProcessor.GetTilesOverArea (this.testObj.TileGenerator.GetBottomLeftTilePosition (), Vector2Int.one * this.testObj.TotalTiles));
    }

    void OnValidate () {
        this.chunkCount.Clamp (Vector2Int.one, Vector2Int.one * 1000);
    }

    private void SetUpProcessor () {
        this.chunkProcessor.Chunks = this.chunks;
        this.chunkProcessor.ChunkCount = this.chunkCount;
        this.chunkProcessor.ChunkWorldSize = this.chunkWorldSize;
        this.chunkProcessor.TileWorldSize = this.tileWorldSize;
    }

    private void GenerateChunks () {
        Vector3 position = Vector3.zero;

        for (int i = 0; i < this.chunkCount.x; i++) {
            for (int j = 0; j < this.chunkCount.y; j++) {
                this.chunks[i, j] = GenerateNewChunk (position);
                this.chunks[i, j].gameObject.name = string.Format ("Chunk [{0}][{1}]", i, j);

                position += Vector3.right * this.chunkWorldSize;
            }
            position.x = Vector3.zero.x;
            position += Vector3.forward * this.chunkWorldSize;
        }
    }

    private Chunk GenerateNewChunk (Vector3 position) {
        GameObject chunkObj = Instantiate (this.chunkPrefab, position, Quaternion.identity);

        Chunk chunkScript = chunkObj.GetComponent<Chunk> ();

        chunkScript.SetToSize (this.tileWorldSize, this.tilesPerChunk);

        return chunkScript;
    }

    private void SetPositionToMiddleOfChunks () {
        Vector3 averagePosition = new Vector3 (this.chunkWorldSize * (this.chunkCount.x - 1), 0, this.chunkWorldSize * (this.chunkCount.y - 1));
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
