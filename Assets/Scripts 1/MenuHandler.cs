using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MenuHandler : MonoBehaviour
{
    public TMPro.TMP_InputField  nameInput;
    public Button startButton;
    public TMPro.TMP_Text menuHighScoreText;

    private void Start()
    {
        if (PersistenceManager.Instance != null)
        {
            if (PersistenceManager.Instance.highScorer == "<NONE>") PersistenceManager.Instance.highScorer = PersistenceManager.Instance.playerName;
        }
        menuHighScoreText.text = $"Best Score : {PersistenceManager.Instance.highScorer} : {PersistenceManager.Instance.highScore}";

    }
    public void StartNew()
    {
        if (PersistenceManager.Instance != null)
        {
            if (PersistenceManager.Instance.highScorer == "<NONE>") PersistenceManager.Instance.highScorer = PersistenceManager.Instance.playerName;
        }
        menuHighScoreText.text = $"Best Score : {PersistenceManager.Instance.highScorer} : {PersistenceManager.Instance.highScore}";

        SceneManager.LoadScene(1);
    }

    public void NameInputEntered()
    {
        string playerName = nameInput.text;
        PersistenceManager.Instance.playerName = playerName;

        Debug.Log(playerName);
        startButton.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        PersistenceManager.Instance.SaveHighScorer();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

}
