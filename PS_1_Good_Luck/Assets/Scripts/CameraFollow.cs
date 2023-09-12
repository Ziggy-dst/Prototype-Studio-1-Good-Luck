using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.OnPlayerMove += MoveCamera;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPlayerMove -= MoveCamera;
    }

    private void MoveCamera()
    {
        Vector2 playerPos = PlayerMovement.Instance.transform.position;
        transform.DOMove(new Vector3(playerPos.x, playerPos.y, transform.position.z), 0.2f);
    }
}
