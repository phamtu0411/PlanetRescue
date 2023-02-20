using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private bool gameFinished;
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private AudioSource BGMSound;

    private float score;
    private float scoreSpeed;
    private int currentLevel;

    [SerializeField] private List<int> levelSpeed, levelMax;

    private void Awake()
    {
        GameManager.game.IsInitialized = true;
        gameFinished = false;
        score = 0;
        currentLevel = 0;
        scoreTxt.text = ((int)score).ToString();
        scoreSpeed = levelSpeed[currentLevel];
    }

    private void Update()
    {
        if (gameFinished) return;
        score += scoreSpeed * Time.deltaTime;
        scoreTxt.text = ((int)score).ToString();

        if (score > levelMax[Mathf.Clamp(currentLevel, 0, levelSpeed.Count - 1)])
        {
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, levelSpeed.Count - 1);
            scoreSpeed = levelSpeed[currentLevel];
        }
    }

    public void GameEnded()
    {
        gameFinished = true;
        GameManager.game.CurrentScore = (int)score;
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        GameManager.game.GoToMainMenu();
    }

    public void PlayBGMSound()
    {
        BGMSound.loop = true;
        BGMSound.Play();
    }
}
