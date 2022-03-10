using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public float ScrollSpeed = -1.5f;
    public bool IsGameOver;
    
    [SerializeField] 
    private Text[] _TextScore;
    [SerializeField] 
    private GameObject _gameOverText;
    [SerializeField]
    private GameObject _scoreText;

    private int _score;
    
    private void Awake()
    {
        GlobalEvents.BirdDied += OnBirdDied;
        GlobalEvents.BirdScored += OnBirdScored;
        
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (IsGameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void OnBirdScored()
    {
        if (IsGameOver)
        {
            return;
        }
            
        _score++;
        _TextScore[0].text = "SCORE: " + _score;
    }
    private void OnBirdDied()
    {
        _gameOverText.SetActive(true);
        _scoreText.SetActive(false);
        _TextScore[1].text = "SCORE: " + _score;        
        
        IsGameOver = true;
    }

    private void OnDestroy()
    {
        GlobalEvents.BirdDied -= OnBirdDied;
        GlobalEvents.BirdScored -= OnBirdScored;
    }
}
