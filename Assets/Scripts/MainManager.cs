using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text highScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    //private List<string> highScorers = new List<string>();
    //private List<int> highScores = new List<int>();
    //private string playerName;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        highScoreText.text = $"Best Score : {PersistenceManager.Instance.GetHighScorers()[0]} : {PersistenceManager.Instance.GetHighScores()[0]}";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if (m_Points > PersistenceManager.Instance.GetHighScores()[0]) {
            Debug.Log("Here");
            PersistenceManager.Instance.AddNewHighScorer(m_Points);
            //PersistenceManager.Instance.highScore = m_Points;
            //PersistenceManager.Instance.highScorer = PersistenceManager.Instance.playerName;
        }
        highScoreText.text = $"Best Score : {PersistenceManager.Instance.GetHighScorers()[0]} : {PersistenceManager.Instance.GetHighScores()[0]}";
    }
 
  

    private void Load()
    {
        PersistenceManager.Instance.LoadPersistentData();
       
    }
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        PersistenceManager.Instance.SavePersistentData();
    }
}
