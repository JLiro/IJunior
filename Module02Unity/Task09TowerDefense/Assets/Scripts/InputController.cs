using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private Camera _camera;

    private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftTouch();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            HandleRightTouch();
        }
    }

    private void HandleLeftTouch()
    {
        Tile tile = _board.GetTile(TouchRay);

        if (tile != null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _board.ToggleTower(tile);
            }
            else
            {
                _board.ToggleWall(tile);
            }
        }
    }

    private void HandleRightTouch()
    {
        Tile tile = _board.GetTile(TouchRay);

        if (tile != null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _board.ToggleDestination(tile);
            }
            else
            {
                _board.ToggleSpawnPoint(tile);
            }
        }
    }
}
