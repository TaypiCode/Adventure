using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveForce;
    [SerializeField] private float _jumpForce;
    private bool _canJump = true;
    private bool _canMove = true;
    private bool _isAlive = true;
    private bool _isFinished = false;

    public bool IsFinished { get => _isFinished; set => _isFinished = value; }
    private void Start()
    {
        _animator.runtimeAnimatorController = CharacterMenuUI.ChoosedCharacter.Animator;
    }
    private void FixedUpdate()
    {
        if (!LevelUI.IsMobile)
        {
            float h = Input.GetAxis("Horizontal");
            Move(h);
            if (Input.GetAxis("Vertical") > 0)
            {
                Jump();
            }
        }
    }
    public void Move(float horizontal)
    {
        if (_isAlive && !_isFinished)
        {
            if (_canMove)
            {
                _rb.velocity = new Vector2(horizontal * _moveForce, _rb.velocity.y);

                bool isRunning = horizontal != 0 ? true : false;
                _animator.SetBool("IsRunning", isRunning);
            }
            bool flipSprite = horizontal == 0 ? _spriteRenderer.flipX : horizontal > 0 ? false : true;
            _spriteRenderer.flipX = flipSprite;
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }
    public void Jump()
    {
        if (_canJump && _isAlive && !_isFinished)
        {

            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _canJump = false;
            _animator.SetTrigger("JumpTrigger");
            _animator.SetBool("IsJumping", true);
        }
    }
    public void GetHit()
    {
        _animator.SetTrigger("HitTrigger");
        _isAlive = false;
        FindObjectOfType<LevelUI>().ShowFinishUI(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.SetBool("IsJumping", false);
        if (collision.gameObject.GetComponent<UnlockJump>())
        {
            _canJump = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        _canMove = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _canMove = false;
    }
}
