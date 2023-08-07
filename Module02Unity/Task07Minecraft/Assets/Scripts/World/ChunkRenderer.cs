using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChunkRenderer : MonoBehaviour
{
    public  int ChunkWitdh = 32;
    public  int ChunkHeight = 32;

    private List<Vector3> verticies = new List<Vector3>();
    private List<int> triangles = new List<int> ();

    private Mesh _chunkMesh;

    public ChunkData ChunkData;
    public ChanksSpawner ParentWorld;

    public static ChunkRenderer Instance { get { return Instance; } set { if (Instance != null) { Instance = new ChunkRenderer(); } } }

    private void Start()
    {
        _chunkMesh = new Mesh();

        GetComponent<MeshFilter>().mesh = _chunkMesh;
        GetComponent<MeshCollider>().sharedMesh = _chunkMesh;
        
        RegenerateMesh();
    }

    private void RegenerateMesh()
    {
        verticies.Clear();
        triangles.Clear();

        for (int y = 0; y < ChunkWitdh; y++)
        {
            for (int x = 0; x < ChunkWitdh; x++)
            {
                for (int z = 0; z < ChunkHeight; z++)
                {
                    GenerateBlock(x, y, z);
                }
            }
        }

        _chunkMesh.vertices = verticies.ToArray();
        _chunkMesh.triangles = triangles.ToArray();

        _chunkMesh.Optimize();

        _chunkMesh.RecalculateNormals();
        _chunkMesh.RecalculateBounds();
    }

    public void SpawnBlock(Vector3Int blockPosition)
    {
        ChunkData.Blocks[blockPosition.x, blockPosition.y, blockPosition.z] = BlockType.Grass;
        RegenerateMesh();
    }

    private void GenerateBlock(int x, int y, int z)
    {
        var blockPosition = new Vector3Int(x, y, z);

        if(GetAtBlockPosition(blockPosition) == 0)
        {
            return;
        }

        if(GetAtBlockPosition(blockPosition + Vector3Int.right) == 0)
        {
            GenerateRightSide(blockPosition);
        }

        if (GetAtBlockPosition(blockPosition + Vector3Int.left) == 0)
        {
            GenerateLeftSide(blockPosition);
        }

        if (GetAtBlockPosition(blockPosition + Vector3Int.forward) == 0)
        {
            GenerateForwardSide(blockPosition);
        }

        if (GetAtBlockPosition(blockPosition + Vector3Int.back) == 0)
        {
            GenerateBackSide(blockPosition);
        }

        if (GetAtBlockPosition(blockPosition + Vector3Int.up) == 0)
        {
            GenerateUpSide(blockPosition);
        }

        if (GetAtBlockPosition(blockPosition + Vector3Int.down) == 0)
        {
            GenerateDownSide(blockPosition);
        } 
    }

    private BlockType GetAtBlockPosition(Vector3Int blockPosition)
    {
        if(
               blockPosition.x >= 0 && blockPosition.x < ChunkWitdh &&
               blockPosition.y >= 0 && blockPosition.y < ChunkHeight &&
               blockPosition.z >= 0 && blockPosition.z < ChunkWitdh
           )
        {
            return ChunkData.Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
        }
        else
        {
            if (blockPosition.y < 0 || blockPosition.y >= ChunkHeight)
            {
                return BlockType.Air;
            }

            Vector2Int adjacentChunkPosition = ChunkData.ChunkPosition;
            
            if (blockPosition.x < 0)
            {
                adjacentChunkPosition.x--;
                blockPosition.x += ChunkWitdh;
            }
            else if(blockPosition.x >= ChunkWitdh)
            {
                adjacentChunkPosition.x++;
                blockPosition.x -= ChunkWitdh;
            }

            if (blockPosition.z < 0)
            {
                adjacentChunkPosition.y--;
                blockPosition.z += ChunkWitdh;
            }
            else if (blockPosition.z >= ChunkWitdh)
            {
                adjacentChunkPosition.y++;
                blockPosition.z -= ChunkWitdh;
            }

            if (ParentWorld.ChunkData.TryGetValue(adjacentChunkPosition, out ChunkData adjacentChunk))
            {
                return adjacentChunk.Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
            }
            else
            {
                return BlockType.Air;
            }
        }
    }

    private void GenerateRightSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(1, 0, 0) + blockPosition);
        verticies.Add(new Vector3(1, 1, 0) + blockPosition);
        verticies.Add(new Vector3(1, 0, 1) + blockPosition);
        verticies.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void GenerateLeftSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(0, 0, 0) + blockPosition);
        verticies.Add(new Vector3(0, 0, 1) + blockPosition);
        verticies.Add(new Vector3(0, 1, 0) + blockPosition);
        verticies.Add(new Vector3(0, 1, 1) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void GenerateForwardSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(0, 0, 1) + blockPosition);
        verticies.Add(new Vector3(1, 0, 1) + blockPosition);
        verticies.Add(new Vector3(0, 1, 1) + blockPosition);
        verticies.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void GenerateBackSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(0, 0, 0) + blockPosition);
        verticies.Add(new Vector3(0, 1, 0) + blockPosition);
        verticies.Add(new Vector3(1, 0, 0) + blockPosition);
        verticies.Add(new Vector3(1, 1, 0) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void GenerateUpSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(0, 1, 0) + blockPosition);
        verticies.Add(new Vector3(0, 1, 1) + blockPosition);
        verticies.Add(new Vector3(1, 1, 0) + blockPosition);
        verticies.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void GenerateDownSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(0, 0, 0) + blockPosition);
        verticies.Add(new Vector3(1, 0, 0) + blockPosition);
        verticies.Add(new Vector3(0, 0, 1) + blockPosition);
        verticies.Add(new Vector3(1, 0, 1) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void AddLastVerticiesSquare()
    {
        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 2);
    }
}
