using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Goblin : MonoBehaviour
{

    //Goblin Monster System
    [Header("Variables")]
    private float speed;
    private float damage;
    public float currenthealth;
    public bool isHurt;
    private bool isLive;
    [Header("Transform")]
    private Transform target;
    [Header("Components")]
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator= GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb=gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        target = GameManager.instance.player.transform;
        isLive = true;
        isHurt= false;
       
    }
    private void FixedUpdate()
    {
        if (!isHurt&&isLive)
        Move();
    }
    public void Move()
    {
        Vector2 dirction = (target.position - transform.position).normalized;
        rb.velocity = dirction * speed;
        if (dirction.x > 0)
            spriteRenderer.flipX = false;
        if (dirction.x < 0)
            spriteRenderer.flipX = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
       {
            currenthealth -= collision.GetComponent<Bullet>().damage;
            if (currenthealth > 0)
            {

                rb.velocity = Vector2.zero;
                animator.SetTrigger("Hurt");
                KnockBack();
                isHurt = true;
            }
            else
            {
                isLive = false;
                gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
                rb.velocity = Vector2.zero;
                animator.SetTrigger("Dead");
                GameManager.instance.player.GetExp();
                GameManager.instance.kill++;
            }
       }
    }

    public void KnockBack()
    {
        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 directionVector = transform.position - playerPosition;
        rb.AddForce(directionVector.normalized * 3, ForceMode2D.Impulse);
    }


    public void Init(float maxHealth,float speed,float damage)
    {
        currenthealth = maxHealth;
        this.speed = speed;
        this.damage = damage;
    }
    public void Dead()
    {
        spriteRenderer.color = Color.white;
        gameObject.SetActive(false);
    }
}
