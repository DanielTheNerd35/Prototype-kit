using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow3D : MonoBehaviour
{

    public Transform target; //this is the transform of the object we want to follow

    public float rotationSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*void Awake()
    {
        //FindObjectOfType<AudioManager>().Play("Theme");
        AudioManager.instance.Play("REgular Theme");
    }

    void Start()
    {
        Scene activeScene = SceneManager.GetActiveScene();
         Debug.Log("Active Scene Name: " + activeScene.name);
         Debug.Log("Active Scene Build Index: " + activeScene.buildIndex);

         if (activeScene.buildIndex == 5)
         {
            AudioManager.instance.Stop("REgular Theme");
            AudioManager.instance.Play("Defeat");
         }

         if (activeScene.buildIndex == 2 || activeScene.buildIndex == 3)
         {
            AudioManager.instance.Stop("REgular Theme");
            AudioManager.instance.Play("SpookTheme");
         }

         if (activeScene.buildIndex == 4)
         {
            AudioManager.instance.Stop("SpookTheme");
            AudioManager.instance.Stop("REgular Theme");
            AudioManager.instance.Play("Victory");
         }
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = target.position - transform.position;

         // Desired rotation that looks at player
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Smoothly rotate toward the player
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
