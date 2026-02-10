using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private Vector2 knockbackToSelf = new Vector2(3f, 5f);
    private Vector2 knockbackToPlayer = new Vector2(3f, 5f);

    public float knockbackDelayToSelf;
    public int damage;

    public void Die()
    {
        Destroy(gameObject);
    }

    public void HitPlayer(Transform playerTransform)
    {
        int direction = GetDirection(playerTransform);
        FindObjectOfType<SideScrollMovement>().knockbackPlayer(knockbackToPlayer, direction);
        FindObjectOfType<PlayerHealth>().TakeDamage(damage);
        GetComponent<EnemyMovement>().knockbackEnemy(knockbackToSelf, -direction, knockbackDelayToSelf);
    }

    private int GetDirection(Transform playerTransform)
    {
        if (transform.position.x > playerTransform.position.x)
        {
            // Our enemy  is to the right of the player
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
