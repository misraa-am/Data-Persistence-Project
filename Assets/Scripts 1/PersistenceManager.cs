using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance;

    [System.Serializable]
    private class PlayerScores
    {
        //public PlayerScores(string nm, int s)
        //{
        //    playerName = nm;
        //    score = s;
        //}
        public string playerName;
        public int score;
    }

    private List<PlayerScores> highPlayerScores = new List<PlayerScores>();
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

    public int HighScorersContains(string nm)
    {
        for (int i = 0; i < highPlayerScores.Count; i++)
        {
            if (highPlayerScores[i].playerName == nm) return i;
        }
        return -1;
    }
    public void AddNewHighScorer(int score)
    {
        Debug.Log("Adding new high " + playerName + " : " + score);
        Debug.Log("On entry" + highScorers.Count);


        if (highScorers.Contains(playerName))
        {
            int idx1 = highScorers.IndexOf(playerName);
            highScorers.Remove(playerName);
            highScores.RemoveAt(idx1);

        }
        if (highScorers.Count > 9)
        {
            highScorers.RemoveAt(9);
            highScores.RemoveAt(9);
        }
        highScorers.Insert(0, playerName);
        highScores.Insert(0, score);

        int idx = HighScorersContains(playerName);
        if ( idx >= 0) { 
        
            highPlayerScores.RemoveAt(idx);
        }
        if (highPlayerScores.Count > 9)
        {
            highPlayerScores.RemoveAt(9);
            highPlayerScores.RemoveAt(9);
        }

        PlayerScores pl = new PlayerScores();
        pl.playerName = playerName;
        pl.score = score;
        Debug.Log("Insert new: " + highPlayerScores);
        highPlayerScores.Insert(0, pl);

        Debug.Log("On exit " + highScorers.Count);

    }

    public void GameOver(int score)
    {
        Debug.Log("Entering Game Over " + playerName + " : " + score);
        Debug.Log("On entry" + highScorers.Count);


        if (highScorers.Contains(playerName))
        {
            Debug.Log("has player name");
            int idx = highScorers.IndexOf(playerName);
            if (score > highScores[idx])
            {
                highScores[idx] = score;
            }
        }
        else
        {
            Debug.Log("No player name");

            bool lowerFound = false;
            int idx = 0;
            
            while (idx < highScores.Count && !lowerFound)
            {
                if (highScores[idx] < score) lowerFound = true;
                else idx++;
            }
            if (lowerFound || idx < 9)
            {
                if (idx == 9)
                {
                    highScorers.RemoveAt(9);
                    highScores.RemoveAt(9);
                }
                highScorers.Insert(idx, playerName);
                highScores.Insert(idx, score);
            }
        }

    }
    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public List<PlayerScores> highPlayerScores = new List<PlayerScores>();
        public List<string> highScorers;
        public List<int> highScores;
    }

    public void SavePersistentData()
    {
        SaveData data = new SaveData();
        data.highPlayerScores = highPlayerScores;
        data.highScores = highScores;
        data.highScorers = highScorers;
        data.playerName = playerName;

        Debug.Log("saving data; hiplayerscores count " + highPlayerScores.Count);
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        Debug.Log(Application.persistentDataPath + "/savefile.json");

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadPersistentData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        Debug.Log("Loading persistent data " + path);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            print("here load 1 count: " + data.highScorers.Count);
            highPlayerScores = data.highPlayerScores;
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
