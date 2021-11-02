using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    private Button button;
    private MainMenuManager menuManagerScript;
    public Color color;

    void Start()
    {
        menuManagerScript = GameObject.Find("Canvas").GetComponent<MainMenuManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetColor);
    }
    void SetColor()
    {
        menuManagerScript.StartGame(color);
    }
}
