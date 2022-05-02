using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGroupMovement : MonoBehaviour
{
  public float speed = 2.5f;
  public Transform[] pipes;
  public float distance = 2.5f;
  public float minHeight = 2.1f;

  private Rigidbody2D[] _rbPipes = new Rigidbody2D[3];
  private Rigidbody2D _rbGroup;
  private bool _isMoving;
  private float _topBarScale;

  void Awake()
  {
    float bottomLimit = minHeight * 100 / 320;
    float topLimit = (10 - distance) * 100 / 320 - bottomLimit;
    float bottomPipeScale = Random.Range(bottomLimit, topLimit);

    _topBarScale = (10 - distance) * 100 / 320 - bottomPipeScale;
    pipes[0].transform.localScale = new Vector2(2, bottomPipeScale);
    pipes[1].transform.localScale = new Vector2(2, _topBarScale);

    _isMoving = true;
    for (int i = 0; i < pipes.Length; i++)
    {
      _rbPipes[i] = pipes[i].GetComponent<Rigidbody2D>();
    }
    _rbGroup = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    GameObject gameInfo = GameObject.FindWithTag("GameInfo");
    if (gameInfo.GetComponent<GameInfo>().isGameOver)
    {
      _isMoving = false;
    }
    else
      _isMoving = true;
  }

  private void FixedUpdate()
  {
    float horizontalVelocity = speed * -1f;
    if (!_isMoving)
      horizontalVelocity = 0;

    for (int i = 0; i < pipes.Length; i++)
    {
      _rbPipes[i].velocity = new Vector2(horizontalVelocity, 0);
    }
    _rbGroup.velocity = new Vector2(horizontalVelocity, 0);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Decomposer"))
    {
      Destroy(gameObject);
    }
  }
}
