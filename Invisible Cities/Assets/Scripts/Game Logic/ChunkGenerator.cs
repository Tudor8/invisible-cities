using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {
    public static readonly Vector2Int sizeLimits = new Vector2Int (1, 1000);

    [Header ("Prefabs")]
    [SerializeField] GameObject chunkPrefab;

    [Header ("Settings")]
    [SerializeField] Vector2Int chunkCount;
    [SerializeField] int tilesPerChunk = 40;
    [SerializeField] float tileWorldSize = 0.5f;

    [Header ("Read Only")]
    [SerializeField, ReadOnly] Chunk[,] chunks;
    [SerializeField, ReadOnly] float chunkWorldSize;

    public ObjectTest testObj;

    void Awake () {
        this.chunks = new Chunk[this.chunkCount.x, this.chunkCount.y];

        this.chunkWorldSize = this.tilesPerChunk * this.tileWorldSize;
    }

    void Start () {
        GenerateChunks ();

        SetPositionToMiddleOfChunks ();

        ParentChunksToSelf ();

        transform.position = Vector3.zero;

        
    }

    void Update() {
        GetTilesOverArea (testObj.transform.TransformPoint (testObj.GetBottomLeftTilePosition ()), Vector2Int.one * testObj.TotalTiles);
    }

    void OnValidate () {
        this.chunkCount.Clamp (Vector2Int.one, Vector2Int.one * 1000);
    }

    public Tile[,] GetTilesOverArea(Vector3 bottomLeft, Vector2Int size) {
        Tile[,] result = new Tile[Mathf.RoundToInt(size.x), Mathf.RoundToInt (size.y)];

       // Debug.Log (bottomLeft);

        Vector2Int chunk = GetChunk (bottomLeft);
        Vector2Int tileIndex = GetTileIndex (bottomLeft);
        //Debug.Log (chunk);
        //Debug.Log (bottomLeft.x + " " + bottomLeft.z + " " + gofuckyourself);

        for(int i = tileIndex.x; i < tileIndex.x + size.x; i++) {
            string s = "";
            for(int j = tileIndex.y; j < tileIndex.y + size.y; j++) {
                s += "[" + i + ", " + j + "]";
            }
            Debug.Log (s);
        }

        return result;
    }

    private Vector2Int GetChunk(Vector3 position) {
        return new Vector2Int (
            (int) ((position.z / chunkWorldSize) + chunkCount.x / 2f),
            (int) ((position.x / chunkWorldSize) + chunkCount.y / 2f)
        );
    }

    private Vector2Int GetTileIndex (Vector3 position) {
        return new Vector2Int (
            (int) (((position.z % chunkWorldSize) + chunkWorldSize / 2f) * (1 / tileWorldSize)),
            (int) (((position.x % chunkWorldSize) + chunkWorldSize / 2f) * (1 / tileWorldSize))
        );
    }

    public Tile GetTileAt (Vector3 position) {
        return null;
    }

    private void GenerateChunks () {
        Vector3 position = Vector3.zero;

        for (int i = 0; i < this.chunkCount.x; i++) {
            for (int j = 0; j < this.chunkCount.y; j++) {
                this.chunks[i, j] = GenerateNewChunk (position);
                chunks[i, j].gameObject.name = string.Format ("Chunk [{0}][{1}]", i, j);

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
