using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject[] Players;
    [SerializeField]
    GameObject CurrentPlayer;
    void Start()
    {
        for (int i = 1; i < Players.Length; i++)
        {
            Players[i].GetComponent<PlayerMovement>().enabled = false;
        }

        CurrentPlayer = Players[0];
    }
    public void ChangePlayer(GameObject player)
    {
        CurrentPlayer.GetComponent<PlayerMovement>().enabled = false;
        CurrentPlayer.layer = 8;
        CurrentPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        CurrentPlayer.GetComponent<Animator>().SetBool("Running", false);
        CurrentPlayer = player;
    }

}
