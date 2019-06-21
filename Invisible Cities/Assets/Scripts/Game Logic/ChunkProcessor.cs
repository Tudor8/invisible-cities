using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChunkProcessor {
    [Header ("Settings")]
    [SerializeField] Vector2Int chunkCount;
    [SerializeField, ReadOnly] float chunkWorldSize;
    [SerializeField] float tileWorldSize = 0.5f;

    [Header ("Read Only")]
    [SerializeField] Chunk[,] chunks;

    public Chunk[,] Chunks { get => this.chunks; set => this.chunks = value; }
    public Vector2Int ChunkCount { get => this.chunkCount; set => this.chunkCount = value; }
    public float ChunkWorldSize { get => this.chunkWorldSize; set => this.chunkWorldSize = value; }
    public float TileWorldSize { get => this.tileWorldSize; set => this.tileWorldSize = value; }

    public Tile[,] GetTilesOverArea (Vector3 bottomLeft, Vector2Int size) {
        Tile[,] result = new Tile[Mathf.RoundToInt (size.x), Mathf.RoundToInt (size.y)];

        Vector2Int chunk = GetChunk (bottomLeft);
        Vector2Int tileIndex = GetTileIndex (bottomLeft);

        for (int i = tileIndex.x; i < tileIndex.x + size.x; i++) {
            for (int j = tileIndex.y; j < tileIndex.y + size.y; j++) {
                result[i - tileIndex.x, j - tileIndex.y] = Chunks[chunk.x, chunk.y].Tiles[i, j];
            }
        }

        return result;
    }

    private Vector2Int GetChunk (Vector3 position) {
        return new Vector2Int (
            (int) ((position.z / ChunkWorldSize) + ChunkCount.x / 2f),
            (int) ((position.x / ChunkWorldSize) + ChunkCount.y / 2f)
        );
    }

    private Vector2Int GetTileIndex (Vector3 position) {
        return new Vector2Int (
            (int) ((ChunkWorldSize / 2f - (position.z % ChunkWorldSize)) * (1 / TileWorldSize)),
            (int) (((position.x % ChunkWorldSize) + ChunkWorldSize / 2f) * (1 / TileWorldSize))
        );
    }
}
