using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//欢迎来到玩家以及属性控制中心
public class Player : MonoBehaviour
{
    [Header("Move Parameter")]
    public Vector2 moveDirection;
    public float moveSpeed;
    private float horizontalInput;
    private float verticalInput;
    [Header("Components")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Texture2D cursorTexture;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }
    public void Update()
    {
        FlipByMouse();
        Mousesign();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move() //移动方法
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        rb.velocity = moveSpeed * moveDirection;
        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VelocityY", Mathf.Abs(rb.velocity.y));
    }
    private void FlipByMouse()//玩家跟着鼠标进行转向方法
    {
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dirX = mousePosition.x - transform.position.x;
        if (dirX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (dirX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    private void Mousesign() //将游戏中的光标更换
    {
        Vector2 mousePoint = new Vector2(201, 201);
        Cursor.SetCursor(cursorTexture, mousePoint, CursorMode.Auto);
    }

}



