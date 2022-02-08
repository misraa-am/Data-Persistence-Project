using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public TMPro.TMP_InputField  nameInput;
    public Button startButton;
 
    public void StartNew()
    {
        if (PersistenceManager.Instance != null)
        {
            if (PersistenceManager.Instance.highScorer == "<NONE>") PersistenceManager.Instance.highScorer = PersistenceManager.Instance.playerName;
        }
        SceneManager.LoadScene(1);
    }

    public void NameInputEntered()
    {
        string playerName = nameInput.text;
        PersistenceManager.Instance.playerName = playerName;

        Debug.Log(playerName);
        startButton.gameObject.SetActive(true);
    }

}
