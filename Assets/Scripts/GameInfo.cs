using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfo : MonoBehaviour
{
  public float points;
  public bool isGameOver;
  public TextMeshProUGUI pointUI;

  private bool _isGrounded;

  void Start()
  {
    ResetGame();
  }

  private bool IsJumpButton()
  {
    return Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0);
  }

  void Update()
  {
    pointUI.text = points.ToString();
    if (IsJumpButton() && _isGrounded)
    {
      ResetGame();
    }
  }

  public void AddPoints(float pointsToAdd)
  {
    points += pointsToAdd;
    Debug.Log("Points: " + points);
  }

  public void ResetGame()
  {
    Debug.Log("ResetGame");
    DestroyAllPipeGroup();
    points = 0;
    isGameOver = false;
    _isGrounded = false;
    GameObject player = GameObject.FindWithTag("Player");
    if (player != null)
    {
      player.GetComponent<BirdMovement>().ResetGame();
    }
  }

  void DestroyAllPipeGroup()
  {
    GameObject[] pipeGroups = GameObject.FindGameObjectsWithTag("PipeGroup");

    for (var i = 0; i < pipeGroups.Length; i++)
    {
      Destroy(pipeGroups[i]);
    }
  }

  public void GameOver()
  {
    isGameOver = true;
  }

  public void EndGame()
  {
    Debug.Log("EndGame");
    isGameOver = true;
    _isGrounded = true;
  }
}
