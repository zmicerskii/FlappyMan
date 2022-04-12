using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    private BoxCollider2D _groundCollider;
    private float _groundHorizontalLength;
    
    private const float _numberPiecesOfBackgrounds = 2f;

    private void Awake()
    {
        _groundCollider = GetComponent<BoxCollider2D>();
        _groundHorizontalLength = _groundCollider.size.x;
    }
    
    private void Update()
    {
        if (transform.position.x < -_groundHorizontalLength)
        {
            RepositionBackground();
        }
    }
    
    private void RepositionBackground()
    {
        var groundOffset = new Vector2(_groundHorizontalLength * _numberPiecesOfBackgrounds, 0f);
        transform.position =(Vector2)transform.position + groundOffset;
    }
}
