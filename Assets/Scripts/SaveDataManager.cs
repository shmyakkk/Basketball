using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;

    public int bestScore = 0; //рекорд забитых м€чей
    public int bonus = 0; //кол-во накопленных монет

    public int itemIndex = 0; //выбранный м€ч

    public bool soundActive = true; //включен ли звук
    public bool vibrationActive = true; //включена ли вибраци€

    public bool[] itemsAvaible; //доступен или недоступен м€ч

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [System.Serializable]
    class Data
    {
        public int saveBestScore;
        public int saveBonus;
        public int saveItemIndex;
        public bool[] saveItemsAvaible;
    }
    public void SaveData()
    {
        Data data = new Data
        {
            saveBestScore = bestScore,
            saveBonus = bonus,
            saveItemIndex = itemIndex,
            saveItemsAvaible = itemsAvaible
        };
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            Data data = JsonUtility.FromJson<Data>(json);

            bestScore = data.saveBestScore;
            bonus = data.saveBonus;
            itemIndex = data.saveItemIndex;
            itemsAvaible = data.saveItemsAvaible;
        }
    }
}
