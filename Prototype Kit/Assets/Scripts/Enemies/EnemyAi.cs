using UnityEngine;
using TMPro;

public class EnemyAi : MonoBehaviour
{
    public EnemyVision vision;
    public Transform[] patrolPoints; // A list of all the waypoints the enemy can go to
    public int targetPoint;
    public float mSpeed;
    public Transform player;

    void Start() {
        //targetPoint = 0; it starts the enemies at the first waypoint but since i have multiple enemies, I do not want all of them to start at target point 0.
    }

    void Update() {

        Vector3 direction = (patrolPoints[targetPoint].position - transform.position).normalized;

         // Rotate to face movement direction
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        //When the enemy reaches a waypoint it goes to the other
        if (transform.position == patrolPoints[targetPoint].position && vision.playerDetected != true)
        {
            increaseTargetInt();
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, mSpeed * Time.deltaTime);

        if (vision.playerDetected)
        {
            transform.position += transform.forward * mSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.SetActive(false); //For some reason, setting the player to disabled won't make the GameManager switch to the Death Scene
            Destroy(collision.gameObject);
            Debug.Log("Player is Dead!");
        }
    }

    void increaseTargetInt() {
        targetPoint++;
        
        //Resets the waypoints so the enemy can loop again to each waypoint
        if(targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
