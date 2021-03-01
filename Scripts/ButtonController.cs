using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject Cause;
    [SerializeField] GameObject Effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Effect.SetActive(false);
    }
}
