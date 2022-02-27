using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    public List<GameObject> items;
    [SerializeField] private GameObject screenPlace;
    private GameObject currentItem;
    private int currentIndex;

    [SerializeField] private GameObject NextButton;
    [SerializeField] private GameObject PreviousButton;

    private void Start()
    {
        currentIndex = SaveDataManager.Instance.itemIndex;
        currentItem = Instantiate(items[currentIndex]);
        currentItem.layer = 5;
        currentItem.transform.SetParent(screenPlace.transform, false);

        if (currentIndex == 0)
        {
            PreviousButton.SetActive(false);
        }
        if(currentIndex == items.Count - 1)
        {
            NextButton.SetActive(false);
        }
    }
    public void NextItem()
    {
        if (currentIndex < items.Count - 1)
        {
            Destroy(currentItem);
            currentItem = Instantiate(items[++currentIndex]);
            currentItem.layer = 5;
            currentItem.transform.SetParent(screenPlace.transform, false);

            PreviousButton.SetActive(true);
        }
        if(currentIndex == items.Count - 1)
        {
            NextButton.SetActive(false);
        }
        SaveDataManager.Instance.itemIndex = currentIndex;
    }
    public void PreviousItem()
    {
        if(currentIndex > 0)
        {
            Destroy(currentItem);
            currentItem = Instantiate(items[--currentIndex]);
            currentItem.layer = 5;
            currentItem.transform.SetParent(screenPlace.transform, false);

            NextButton.SetActive(true);
        }
        if (currentIndex == 0)
        {
            PreviousButton.SetActive(false);
        }
        SaveDataManager.Instance.itemIndex = currentIndex;
    }
}
