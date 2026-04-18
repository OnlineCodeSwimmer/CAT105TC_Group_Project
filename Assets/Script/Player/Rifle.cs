using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("variable")]
    public float interval;
    private float timer;
    private Vector2 mousePosition;
    private Vector2 direction;
    [Header("Components")]
    public GameObject bulletPrefab;
    public GameObject shellPrefab;
    private Transform muzzle;
    private Transform shellPosition;
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        animator=GetComponent<Animator>();
        muzzle = transform.Find("Muzzle");
        shellPosition = transform.Find("Shell");

    }

    // Update is called once per frame
    private void Update()
    {
        
        Direction(); //控制枪旋转方向
    }

    private void FixedUpdate()
    {
        Shoot(); //间隔射击系统,避免子弹腹泻式射击
    }

    private void Direction() 
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;
        if (mousePosition.x < transform.position.x)
            transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), 1);
        if (mousePosition.x > transform.position.x)
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), 1);

    }
    private void Shoot()
    {
        if(timer!=0)
        {
            timer-=Time.deltaTime;
            if(timer<=0)
                timer=0;
        }
        if(Input.GetMouseButton(0))
        {
            if(timer==0)
            {
                Fire();
                timer = interval;
            }
        }
    }
    private void Fire()
    {
        animator.SetTrigger("Shoot");
        GameObject bullet = GameManager.instance.poolManager.Get(0);
        bullet.GetComponent<Bullet>().SetSpeed(direction);//调用bullet的飞行函数
        bullet.transform.position= muzzle.position;
        GameObject shell=GameManager.instance.poolManager.Get(1);
        shell.transform.position = shellPosition.position;
    }
}
