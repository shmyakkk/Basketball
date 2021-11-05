using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectColor : MonoBehaviour
{
    private SelectionPointer selectionPointerWall;
    private SelectionPointer selectionPointerBall;
    private RectTransform posButton;
    private Button button;
    private Color color;

    private void Start()
    {
        selectionPointerWall = GameObject.Find("Selected BG").GetComponent<SelectionPointer>();
        selectionPointerBall = GameObject.Find("Selected Ball").GetComponent<SelectionPointer>();
        posButton = GetComponent<RectTransform>();
        color = GetComponent<Image>().color;
        button = GetComponent<Button>();
        button.onClick.AddListener(SetColor);
    }
    private void SetColor()
    {
        if(gameObject.CompareTag("background color"))
        {
            SaveDataManager.Instance.wallColor = color;
            SaveDataManager.Instance.selectedWallPosition = posButton.localPosition;
            selectionPointerWall.SetPosition(posButton.localPosition);
        }
        if (gameObject.CompareTag("ball"))
        {
            SaveDataManager.Instance.ballColor = color;
            SaveDataManager.Instance.selectedBallPosition = posButton.localPosition;
            selectionPointerBall.SetPosition(posButton.localPosition);
        }
        SaveDataManager.Instance.SaveData();
    }
}
