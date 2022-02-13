using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance;
    private List<string> highScorers = new List<string>();
    private List<int> highScores = new List<int>();
    private string playerName = "";

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
        LoadPersistentData();

    }
    [System.Serializable]
    
    class SaveData
    {
        public string playerName;
        public List<string> highScorers;
        public List<int> highScores;
    }

    public void SavePersistentData()
    {
        SaveData data = new SaveData();
        data.highScores = highScores;
        data.highScorers = highScorers;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        Debug.Log(Application.persistentDataPath + "/savefile.json");

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public List<string> GetHighScorers()
    {
        return highScorers;
    }
    public List<int> GetHighScores()
    {
        return highScores;
    }

    public string GetPlayerName() { return playerName;  }

    public void SetPlayer(string pname)
    {
        playerName = pname;

        Debug.Log("setting player " + highScorers.Count);
        if (highScorers[0] == "")
        {
            highScorers[0] = playerName;
            highScores[0] = 0;
        }
    }

    public void AddNewHighScorer(int score)
    {
        Debug.Log("On entry" + highScorers.Count);
        if (highScorers.Contains(playerName))
        {
            int idx = highScorers.IndexOf(playerName);
            highScorers.Remove(playerName);
            highScores.RemoveAt(idx);

        }
        if (highScorers.Count > 9)
        {
            highScorers.RemoveAt(9);
            highScores.RemoveAt(9);
        }
        highScorers.Insert(0, playerName);
        highScores.Insert(0, score);
        Debug.Log("On exit " + highScorers.Count);

    }

    public void LoadPersistentData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        Debug.Log(path);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            print("here load 1 count: " + data.highScorers.Count);
            highScorers = data.highScorers;
            highScores = data.highScores;
            playerName = data.playerName;
        }
        else
        {
            highScores.Insert(0, 0);
            highScorers.Insert(0, "");
        }
    }

}
