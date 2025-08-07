using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    private InfiniteMovingRoad roadController;
    private float roadSpeed;
    private Camera mainCamera;
    private float destroyOffset = 10f;

    private void Start()
    {
        roadController = FindObjectOfType<InfiniteMovingRoad>();
        
        if (roadController == null)
        {
            Debug.LogError("Не найден MovingRoad в сцене!");
            return;
        }
        
        roadSpeed = roadController.roadSpeed;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (roadController != null)
        {
            // Двигаем объект с той же скоростью, что и дорога
            transform.Translate(0, 0, -roadSpeed * Time.deltaTime);
        }
        
        CheckIfBehindCamera();
    }
    
    private void CheckIfBehindCamera()
    {
        if (mainCamera == null) return;
        
        if (transform.position.z < mainCamera.transform.position.z - destroyOffset)
        {
            Destroy(gameObject);
        }
    }
}