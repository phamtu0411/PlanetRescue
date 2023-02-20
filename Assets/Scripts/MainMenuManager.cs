using System.Collections;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private TMP_Text newBestScoreTxt;
    [SerializeField] private TMP_Text highScoreTxt;

    private void Awake()
    {
        if (GameManager.game.IsInitialized)
        {
            StartCoroutine(ShowScore());
        }
        else
        {
            scoreTxt.gameObject.SetActive(false);
            newBestScoreTxt.gameObject.SetActive(false);
            highScoreTxt.text = GameManager.game.HighScore.ToString();
        }
    }

    [SerializeField] private float animationTime;
    [SerializeField] private AnimationCurve speedCurve;

    private IEnumerator ShowScore()
    {
        int tempScore = 0;
        scoreTxt.text = tempScore.ToString();

        int currentScore = GameManager.game.CurrentScore;
        int highScore = GameManager.game.HighScore;

        if (highScore < currentScore)
        {
            newBestScoreTxt.gameObject.SetActive(true);
            GameManager.game.HighScore = currentScore;
        }
        else
        {
            newBestScoreTxt.gameObject.SetActive(true);
        }

        highScoreTxt.text = GameManager.game.HighScore.ToString();

        float speed = 1 / animationTime;
        float timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;
            tempScore = (int)(speedCurve.Evaluate(timeElapsed) * currentScore);
            scoreTxt.text = tempScore.ToString();
            yield return null;
        }

        tempScore = currentScore;
        scoreTxt.text = tempScore.ToString();
    }

    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource BGMSound;

    public void ClickPlay()
    {
        SoundManager.sound.PlaySound(clip);
        GameManager.game.GoToGameplay();
    }

    public void PlayBGMSound()
    {
        BGMSound.loop = true;
        BGMSound.Play();
    }
}
