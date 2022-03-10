using UnityEngine;
public class GameScroller : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(GameController.Instance.ScrollSpeed, 0);
    }
    private void Update()
    {
        if (GameController.Instance.IsGameOver)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}
