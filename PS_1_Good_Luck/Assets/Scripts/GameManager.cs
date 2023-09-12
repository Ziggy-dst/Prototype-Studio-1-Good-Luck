using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector] public int currentScore = -8;

    public bool isGameEnd = false;

    public Action OnPlayerMove;
    public Action<int, KeyCode> OnMoveKeyClicked;
    public Action<int, KeyCode> OnKeyRefresh;
    public Action OnTileGenerate;
    // UI close, block input, open end UI
    public Action OnGameEnds;

    public List<KeyCode> keyPool;
    public List<BoundKey> activeKeys;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & !PlayerMovement.Instance.isMoving)
        {
            RestartLevel();
        }
    }

    public static void RestartLevel()
    {
        Instance.isGameEnd = false;
        Instance.currentScore = -8;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerMovement.Instance.transform.position = Vector3.zero;
        PlayerMovement.Instance.InitializeKeys();
    }
}
