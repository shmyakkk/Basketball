using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemsController : MonoBehaviour
{
    private GameManager gameManager;

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


    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        gameManager.ChangeBonusValue();

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

        SaveDataManager.Instance.SaveData();
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

        SaveDataManager.Instance.SaveData();
    }

    private void SetItemsProperty() 
    {
        int price = 0;
        bool avaible = false;

        bool[] a = new bool[items.Count];
        SaveDataManager.Instance.itemsAvaible.CopyTo(a, 0);

        for (int i = 0; i < items.Count; i++)
        {
            if (SaveDataManager.Instance.itemsAvaible != null)
            {
                avaible = a[i];
            }
            if (i == 0)
            {
                avaible = true;
            }
            switch (i)
            {
                case 8:
                    price = 2000;
                    break;
                case 9:
                    price = 2000;
                    break;
                default:
                    price = i * 50;
                    break;
            }

            ItemProperty itemProperty = new ItemProperty(price, avaible);
            itemsProperty.Add(itemProperty);
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
        if (SaveDataManager.Instance.Bonus >= itemsProperty[currentIndex].Price)
        {
            SaveDataManager.Instance.Bonus -= itemsProperty[currentIndex].Price;

            gameManager.ChangeBonusValue();

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

            SaveDataManager.Instance.itemIndex = currentIndex;

            SaveDataManager.Instance.SaveData();
        }
    }
}
