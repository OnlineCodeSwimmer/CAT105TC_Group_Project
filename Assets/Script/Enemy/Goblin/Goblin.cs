using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Goblin : MonoBehaviour
{

    //Goblin Monster System
    [Header("Basic Data")]
    public MonsterSO BasicData;
    [Header("Variables")]
    public float currenthealth;
    public bool isHurt;
    private bool isLive;
    [Header("Transform")]
    public Transform target;
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

    public void OnEnable()
    {
        animator.SetBool("Dead", false);
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        target = GameManager.instance.player.transform;
        isLive = true;
        isHurt= false;
        currenthealth = BasicData.maxHealth;
    }
    private void FixedUpdate()
    {
        if (!isHurt&&isLive)
        Move();
    }
    public void Move()
    {
        Vector2 dirction = (target.position - transform.position).normalized;
        rb.velocity = dirction * BasicData.speed;
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
            }
       }
    }

    public void KnockBack()
    {
        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 directionVector = transform.position - playerPosition;
        rb.AddForce(directionVector.normalized * 3, ForceMode2D.Impulse);
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }
}
