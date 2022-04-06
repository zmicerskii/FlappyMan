using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    [SerializeField] 
    private GameObject _prefab;
    [SerializeField]
    private int _poolSize = 5;
    [SerializeField] 
    private float _spawnRate = 4f;
    [SerializeField] 
    private float _offsetMin = -1f;
    [SerializeField] 
    private float _offsetMax = 3.5f;
    [SerializeField] 
    private Vector2 _objectPoolPosition = new(-15f, -25f);
    [SerializeField] 
    private float _spawnXPosition = 12f;
    
    private GameObject[] _columns;
    private float _timeSinceLastSpawned;
    private int _currentColumn;
    
    private void Awake()
    {
        _columns = new GameObject[_poolSize];
        for (var i = 0; i < _poolSize; i++)
        {
            _columns[i] = Instantiate(_prefab, _objectPoolPosition, Quaternion.identity);
        }
    }
    
    private void Update()
    {
        _timeSinceLastSpawned += Time.deltaTime;
        if (!GameController.Instance.IsGameOver && _timeSinceLastSpawned >= _spawnRate)
        {
            _timeSinceLastSpawned = 0;
            var spawnYPosition = Random.Range(_offsetMin, _offsetMax);
            _columns[_currentColumn].transform.position = new Vector2(_spawnXPosition, spawnYPosition);
            _currentColumn++;
            
            if (_currentColumn >= _poolSize)
            {
                _currentColumn = 0;
            }
        }
    }
}
