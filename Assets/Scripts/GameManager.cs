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

    [SerializeField] private GameObject throwsManager;

    [SerializeField] private GameObject timeline;

    //[SerializeField] private GameObject best;

    [SerializeField] private Material ballMaterial;

    private bool isGameStarted = false;

    private int gameTime = 30;
    private float timelineScaleX;

    private int score = 0;

    public static float mod;
    [SerializeField] private Color defaultBallColor;

    [SerializeField] private AudioSource music;
    [SerializeField] private Animation soundAnim;
    [SerializeField] private Image soundBG;

    [SerializeField] private Animation vibrationAnim;
    [SerializeField] private Image vibrationBG;
    private bool VibrationCondition = true;
    

    private void Awake()
    {
        mod = Screen.width / (float)Screen.height;
    }
    private void Start()
    {
        SaveDataManager.Instance.LoadData();
        SetColors();

        if (SaveDataManager.Instance.bestScore > 0)
        {
            //best.SetActive(true);
            bestScore.text = "score " + SaveDataManager.Instance.bestScore.ToString();
        }
        scoreText.text = "";
        timelineScaleX = timeline.transform.localScale.x / gameTime;
    }
    private void Update()
    {
        ChangeTimeScale();
    }
    private void SetColors()
    {
        ballMaterial.color = SaveDataManager.Instance.ballColor == new Color(0, 0, 0, 0) ? defaultBallColor : SaveDataManager.Instance.ballColor;
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
            music.mute = false;
            soundAnim.Play("ToOn");
            soundBG.color = new Color(1, 1, 1, 1);
        }
        else
        {
            music.mute = true;
            soundAnim.Play("ToOff");
            soundBG.color = new Color(1, 1, 1, 0.5f);
        }
    }

    public void VibrationControl(bool on)
    {
        if (on)
        {
            vibrationAnim.Play("ToOn");
            vibrationBG.color = new Color(1, 1, 1, 1);
        }
        else
        {
            vibrationAnim.Play("ToOff");
            vibrationBG.color = new Color(1, 1, 1, 0.5f);
        }
        VibrationCondition = on;
    }
    public void Vibrate()
    {
        if (VibrationCondition)
        {
            Handheld.Vibrate();
        }
    }
}
