using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance;
    public string highScorer;
    public string playerName;
    public int highScore;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScorer();

    }
    [System.Serializable]
    class SaveData
    {
        public string highScorer;
        public string playerName;
        public int highScore;
    }

    public void SaveHighScorer()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScorer = highScorer;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScorer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highScorer = data.highScorer;
            playerName = data.playerName;
        }
        else
        {
            highScore = 0;
            highScorer = "<NONE>";
            playerName = "<NONE>";
        }
    }

}
