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

    void Start()
    {
        menuHighScoreText.text = $"Best Score : {PersistenceManager.Instance.GetHighScorers()[0]} : {PersistenceManager.Instance.GetHighScores()[0]}";

        string pname = PersistenceManager.Instance.GetPlayerName();
        Debug.Log(pname);
        nameInput.text = pname;

        if (pname != "")
        {
            startButton.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene(2);
        }
    }
    public void StartNew()
    {
         SceneManager.LoadScene(1);
    }


    public void NameInputEntered()
    {
        string playerName = nameInput.text;
        PersistenceManager.Instance.SetPlayer(playerName);

        Debug.Log(playerName);
        startButton.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        PersistenceManager.Instance.SavePersistentData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

}
