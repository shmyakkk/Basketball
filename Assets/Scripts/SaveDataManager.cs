using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;

    public int bestScore = 0;
    public int bonus = 0;
    public int itemIndex = 0;
    public bool soundActive = true;
    public bool vibrationActive = true;
    public bool[] avaibleItems = new bool[3] { false, false, false };

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
        public bool[] saveAvaibleItems;
    }
    public void SaveData()
    {
        Data data = new Data
        {
            saveBestScore = bestScore,
            saveBonus = bonus,
            saveItemIndex = itemIndex,
            saveAvaibleItems = avaibleItems
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
            avaibleItems = data.saveAvaibleItems;
        }
    }
}
