using UnityEngine;

public class BlockPlacement : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnPlace()
    {
        Ray viewPointPosition = _mainCamera.ViewportPointToRay(new Vector3(.5f, .5f));

        float blockScale = 1f / 2f;

        if (Physics.Raycast(viewPointPosition, out var hitInfo))
        {
            Vector3 blockCenter = hitInfo.point + hitInfo.normal * blockScale;
            Vector3Int blockChunkPosition = Vector3Int.FloorToInt(blockCenter / blockScale);

            int chunkWidth = 32;

            Vector2Int chunkPosition = new Vector2Int(blockChunkPosition.x / chunkWidth, blockChunkPosition.z / chunkWidth);

            ChunkRenderer.Instance.SpawnBlock(blockChunkPosition - new Vector3Int(chunkPosition.x, 0, chunkPosition.y) * chunkWidth);
        }
    }
}