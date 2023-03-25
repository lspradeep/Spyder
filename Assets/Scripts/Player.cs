using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;
    private bool isJump = false;
    private float force = 10f;
    private string leftLaneMask = "LeftLaneMask";
    private string rightLaneMask = "RightLaneMask";
    private string currentLane;
    private string targetLane;
    private SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D legsCollider;
    private bool isPlayerFeetOnGround;
    private string ANIM_JUMP = "JUMP";
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject web;
    private bool startShooting = false;
    private Coroutine shootingCoroutine = null;
    [SerializeField] int walkSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        CheckLane();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    private void Update()
    {
        isPlayerFeetOnGround = legsCollider.IsTouchingLayers(LayerMask.GetMask(leftLaneMask, rightLaneMask));
        animator.SetBool(ANIM_JUMP, !isPlayerFeetOnGround);
        //if (isPlayerFeetOnGround)
        //{
        //    rb.velocity = Vector2.up * walkSpeed * Time.deltaTime;
        //}
    }

    void FixedUpdate()
    {
        if (isJump && isPlayerFeetOnGround)
        {
            Jump();
            FlipPlayer();
            LandOppositeSide();

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPlayerFeetOnGround)
        {
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    public void OnJump(InputValue input)
    {
        isJump = input.isPressed;
    }

    public void OnFire(InputValue inputValue)
    {
        startShooting = inputValue.isPressed;
        if (startShooting)
        {
            ShootWeb();
        }
        else
        {
            StopShootWeb();
        }
    }

    private void ShootWeb()
    {
        if (shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(StartShoot());
        }
    }

    private void StopShootWeb()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    private IEnumerator StartShoot()
    {
        while (startShooting)
        {
            Instantiate(web, shootingPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

    private void FlipPlayer()
    {
        if (currentLane == rightLaneMask)
        {
            transform.eulerAngles = new Vector3(0, 180, 90);
        }
        else if (currentLane == leftLaneMask)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
    }

    private void Jump()
    {
        CheckLane();
        if (currentLane == rightLaneMask)
        {
            //rb.velocity = Vector2.left * force;
            rb.velocity = new Vector2(-force, force);
        }
        else if (currentLane == leftLaneMask)
        {
            //rb.velocity = Vector2.right * force;
            rb.velocity = new Vector2(force, force);
        }
    }

    private void LandOppositeSide()
    {
        if (targetLane == leftLaneMask)
        {
            rb.velocity = Vector2.left * force;
        }
        else if (targetLane == rightLaneMask)
        {
            rb.velocity = Vector2.right * force;
        }
    }

    private void CheckLane()
    {
        RaycastHit2D raycastHitLeftLane = Physics2D.Raycast(origin: transform.position, direction: transform.up, distance: 10, layerMask: LayerMask.GetMask(leftLaneMask));
        RaycastHit2D raycastHitRightLane = Physics2D.Raycast(origin: transform.position, direction: transform.up, distance: 10, layerMask: LayerMask.GetMask(rightLaneMask));
        if (raycastHitLeftLane.collider != null)
        {
            currentLane = rightLaneMask;
            targetLane = leftLaneMask;
        }
        else if (raycastHitRightLane.collider != null)
        {
            currentLane = leftLaneMask;
            targetLane = rightLaneMask;
        }

    }
}
