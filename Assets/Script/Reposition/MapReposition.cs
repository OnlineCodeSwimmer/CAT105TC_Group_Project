using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReposition : MonoBehaviour
{
    public Vector3 playerPosition;
    public float differenceX;
    public float differenceY;
    public float directionX;
    public float directionY;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground") {
            playerPosition = GameManager.instance.player.transform.position;
            differenceX = Mathf.Abs(playerPosition.x - collision.transform.position.x);
            differenceY = Mathf.Abs(playerPosition.y - collision.transform.position.y);

            directionX = GameManager.instance.player.moveDirection.x < 0 ? -1 : 1;
            directionY = GameManager.instance.player.moveDirection.y < 0 ? -1 : 1;

            if (differenceX > differenceY)
            {
                collision.transform.Translate(Vector3.right * directionX * 80);
            }
            else if (differenceX < differenceY)
            {
                collision.transform.Translate(Vector3.up * directionY * 80);
            }
        }
    }
}
