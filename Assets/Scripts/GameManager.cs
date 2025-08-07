using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Score Settings")]
    public int score = 0;
    public int scoreMultiplier = 1;
    public Text scoreText;
    public float scoreUpdateInterval = 0.1f;
    private float lastScoreUpdateTime;
    
    [Header("Speed Settings")]
    public float initialSpeed = 5f;
    public float maxSpeed = 15f;
    public float accelerationRate = 0.1f;
    public float accelerationInterval = 10f;
    private float lastAccelerationTime;
    
    [Header("Spawn Rate Settings")]
    public float initialSpawnInterval = 2f;
    public float minSpawnInterval = 0.5f;
    public float spawnIntervalDecrease = 0.1f;
    
    private ObjectGenerate objectGenerator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        objectGenerator = FindObjectOfType<ObjectGenerate>();
        if (objectGenerator != null)
        {
            objectGenerator.spawnInterval = initialSpawnInterval;
        }
        
        UpdateScoreText();
        lastAccelerationTime = Time.time;
        lastScoreUpdateTime = Time.time;
    }

    private void Update()
    {
        // Автоматическое увеличение очков со временем
        if (Time.time - lastScoreUpdateTime >= scoreUpdateInterval)
        {
            AddScore(1 * scoreMultiplier);
            lastScoreUpdateTime = Time.time;
        }
        
        // Плавное ускорение игры
        if (Time.time - lastAccelerationTime >= accelerationInterval)
        {
            AccelerateGame();
            lastAccelerationTime = Time.time;
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void AccelerateGame()
    {
        // Увеличиваем скорость дороги
        float newSpeed = MovingRoad.Instance.roadSpeed + accelerationRate;
        newSpeed = Mathf.Min(newSpeed, maxSpeed);
        MovingRoad.Instance.SetSpeed(newSpeed);
        
        // Уменьшаем интервал спавна
        if (objectGenerator != null)
        {
            float newSpawnInterval = objectGenerator.spawnInterval - spawnIntervalDecrease;
            newSpawnInterval = Mathf.Max(newSpawnInterval, minSpawnInterval);
            objectGenerator.spawnInterval = newSpawnInterval;
            
            objectGenerator.nextSpawnTime = Time.time;
        }
        
        Debug.Log($"Speed: {newSpeed}, Spawn Rate: {objectGenerator?.spawnInterval ?? 0}");
    }
}