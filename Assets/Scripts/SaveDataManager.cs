using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;

    public int bestScore = 0;
    public int itemIndex = 0;

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
        public int saveItemIndex;
    }
    public void SaveData()
    {
        Data data = new Data
        {
            saveBestScore = bestScore,
            saveItemIndex = itemIndex
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
            itemIndex = data.saveItemIndex;
        }
    }
}
