using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    public static event Action ChangeGameSpeed;
    
    public static GameController Instance
    {
        get
        {
            if ( instance == null )
            {
                instance = FindObjectOfType<GameController> ();
                if ( instance == null )
                {
                    var obj = new GameObject
                    {
                        name = nameof(GameController)
                    };
                    instance = obj.AddComponent<GameController>();
                }
            }
            return instance;
        }
    }

    public float ScrollSpeed = -1.5f;
    public bool IsGameOver;
    
    [Space (5)]
    [SerializeField] 
    private TMP_Text _scoreGame;
    [SerializeField]
    private TMP_Text _scoreResult;
    
    [SerializeField] 
    private GameObject _gameOverPanel;
    [SerializeField]
    private GameObject _gameScoreboard;
    
    [Space (5)]
    [Header("Increase speed from points scored")]
    [SerializeField, Tooltip("Multiple of this number increases the speed"), Range(1,20)] 
    private int _pointsScored;
    [SerializeField,Tooltip("Speed increases by this number"), Min(0)] 
    private float _increaseSpeed;
    [SerializeField, Min(0)] 
    private float _rateOfChangeSpeed;
    
    private static GameController instance;
    private int _score;
    
    private void Awake()
    {
        BirdController.BirdDied += BirdDiedHandler;
        Column.BirdScored += BirdScoredHandler;
        
        if ( instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy (gameObject);
        }
    }
    
    private void Update()
    {
        RestartGame();
    }
    
    private void RestartGame()
    {
        if (IsGameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    private void BirdScoredHandler()
    {
        if (IsGameOver)
        {
            return;
        }
            
        _score++;
        
        IncreaseGameSpeed(); 
        _scoreGame.text = _score.ToString();
    }
    
    private void IncreaseGameSpeed()
    {
        if (_score % _pointsScored == 0)
        {
            ScrollSpeed = Mathf.Lerp(ScrollSpeed, ScrollSpeed - _increaseSpeed, _rateOfChangeSpeed);
            ChangeGameSpeed?.Invoke();
        }
    }
    
    private void BirdDiedHandler()
    {
        _gameOverPanel.SetActive(true);
        _gameScoreboard.SetActive(false);
        
        _scoreResult.text = "SCORE: " + _score;        
        
        IsGameOver = true;
    }

    private void OnDestroy()
    {
        BirdController.BirdDied -= BirdDiedHandler;
        Column.BirdScored -= BirdScoredHandler;
    }
}
