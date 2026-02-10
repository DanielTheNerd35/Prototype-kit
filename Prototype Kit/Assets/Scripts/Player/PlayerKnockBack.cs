using UnityEngine;

public class PlayerKnockBack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float bounceForce;
    private Rigidbody2D rb;
    private float halfHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        halfHeight = sr.bounds.extents.y;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CollisionWithEnemy(other);
        }
    }

    private void CollisionWithEnemy(Collision2D other)
    {
        EnemyLogic enemy = other.gameObject.GetComponent<EnemyLogic>();

        if (Physics2D.Raycast(transform.position, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Enemy")))
        {
            // Hit enemy top
            // Player should bounce upwards
            // enemy should die
            Vector2 velocity = rb.linearVelocity;
            velocity.y = 0f;
            rb.linearVelocity = velocity;
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            enemy.Die();
            Debug.Log("Hit enemy top");
        }
        else 
        {
            // Hit side
            // Player should be knocked back
            // Player should flash red
            // Player should take damage
            // Enemy should be knocked back the other direction
            // Enemy should also pause movement
            enemy.HitPlayer(transform);
            Debug.Log("Hit enemy side");
        }
    }
    
}
