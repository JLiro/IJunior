using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;

    private const string Horizontal = "Horizontal";
    private const float  _jumpColliderHeight = 0.1f;

    private bool _isGrounded;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _direction;

    private float _jumpColliderWidth;
    private float _jumpColliderWidthPadding;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _jumpColliderWidth = _collider.bounds.size.x;
        _jumpColliderWidthPadding = 0.25f;
    }

    private void Update()
    {
        Vector2 bottomPosition = new Vector2(transform.position.x, _collider.bounds.min.y);

        _isGrounded = Physics2D.OverlapBox(bottomPosition, new Vector2(_jumpColliderWidth - _jumpColliderWidthPadding, _jumpColliderHeight), 0f, _groundLayer);

        _direction.x = Input.GetAxisRaw(Horizontal);

        Move(_direction);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
    }

    private void Jump() => _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);

    private void Move(Vector2 direction)
    {
        if (direction.x != 0)
        {
            _spriteRenderer.flipX = direction.x < 0;
        }

        _rigidbody.velocity = new Vector2(_speed * Time.deltaTime * direction.x, _rigidbody.velocity.y);

        if(direction.x != 0)
        {
            _animator.Play(PlayerAnimator.Run);
        }
        else
        {
            _animator.Play(PlayerAnimator.Idle);
        }
    }
}