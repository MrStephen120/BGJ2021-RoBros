using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public CharacterSwitcher CharSwitch;
    GameObject CharacterSwitcher;
    GameObject Camera;
    private void Start()
    {
        CharacterSwitcher = GameObject.Find("CharacterSwitcher");
        Camera = GameObject.Find("Main Camera");
    }

    private void OnMouseDown()
    {
        if (CharacterSwitcher.GetComponent<CharacterSwitcher>().enabled)
        {
            CharSwitch.ChangePlayer(this.gameObject);
            GetComponent<PlayerMovement>().enabled = true;
            Camera.GetComponent<dg_simpleCamFollow>().target = gameObject.transform;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            gameObject.layer = 9;
        }
    }
}
