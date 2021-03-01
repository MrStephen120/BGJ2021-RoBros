using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float WaitTime = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("s"))
        {
            WaitTime = 0.5f;
        }
        if (Input.GetKeyDown("s"))
        {
            if (WaitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                WaitTime = 0.5f;
            }
            else
            {
                WaitTime -= Time.deltaTime;
            }
        }
        if (Input.GetKey("space"))
        {
            effector.rotationalOffset = 0;
        }
    }
}
