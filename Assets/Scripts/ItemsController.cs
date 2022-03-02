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
    [SerializeField] private GameObject blockImage;
    [SerializeField] private GameObject payButton;

    [SerializeField] private TextMeshProUGUI bonusTextPay;

    private void Start()
    {
        bonusTextPay.text = SaveDataManager.Instance.bonus.ToString();

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
        if (itemsProperty[currentIndex].Avaible)
        {
            SaveDataManager.Instance.itemIndex = currentIndex;
        }
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
        if (itemsProperty[currentIndex].Avaible)
        {
            SaveDataManager.Instance.itemIndex = currentIndex;

        }
        ShowProperty();
    }

    private void SetItemsProperty() ////
    {
        int price = 0;
        bool avaible;

        bool[] a = new bool[items.Count];
        SaveDataManager.Instance.itemsAvaible.CopyTo(a, 0);

        for (int i = 0; i < items.Count; i++)
        {
            avaible = a[i];
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
            blockImage.SetActive(true);
            payButton.SetActive(true);
        }
        else
        {
            priceText.text = "";
            blockImage.SetActive(false);
            payButton.SetActive(false);
        }
    }

    public void PayItem()
    {
        if (SaveDataManager.Instance.bonus >= itemsProperty[currentIndex].Price)
        {
            SaveDataManager.Instance.bonus -= itemsProperty[currentIndex].Price;
            bonusTextPay.text = SaveDataManager.Instance.bonus.ToString();

            itemsProperty[currentIndex].Avaible = true;
            priceText.text = "";
            blockImage.SetActive(false);
            payButton.SetActive(false);

            bool[] a = new bool[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                a[i] = itemsProperty[i].Avaible;
            }
            SaveDataManager.Instance.itemsAvaible = new bool[itemsProperty.Count];
            a.CopyTo(SaveDataManager.Instance.itemsAvaible, 0);
        }
    }
}
