using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChunkProcessor : Singleton<ChunkProcessor> {
    [Header ("Read Only")]
    [SerializeField, ReadOnly] Vector2Int chunkCount;
    [SerializeField, ReadOnly] Vector2 chunkWorldSize;
    [SerializeField, ReadOnly] float tileWorldSize = 0.5f;
    [SerializeField] Chunk[,] chunks;

    public Chunk[,] Chunks { get => this.chunks; set => this.chunks = value; }
    public Vector2Int ChunkCount { get => this.chunkCount; set => this.chunkCount = value; }
    public Vector2 ChunkWorldSize { get => this.chunkWorldSize; set => this.chunkWorldSize = value; }
    public float TileWorldSize { get => this.tileWorldSize; set => this.tileWorldSize = value; }

    void Awake () {
        InitiateSingleton ();
    }

    public bool GetTilesOverArea (Vector3 topLeft, Vector2Int size, out Tile[,] tiles) {
        tiles = new Tile[size.x, size.y];

        Vector3 bottomRight = topLeft + (size - Vector2.one).ToHorizontalVector3 ().FlipZ () * this.tileWorldSize;

        if (!IsInBounds (topLeft, bottomRight)) {
            return false;
        }

        Chunk chunk = GetChunk (topLeft);
        Vector2Int tileIndex = GetTileIndex (topLeft);

        for (int i = tileIndex.x; i < tileIndex.x + size.x; i++) {
            for (int j = tileIndex.y; j < tileIndex.y + size.y; j++) {
                tiles[i - tileIndex.x, j - tileIndex.y] = chunk.Tiles[i, j];
            }
        }

        return true;
    }

    private bool IsInBounds (Vector3 topLeft, Vector3 bottomRight) {
        (Vector3 topLeft, Vector3 bottomRight) bounds = GetBounds ();

        return topLeft.x > bounds.topLeft.x && topLeft.z < bounds.topLeft.z && bottomRight.x < bounds.bottomRight.x && bottomRight.z > bounds.bottomRight.z;
    }

    private (Vector3 topLeft, Vector3 bottomRight) GetBounds () {
        Vector2 totalSize = this.chunkWorldSize * this.chunkCount;
        Vector3 halfScale = (totalSize / 2).ToHorizontalVector3 ();

        Vector3 topLeft = halfScale.FlipX ();
        Vector3 bottomRight = halfScale.FlipZ ();

        return (topLeft, bottomRight);
    }

    private Vector2Int GetChunkIndex (Vector3 position) {
        return new Vector2Int (
            (int) ((position.z / ChunkWorldSize.y) + ChunkCount.x / 2f),
            (int) ((position.x / ChunkWorldSize.x) + ChunkCount.y / 2f)
        );
    }

    private Chunk GetChunk (Vector3 position) {
        Vector2Int chunkIndex = GetChunkIndex (position);
        return this.chunks[chunkIndex.x, chunkIndex.y];
    }

    private Vector2Int GetTileIndex (Vector3 position) {
        return new Vector2Int (
            (int) ((ChunkWorldSize.y / 2f - (position.z % ChunkWorldSize.y)) * (1 / TileWorldSize)),
            (int) (((position.x % ChunkWorldSize.x) + ChunkWorldSize.x / 2f) * (1 / TileWorldSize))
        );
    }
}
