using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    private Camera mainCamera;
    private float destroyOffset = 10f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Движение с текущей скоростью дороги
        transform.Translate(0, 0, -MovingRoad.Instance.roadSpeed * Time.deltaTime);
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(10); // +10 очков за препятствие
            Destroy(gameObject);
        }
    }
}