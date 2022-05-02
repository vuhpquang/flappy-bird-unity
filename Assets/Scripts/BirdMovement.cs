using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
  public float junpForce = 10f;

  // Ground
  public Transform groundCheck;
  public LayerMask groundLayer;
  public float groundCheckRadius;

  //Movement
  private bool _isGrounded;

  private Rigidbody2D _rigidbody;
  private Animator _animator;
  private bool _isDead;

  void Awake()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
  }

  void Start()
  {
    ResetGame();
  }

  public void ResetGame()
  {
    _isDead = false;
    transform.position = new Vector2(-2, 2);
  }

  private bool IsJumpButton()
  {
    return Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0);
  }

  void Update()
  {
    // Is Jumping?
    if (IsJumpButton() && !_isDead)
    {
      _rigidbody.velocity = Vector2.zero;
      _rigidbody.AddForce(new Vector2(0f, junpForce), ForceMode2D.Impulse);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Ground"))
    {
      _isDead = true;
      //_animator.SetTrigger("Die");

      other.SendMessageUpwards("EndGame");
    }
    else if (other.CompareTag("Pipe"))
    {
      _isDead = true;
      //_animator.SetTrigger("Die");

      other.SendMessageUpwards("GameOver");
    }
    else if (other.CompareTag("Score") && !_isDead)
    {
      //_animator.SetTrigger("Point");

      other.SendMessageUpwards("AddPoints", 1);
    }
  }
}
