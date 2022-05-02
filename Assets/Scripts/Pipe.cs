using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
  public GameObject prefab;
  public int amount = 4;
  public int instantiateGap = 2;

  private bool _isStopped;
  void Start()
  {
    InitializePool();
    _isStopped = false;
    InvokeRepeating("GetPipeGroupFromPool", 1f, instantiateGap);
  }

  private void InitializePool()
  {
    for (int i = 0; i < amount; i++)
    {
      AddPipeGroupToPool();
    }
  }

  private void AddPipeGroupToPool()
  {
    GameObject pipeGroup = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
    pipeGroup.SetActive(false);
  }

  private void Update()
  {
    GameObject gameInfo = GameObject.FindWithTag("GameInfo");
    if (!gameInfo.GetComponent<GameInfo>().isGameOver && _isStopped)
    {
      _isStopped = false;
      InvokeRepeating("GetPipeGroupFromPool", 1f, instantiateGap);
    }
    if (gameInfo.GetComponent<GameInfo>().isGameOver && !_isStopped)
    {
      _isStopped = true;
      CancelInvoke();
    }
  }

  private GameObject GetPipeGroupFromPool()
  {
    GameObject pipeGroup = null;

    for (int i = 0; i < transform.childCount; i++)
    {
      if (!transform.GetChild(i).gameObject.activeSelf)
      {
        pipeGroup = transform.GetChild(i).gameObject;
        break;
      }
    }

    if (pipeGroup == null)
    {
      AddPipeGroupToPool();
      pipeGroup = transform.GetChild(transform.childCount - 1).gameObject;
    }

    pipeGroup.transform.position = this.transform.position;
    pipeGroup.SetActive(true);
    return pipeGroup;
  }
}
