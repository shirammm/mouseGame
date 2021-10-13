using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerAndScoreManager : MonoBehaviour
{
    [SerializeField]
    private Image timerBar;
    [SerializeField]
    private static float maxTime = 60f;
    private static float timeLeft;

    [SerializeField]
    private Image attentionHand, meditationHand;
    private const float kHands = -2.38f;
    private const float k = -60;

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText, bestScoreText;
    private float score;
    private int bestScore;

    [SerializeField]
    private TMPro.TextMeshProUGUI gameOverScore, gameOverBest;
    [SerializeField]
    private Canvas gameOver, timerScore, pause;

    private int meditationLevel;
    private int attentionLevel;


    // Start is called before the first frame update
    void Start()
    {
        HeadsetManager.UpdateAttentionEvent += updateAttention;
        HeadsetManager.UpdateMeditationEvent += updateMeditation;
        timeLeft = maxTime;
        scoreText.text = string.Format("{0:00}:{1:00}", 0, 0);
        bestScore = PlayerPrefs.GetInt("bestScore");
        bestScoreText.text = string.Format("BEST SCORE: {0:00}:{1:00}", Mathf.FloorToInt(bestScore / 60), Mathf.FloorToInt(bestScore % 60));
}


    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        UpdateSliders();
        UpdateScore();
    }

    private void UpdateSliders()
    {
        attentionHand.transform.eulerAngles = new Vector3(0, 0, attentionLevel * kHands + k);
        meditationHand.transform.eulerAngles = new Vector3(0, 0, meditationLevel * kHands + k);
    }


    private void UpdateTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }

        else
        {
            bestScore = PlayerPrefs.GetInt("bestScore");
            gameOverBest.text = string.Format("BEST SCORE: {0:00}:{1:00}", Mathf.FloorToInt(bestScore / 60), Mathf.FloorToInt(bestScore % 60));
            gameOverScore.text = string.Format("GAME'S SCORE: {0:00}:{1:00}", Mathf.FloorToInt(score / 60), Mathf.FloorToInt(score % 60));
            pause.gameObject.SetActive(false);
            timerScore.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(true);
        }
    }

    private void UpdateScore()
    {
        score = MouseyManager.score;
        float minutes = Mathf.FloorToInt(score / 60);
        float seconds = Mathf.FloorToInt(score % 60);
        scoreText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (bestScore < score)
        {
            bestScore = (int)score;
            bestScoreText.text = string.Format("BEST SCORE: {0:00}:{1:00}", Mathf.FloorToInt(bestScore / 60), Mathf.FloorToInt(bestScore % 60));
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
    }

    private void updateAttention(int value)
    {
        attentionLevel = value;
    }

    private void updateMeditation(int value)
    {
        meditationLevel = value;
    }


}
