using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private TextMeshProUGUI bonusTextMenu;
    [SerializeField] private TextMeshProUGUI bonusTextGame;

    [SerializeField] private GameObject throwsManager;

    [SerializeField] private GameObject timeline;

    private bool isGameStarted = false;

    private int gameTime = 30;
    private float timelineScaleX;

    private int score = 0;

    public static float mod;

    [SerializeField] private AudioSource music;
    [SerializeField] private Animation soundAnim;
    [SerializeField] private Image soundBG;

    [SerializeField] private Animation vibrationAnim;
    [SerializeField] private Image vibrationBG;

    

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
        bonusTextMenu.text = SaveDataManager.Instance.bonus.ToString();
        bonusTextGame.text = SaveDataManager.Instance.bonus.ToString();

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
        SaveDataManager.Instance.bonus += 5;
        bonusTextGame.text = SaveDataManager.Instance.bonus.ToString();
    }
    private void GameOver()
    {
        if (score > SaveDataManager.Instance.bestScore)
        {
            SaveDataManager.Instance.bestScore = score;
        }
        SaveDataManager.Instance.SaveData();
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
