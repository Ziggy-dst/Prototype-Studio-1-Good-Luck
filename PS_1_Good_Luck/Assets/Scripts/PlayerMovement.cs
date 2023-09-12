using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    public BoundKey boundUpKey;
    public BoundKey boundDownKey;
    public BoundKey boundLeftKey;
    public BoundKey boundRightKey;

    public MMF_Player moveFeedback;
    public MMF_Player dropFeedback;

    public bool isMoving = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitializeKeys();
    }

    private void Update()
    {
        isMoving = DOTween.IsTweening(transform, true);
        if (!GameManager.Instance.isGameEnd) GetMoveInput();
    }

    public void InitializeKeys()
    {
        BoundKey[] boundKeys = GetComponentsInChildren<BoundKey>();
        foreach (var boundKey in boundKeys)
        {
            boundKey.Initialize();
            if (GameManager.Instance.activeKeys.Count < 4) GameManager.Instance.activeKeys.Add(boundKey);
        }
    }

    private void GetMoveInput()
    {
        Vector3 currentDirt = Vector3.zero;

        if (Input.GetKeyDown(UpKey))
        {
            boundUpKey.RefreshCountDown(4, UpKey);
            currentDirt = Vector3.up;
            Move(currentDirt);
        }
        else if (Input.GetKeyDown(LeftKey))
        {
            boundLeftKey.RefreshCountDown(3, LeftKey);
            currentDirt = Vector3.left;
            Move(currentDirt);
        }
        else if (Input.GetKeyDown(DownKey))
        {
            boundDownKey.RefreshCountDown(2, DownKey);
            currentDirt = Vector3.down;
            Move(currentDirt);
        }
        else if (Input.GetKeyDown(RightKey))
        {
            boundRightKey.RefreshCountDown(1, RightKey);
            currentDirt = Vector3.right;
            Move(currentDirt);
        }
    }

    private void Move(Vector3 currentDirt)
    {
        if (!CheckIfBlocked(currentDirt) & !isMoving)
        {
            // play move feedback
            // moveFeedback.

            Vector3 currentDestination = transform.position + currentDirt;
            transform.DOMove(currentDestination, 0.2f)
                .OnComplete(() => { GameManager.Instance.OnPlayerMove(); });
        }
    }

    private bool CheckIfBlocked(Vector3 moveDirt)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position + moveDirt, 0.1f, Vector2.zero);

        if (hit.collider != null)
            if (hit.collider.tag.Equals("Wall"))
            {
                print("wall1");
                return true;
            }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Water"))
        {
            // play die feedback

            GameManager.Instance.isGameEnd = true;
            GameManager.Instance.OnGameEnds();
        }
    }
}
