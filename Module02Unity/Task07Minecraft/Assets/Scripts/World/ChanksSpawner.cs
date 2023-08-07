using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanksSpawner : MonoBehaviour
{
    public Dictionary<Vector2Int, ChunkData> ChunkData = new Dictionary<Vector2Int, ChunkData>();
    public ChunkRenderer ChunkPrefab;

    public int ChunkWitdh = 32;
    public int ChunkHeight = 32;

    private int _chunkCount = 6;

    private void Start()
    {
        var chunkData = new ChunkData();

        int xPos;
        int zPos;

        for (int x = 0; x < _chunkCount; x++)
        {
            for (int z = 0; z < _chunkCount; z++)
            {
                xPos = x * ChunkWitdh;
                zPos = z * ChunkHeight;

                chunkData.ChunkPosition = new Vector2Int(x, z);

                chunkData.Blocks = TerrainGenerator.GenerateTerrain(ChunkWitdh, ChunkHeight, xPos, zPos);
                ChunkData.Add(new Vector2Int(x, z), chunkData);

                var chunk = Instantiate(ChunkPrefab, new Vector3(xPos, 0, zPos), Quaternion.identity, transform);
                chunk.ChunkData = chunkData;
                chunk.ParentWorld = this;
            }
        }
    }
}
