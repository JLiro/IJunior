using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _takeDistance;
    [SerializeField] private float _holdDistance;

    private PlayerInput _input;

    private BlockPlacement _placement;

    private Transform _currentObject;

    private Vector2 _direction;
    private Vector2 _rotate;

    private Vector2 _rotation;

    private void Awake()
    {
        _mainCamera = Camera.main;

        //_placement = new BlockPlacement();
        _input = new PlayerInput();
        _input.Enable();
    }

    private void Update()
    {
        _direction = _input.Player.Move.ReadValue<Vector2>();
        _rotate    = _input.Player.Look.ReadValue<Vector2>();

        Move(_direction);
        Look(_rotate);

        _input.Player.PlaceBlock.performed += ctx => OnPlace();
    }

    private void Move(Vector2 direction)
    {
        float scaleMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        transform.position += move * scaleMoveSpeed;
    }

    private void Look(Vector2 rotate)
    {
        float scaleMoveSpeed = _rotateSpeed * Time.deltaTime;

        _rotation.y += rotate.x * scaleMoveSpeed;
        _rotation.x = Mathf.Clamp(_rotation.x - rotate.y * scaleMoveSpeed, -90, 90);
        transform.localEulerAngles = _rotation;
    }

    private Camera _mainCamera;

    public void OnPlace()
    {
        /*if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, _takeDistance) && hitInfo.collider.gameObject.isStatic == false)
        {

        }*/

        Debug.Log("afsafasf");

        Ray viewPointPosition = _mainCamera.ViewportPointToRay(new Vector3(.5f, .5f));

        float blockScale = 1 / 2;

        if (Physics.Raycast(viewPointPosition, out var hitInfo2))
        {
            Vector3 blockCenter = hitInfo2.point + hitInfo2.normal * blockScale;
            Vector3Int blockChunkPosition = Vector3Int.FloorToInt(blockCenter / blockScale);

            int chunkWidth = 32;

            Vector2Int chunkPosition = new Vector2Int(blockChunkPosition.x / chunkWidth, blockChunkPosition.z / chunkWidth);

 

            ChunkRenderer.Instance.SpawnBlock(blockChunkPosition - (new Vector3Int(chunkPosition.x, 0, chunkPosition.y) * chunkWidth));
        }
    }
}
