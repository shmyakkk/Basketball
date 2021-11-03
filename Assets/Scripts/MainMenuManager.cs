using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScore;
    private void Awake()
    {
        SaveDataManager.Instance.LoadData();
        bestScore.text = "Best score: " + SaveDataManager.Instance.bestScore;
    }
    public void StartGame()
    {
        GameManager.isStartInMainMenu = true;
        SceneManager.LoadScene(1);
    }
}
