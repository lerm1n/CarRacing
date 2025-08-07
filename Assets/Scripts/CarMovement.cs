using System.Collections;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float laneDistance = 3f;
    public float laneSwitchCooldown = 0.5f;
    public float laneSwitchSpeed = 5f; 
    
    private int currentLane = 1; // 0 = лево, 1 = центр, 2 = право
    private Vector3 targetPosition;
    private bool canSwitchLane = true;

     void Start()
    {
        targetPosition = transform.position;
    }
     
    void Update()
    {
        if (canSwitchLane)
        {
            if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
            {
                StartCoroutine(SwitchLane(-1)); // Влево
                
            }
            if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
            {
                StartCoroutine(SwitchLane(1));  // Вправо
                
            }
        }
        
        transform.position = Vector3.Lerp(
            transform.position, 
            targetPosition, 
            laneSwitchSpeed * Time.deltaTime
        );
    }
    
    IEnumerator SwitchLane(int direction)
    {
        canSwitchLane = false;
        currentLane += direction;
        targetPosition.x += direction * laneDistance;
        
        yield return new WaitForSeconds(laneSwitchCooldown);
        canSwitchLane = true;
    }
}
