using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject explosionPrefab;
    private Rigidbody2D rb;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FarToDestory(); //超过一定距离子弹就引爆
    }
    public void SetSpeed(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public void FarToDestory() 
    {
        float distanceX = Mathf.Abs(GameManager.instance.player.transform.position.x - transform.position.x);
        float distanceY = Mathf.Abs(GameManager.instance.player.transform.position.y - transform.position.y);
        if (distanceX > 10 || distanceY > 10)
        {
            GameObject shell = GameManager.instance.poolManager.Get(2);
            gameObject.SetActive(false);
        }

    }
}
