using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] int LevelNumber;
    public void StartFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(LevelNumber);
    }

    public void MainMenu()
    {
        GameObject.Find("Audio Source").SetActive(false);
        SceneManager.LoadScene(0);
    }
}
