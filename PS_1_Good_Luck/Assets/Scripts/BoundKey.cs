using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoundKey : MonoBehaviour
{
    public KeyCode currentKeyCode;

    [SerializeField] private int refreshTurnNum = 4;

    private int _currentRefreshTurnNum;

    public void Initialize()
    {
        _currentRefreshTurnNum = 4;

        // assign the starting random key
        RefreshKey();
    }

    public void RefreshCountDown(int dir, KeyCode keyCode)
    {
        if (keyCode.Equals(currentKeyCode))
        {
            _currentRefreshTurnNum--;
            GameManager.Instance.OnMoveKeyClicked.Invoke(dir, keyCode);
            print($"remaining turn: /{_currentRefreshTurnNum} | dir: /{dir} | Key: /{keyCode}");
        }

        if (_currentRefreshTurnNum == 0)
        {
            GameManager.Instance.OnKeyRefresh.Invoke(dir, keyCode);
            RefreshKey();
            _currentRefreshTurnNum = refreshTurnNum;
            GameManager.Instance.OnKeyRefresh.Invoke(dir, keyCode);
        }
    }

    private void RefreshKey()
    {
        List<KeyCode> keyPool = GameManager.Instance.keyPool;
        List<BoundKey> activeKeys = GameManager.Instance.activeKeys;

        List<KeyCode> activeKeycodes = new List<KeyCode>();
        foreach (var activeKey in activeKeys) {activeKeycodes.Add(activeKey.currentKeyCode);}

        // calculate the exception of key lists to get unoccupied keys
        List<KeyCode> availableKeyCodes = keyPool.Except(activeKeycodes).ToList();

        // randomly pick an unoccupied key from the key pool
        int randomKeyIndex = UnityEngine.Random.Range(0, availableKeyCodes.Count);
        currentKeyCode = availableKeyCodes[randomKeyIndex];
        UpdateBindings();
    }

    private void UpdateBindings()
    {
        switch (gameObject.name)
        {
            case "RightKey":
                PlayerMovement.Instance.RightKey = currentKeyCode;
                break;
            case "DownKey":
                PlayerMovement.Instance.DownKey = currentKeyCode;
                break;
            case "LeftKey":
                PlayerMovement.Instance.LeftKey = currentKeyCode;
                break;
            case "UpKey":
                PlayerMovement.Instance.UpKey = currentKeyCode;
                break;
        }
    }
}
