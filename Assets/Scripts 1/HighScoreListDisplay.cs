using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreListDisplay : MonoBehaviour
{
    public List<Text> highScoreList = new List<Text>(10);
    // Start is called before the first frame update
    void Start()
    {
        DisplayList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(0);
        }
    }

    void DisplayList()
    {
        List<string> scorers = PersistenceManager.Instance.GetHighScorers();
        List<int> scores = PersistenceManager.Instance.GetHighScores();

        for (int i = 0; i < highScoreList.Count; i++)
        {
            Debug.Log(i);
            Text scoreText = highScoreList[i];
            Debug.Log(scoreText.name);
            Debug.Log(scorers[i] + "\t\t: " + scores[i]);
            scoreText.text = (scorers[i] + "\t\t: " + scores[i]);
        }
    }
}
