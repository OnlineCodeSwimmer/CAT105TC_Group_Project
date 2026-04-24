using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRePosition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.transform.Translate(GameManager.instance.player.moveDirection * 35 + new Vector2(Random.Range(3f, -3f), Random.Range(3f, -3f)));
        }
    }
}
