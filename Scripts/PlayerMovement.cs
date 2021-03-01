using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    //Settings
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 300f;
    [SerializeField] private LayerMask myLayerMask;
    [SerializeField] Vector2 deathKick = new Vector2(0.0f, 100.0f);
    [SerializeField] GameObject GameOverUI;
    //State
    bool isAlive = true;
    bool CanJump = true;

    //Cached Component References
    Rigidbody2D myRigidBody;
    Animator playerAnimator;
    Collider2D myCollider;
    SpriteRenderer Sprite;
    GameObject CharacterSwitcher;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        Sprite = GetComponent<SpriteRenderer>();
        CharacterSwitcher = GameObject.Find("CharacterSwitcher");
    }

    void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipSprite();
        Jump();
        PlatformFall();
        Die();
    }
    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); //value is -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        print(playerVelocity);

        bool playerMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("Running", playerMoving);
    }

    private bool IsGrounded()
    {
        float extraHeighText = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(myCollider.bounds.center, myCollider.bounds.size , 0f , Vector2.down, myCollider.bounds.extents.y + extraHeighText, myLayerMask);

        //Changes Ray Color
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        //Draws Ray
        Debug.DrawRay(myCollider.bounds.center, Vector2.down * (myCollider.bounds.extents.y + extraHeighText), rayColor);
        //Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;
    }
    private void Jump()
    {
        //If Player Presses Jump Input
        if (IsGrounded() && Input.GetButtonDown("Jump") && CanJump)
        {
            StartCoroutine(JumpTimer());
        }
    }

    IEnumerator JumpTimer()
    {
        playerAnimator.Play("Jump1");
        playerAnimator.Play("Jump2");
        myRigidBody.AddForce(transform.up * jumpSpeed);
        CanJump = false;
        yield return new WaitForSeconds(0.5f);
        CanJump = true;
    }
    private bool OnPlatform()
    {
        float extraHeighText = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(myCollider.bounds.center, myCollider.bounds.size, 0f, Vector2.down, myCollider.bounds.extents.y + extraHeighText, myLayerMask);

        if (raycastHit.collider.GetComponent<PlatformEffector2D>())
        {
            return true;
        }
        else

        return false;
    }
    private void PlatformFall()
    {
        if (OnPlatform() && Input.GetKeyDown("s"))
        {
            StartCoroutine(FallTimer());
        }
    }

    IEnumerator FallTimer()
    {
        myCollider.enabled = false;
        yield return new WaitForSeconds(0.2f);
        myCollider.enabled = true;
    }
    private void FlipSprite()
    {
        bool playerMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    private void GameOver()
    {
        GameOverUI.SetActive(true);
    }
    private void Die()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dead");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            GetComponent<PlayerSwitch>().enabled = false;
            CharacterSwitcher.GetComponent<CharacterSwitcher>().enabled = false;
            GameOver();
        }
    }
}
