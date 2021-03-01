using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] GameObject EndScreen;
    private bool Player1 = false;
    private bool Player2 = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player1")
        {
            Player1 = true;
        }
        if (collision.name == "Player2")
        {
            Player2 = true;
        }
        if (Player1 && Player2)
        {
            EndScreen.SetActive(true);
        }
        
    }
}
