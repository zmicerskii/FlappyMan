using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] 
    private float _upForce = 200f;
    
    private static readonly int Flap = Animator.StringToHash("Flap");
    private static readonly int Die = Animator.StringToHash("Die");
    
    private bool _isDead;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_isDead) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(new Vector2(0,_upForce));
            _animator.SetTrigger(Flap);
        }
    }
    private void OnCollisionEnter2D()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _isDead = true;
        _animator.SetTrigger(Die);

        GlobalEvents.BirdDied?.Invoke();
    }
}
