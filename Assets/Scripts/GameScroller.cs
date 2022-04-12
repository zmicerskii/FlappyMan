using UnityEngine;

public class GameScroller : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        BirdController.BirdDied += BirdDiedHandler;
        GameController.ChangeGameSpeed += ChangeGameSpeedHandler;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(GameController.Instance.ScrollSpeed, 0);
    }

    private void BirdDiedHandler()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void ChangeGameSpeedHandler()
    {
        _rigidbody2D.velocity = new Vector2(GameController.Instance.ScrollSpeed, 0);
    }

    private void OnDestroy()
    {
        BirdController.BirdDied -= BirdDiedHandler;
        GameController.ChangeGameSpeed -= ChangeGameSpeedHandler;
    }
}
