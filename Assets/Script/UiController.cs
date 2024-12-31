using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController instance;
    public int score;
    public TextMeshProUGUI ScoreText;
    public Image spriteTarget;
    public Image spritePrefab; // Prefab of the Image to instantiate
    public Canvas canvas;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        score = 0;
    }

    public void UpdateScore()
    {
        ScoreText.text = score.ToString();
    }
}
