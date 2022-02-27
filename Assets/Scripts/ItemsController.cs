using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemsController : MonoBehaviour
{
    public List<GameObject> items;
    private List<ItemProperty> itemsProperty = new List<ItemProperty>(100);

    [SerializeField] private GameObject screenPlace;
    private GameObject currentItem;
    private int currentIndex;

    [SerializeField] private GameObject NextButton;
    [SerializeField] private GameObject PreviousButton;

    [SerializeField] private TextMeshProUGUI priceText;

    private void Start()
    {
        SetItemsProperty();

        currentIndex = SaveDataManager.Instance.itemIndex;
        currentItem = Instantiate(items[currentIndex]);
        currentItem.GetComponent<BallController>().enabled = false;

        ShowProperty();

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
            currentItem.GetComponent<BallController>().enabled = false;

            currentItem.layer = 5;
            currentItem.transform.SetParent(screenPlace.transform, false);

            PreviousButton.SetActive(true);
        }
        if(currentIndex == items.Count - 1)
        {
            NextButton.SetActive(false);
        }
        SaveDataManager.Instance.itemIndex = currentIndex;
        ShowProperty();
    }
    public void PreviousItem()
    {
        if(currentIndex > 0)
        {
            Destroy(currentItem);
            currentItem = Instantiate(items[--currentIndex]);
            currentItem.GetComponent<BallController>().enabled = false;

            currentItem.layer = 5;
            currentItem.transform.SetParent(screenPlace.transform, false);

            NextButton.SetActive(true);
        }
        if (currentIndex == 0)
        {
            PreviousButton.SetActive(false);
        }
        SaveDataManager.Instance.itemIndex = currentIndex;
        ShowProperty();
    }

    private void SetItemsProperty()
    {
        Debug.Log(itemsProperty.Count);
        int price = 0;
        bool avaible;
        for (int i = 0; i < items.Count; i++)
        {
            if (i == 0)
            {
                avaible = true;
            }
            else
            {
                avaible = false;
            }
            ItemProperty itemProperty = new ItemProperty(price, avaible);
            itemsProperty.Add(itemProperty);
            price += 50;
        }
    }

    private void ShowProperty()
    {
        if (!itemsProperty[currentIndex].Avaible)
        {
            priceText.text = itemsProperty[currentIndex].Price.ToString();
        }
        else
        {
            priceText.text = "";
        }
    }
}
