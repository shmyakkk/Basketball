using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionPointer : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 defaultPos = new Vector3(-210, 300, 0);
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(gameObject.name=="Selected BG")
        {
            rectTransform.localPosition = SaveDataManager.Instance.selectedWallPosition == Vector3.zero ? defaultPos : SaveDataManager.Instance.selectedWallPosition;
        }
        if(gameObject.name=="Selected Ball")
        {
            rectTransform.localPosition = SaveDataManager.Instance.selectedBallPosition == Vector3.zero ? defaultPos : SaveDataManager.Instance.selectedBallPosition;
        }
    }
    public void SetPosition(Vector3 position)
    {
        rectTransform.localPosition = position;
    }
}
