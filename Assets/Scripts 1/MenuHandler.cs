using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

    public Button startButton;

 
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void NameInputEntered()
    {
        startButton.gameObject.SetActive(true);
    }

}
