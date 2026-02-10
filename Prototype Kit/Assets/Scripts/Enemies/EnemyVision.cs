using UnityEngine;
using TMPro;

public class EnemyVision : MonoBehaviour
{

    bool isInRange, isNotHidden;

    public GameObject player;
    public  TMP_Text RangeText, HiddenText, DetectedText;
    public float detectedRange = 10;
    public float coneAngle = 60f;
    public int rayCount = 20;
    public bool playerDetected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       isInRange = false;
       isNotHidden = false;
       playerDetected = false;

       float halfAngle = coneAngle / 2f;

        // Checks if the player is in range of the enemy
        if(Vector3.Distance(transform.position, player.transform.position) < detectedRange)
        {
            isInRange = true;
            //RangeText.text = "In Range"; 
            //RangeText.color = Color.red;
        }
        else    //All the comments sections was from following a tutorial 
        {
            //RangeText.text = "Not In Range";
            //RangeText.color = Color.green;
        }
        
        for (int i = 0; i < rayCount; i++)
        {
            // Get t value between 0 and 1
            float t = (float)i / (rayCount - 1);

            // Convert t to angle between -halfAngle and +halfAngle
            float angle = Mathf.Lerp(-halfAngle, halfAngle, t);

            // Rotate the forward direction by angle around the Y axis
            Vector3 rayDir = Quaternion.Euler(0, angle, 0) * transform.forward;

            // Raycast
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDir, out hit, detectedRange))
            {
                Debug.DrawRay(transform.position, rayDir * detectedRange, Color.red);

                if (hit.transform == player.transform)
                {
                    isNotHidden = true;
                    //HiddenText.text = "Not Hidden";
                    //HiddenText.color = Color.red;
                    break;  // No need to keep checking
                }
            }
            else
            {
                //HiddenText.text = "Hidden";
                //HiddenText.color = Color.green;
                Debug.DrawRay(transform.position, rayDir * detectedRange, Color.green);
            }
        }

        if (isInRange && isNotHidden)
        {
            playerDetected = true;
            DetectedText.text = "Player Detected";
            DetectedText.color = Color.red;
        }
        else
        {
            playerDetected = false;
            DetectedText.text = "Player Not Detected";
            DetectedText.color = Color.green;
        }
    }

    private void OnDrawGizmosSelected()
   {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectedRange);
   }
}
