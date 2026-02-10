using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 1f;
    public Transform target;

    /*void Awake()
    {
        AudioManager.instance.Play("ChillBG");
    }*/

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            FollowPlayer();
        }
       
    }

    public void FollowPlayer()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -5f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
