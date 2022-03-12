using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private TextMeshProUGUI showTimeText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI currentBonusText;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScore;

    [SerializeField] private TextMeshProUGUI bonusText;

    [SerializeField] private GameObject throwsManager;

    [SerializeField] private GameObject timeline;

    [SerializeField] private AudioSource music;
    [SerializeField] private Animation soundAnim;
    [SerializeField] private Image soundBG;

    [SerializeField] private Animation vibrationAnim;
    [SerializeField] private Image vibrationBG;

    private bool isGameStarted = false;

    private int gameTime = 30;
    private float timelineScaleX;

    private int score = 0;

    public static float mod;

    private int time = 10;
    private bool adStarted = false;

    public int Score { get => score; set => score = value; }
    public bool AdStarted { get => adStarted; set => adStarted = value; }

    private void Awake()
    {
        mod = Screen.width / (float)Screen.height;
    }
    private void Start()
    {
        SaveDataManager.Instance.LoadData();

        if (SaveDataManager.Instance.bestScore > 0)
        {
            bestScore.text = "best " + SaveDataManager.Instance.bestScore.ToString();
        }

        ChangeBonusValue();

        scoreText.text = "";
        timelineScaleX = timeline.transform.localScale.x / gameTime;

        if (SaveDataManager.Instance.soundActive) SoundOn();
        else SoundOff();

        if (SaveDataManager.Instance.vibrationActive) VibrationOn();
        else VibrationOff();

    }
    private void Update()
    {
        ChangeTimeScale(); 
    }

    public void StartGame()
    {
        score = 0;
        isGameStarted = true;
        scoreText.text = score.ToString();
        throwsManager.SetActive(true);
        StartCoroutine(CountdownGame());
    }
    IEnumerator CountdownGame()
    {
        while (gameTime > 0)
        {
            yield return new WaitForSeconds(1);
            gameTime--;
        }
        if (gameTime == 0)
        {
            GameOver();
        }
    }

    public void ChangeBonusValue()
    {
        bonusText.text = SaveDataManager.Instance.Bonus.ToString();
    }

    private void ChangeTimeScale()
    {
        if (isGameStarted && timeline.transform.localScale.x > 0)
        {
            timeline.transform.localScale += Time.deltaTime * timelineScaleX * Vector3.left;
        }
    }
    public void ScoreGame()
    {
        score++;
        scoreText.text = score.ToString();
        SaveDataManager.Instance.Bonus += 5;

        ChangeBonusValue();
    }
    private void GameOver()
    {
        if (score > SaveDataManager.Instance.bestScore)
        {
            SaveDataManager.Instance.bestScore = score;
        }
        SaveDataManager.Instance.SaveData();

        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);

        GameObject.Find("Throws Manager").SetActive(false);

        currentScoreText.text = score.ToString();
        currentBonusText.text = "( " + (score * 5).ToString() + "      )";

        while (time >= 0)
        {
            yield return new WaitForSeconds(1);

            showTimeText.text = time.ToString();

            if (!adStarted)
            {
                time--;
            }
        }
        if (time == -1)
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void SoundControl(bool on)
    {
        if (on)
        {
            SoundOn();
            SaveDataManager.Instance.soundActive = true;
        }
        else
        {
            SoundOff();
            SaveDataManager.Instance.soundActive = false;
        }
    }
    private void SoundOn()
    {
        music.mute = false;
        soundAnim.Play("ToOn");
        soundBG.color = new Color(1, 1, 1, 1);
    }

    private void SoundOff()
    {
        music.mute = true;
        soundAnim.Play("ToOff");
        soundBG.color = new Color(1, 1, 1, 0.5f);
    }

    public void VibrationControl(bool on)
    {
        if (on)
        {
            VibrationOn();
            SaveDataManager.Instance.vibrationActive = true;
        }
        else
        {
            VibrationOff();
            SaveDataManager.Instance.vibrationActive = false;
        }
    }

    private void VibrationOn()
    {
        vibrationAnim.Play("ToOn");
        vibrationBG.color = new Color(1, 1, 1, 1);
    }

    private void VibrationOff()
    {
        vibrationAnim.Play("ToOff");
        vibrationBG.color = new Color(1, 1, 1, 0.5f);
    }
}
