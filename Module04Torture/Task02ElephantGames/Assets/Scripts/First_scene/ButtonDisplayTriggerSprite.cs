using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class ButtonDisplayTriggerSprite : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Color _colorOnClick;
    [SerializeField] private Color _colorOnHover;

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _originalColor = _spriteRenderer.color;
    }

    private void OnMouseDown()
    {
        _spriteRenderer.color = _colorOnClick;
        _button.gameObject.SetActive(true);
    }

    private void OnMouseUp()
    {
        _spriteRenderer.color = _colorOnHover;
    }

    private void OnMouseEnter()
    {
        _spriteRenderer.color = _colorOnHover;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.color = _originalColor;
    }
}