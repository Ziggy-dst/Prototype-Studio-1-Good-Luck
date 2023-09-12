using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject EndPage;
    public TMP_Text endScoreText;

    public GameObject upKey;
    public GameObject leftKey;
    public GameObject downKey;
    public GameObject rightKey;

    void Start()
    {
        GameManager.Instance.OnTileGenerate += UpdateScoreText;
        GameManager.Instance.OnMoveKeyClicked += UpdateKeyText;
        GameManager.Instance.OnKeyRefresh += RefreshKeyUI;
        GameManager.Instance.OnGameEnds += ChangeUI;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnTileGenerate -= UpdateScoreText;
        GameManager.Instance.OnMoveKeyClicked -= UpdateKeyText;
        GameManager.Instance.OnKeyRefresh -= RefreshKeyUI;
        GameManager.Instance.OnGameEnds -= ChangeUI;
    }

    private void ChangeUI()
    {
        endScoreText.text = $"You have traveled {GameManager.Instance.currentScore} places";
        EndPage.SetActive(true);
    }

    // 1 = right
    public void UpdateKeyText(int dir, KeyCode keyCode)
    {
        switch (dir)
        {
            case 1:
                Color color1 = rightKey.GetComponent<Image>().color;
                rightKey.GetComponent<Image>().color = new Color(color1.r, color1.g, color1.b, color1.a - 0.25f);
                rightKey.GetComponentInChildren<TMP_Text>().text = keyCode.ToString();
                break;
            case 2:
                Color color2 = downKey.GetComponent<Image>().color;
                downKey.GetComponent<Image>().color = new Color(color2.r, color2.g, color2.b, color2.a - 0.25f);
                downKey.GetComponentInChildren<TMP_Text>().text = keyCode.ToString();
                break;
            case 3:
                Color color3 = leftKey.GetComponent<Image>().color;
                leftKey.GetComponent<Image>().color = new Color(color3.r, color3.g, color3.b, color3.a - 0.25f);
                leftKey.GetComponentInChildren<TMP_Text>().text = keyCode.ToString();
                break;
            case 4:
                Color color4 = upKey.GetComponent<Image>().color;
                upKey.GetComponent<Image>().color = new Color(color4.r, color4.g, color4.b, color4.a - 0.25f);
                upKey.GetComponentInChildren<TMP_Text>().text = keyCode.ToString();
                break;
        }
    }

    private void UpdateScoreText()
    {
        GameManager.Instance.currentScore++;
        scoreText.text = GameManager.Instance.currentScore.ToString();
    }

    public void RefreshKeyUI(int dir, KeyCode keyCode)
    {
        switch (dir)
        {
            case 1:
                Color color1 = rightKey.GetComponent<Image>().color;
                rightKey.GetComponent<Image>().color = new Color(color1.r, color1.g, color1.b, 1);
                rightKey.GetComponentInChildren<TMP_Text>().text = "";
                break;
            case 2:
                Color color2 = downKey.GetComponent<Image>().color;
                downKey.GetComponent<Image>().color = new Color(color2.r, color2.g, color2.b, 1);
                downKey.GetComponentInChildren<TMP_Text>().text = "";
                break;
            case 3:
                Color color3 = leftKey.GetComponent<Image>().color;
                leftKey.GetComponent<Image>().color = new Color(color3.r, color3.g, color3.b, 1);
                leftKey.GetComponentInChildren<TMP_Text>().text = "";
                break;
            case 4:
                Color color4 = upKey.GetComponent<Image>().color;
                upKey.GetComponent<Image>().color = new Color(color4.r, color4.g, color4.b, 1);
                upKey.GetComponentInChildren<TMP_Text>().text = "";
                break;
        }
    }
}
