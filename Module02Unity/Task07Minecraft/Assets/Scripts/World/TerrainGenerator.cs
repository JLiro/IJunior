using UnityEngine;

public static class TerrainGenerator
{
    public static BlockType[,,] GenerateTerrain(int chunkWitdh, int chunkHeight, int xOffset, int zOffset)
    {
        var result = new BlockType[chunkWitdh, chunkHeight, chunkWitdh];

        for (int x = 0; x < chunkWitdh; x++)
        {
            for (int z = 0; z < chunkWitdh; z++)
            {
                float height = Mathf.PerlinNoise( (x + xOffset) * .1f, (z + zOffset) * .1f) * 4 + 6;

                for (int y = 0; y < height; y++)
                {
                    result[x, y, z] = BlockType.Grass;
                }
            }
        }

        return result;
    }
}