using UnityEngine;
using System;

public class BirdController : MonoBehaviour
{
    public static event Action BirdDied;
    
    [SerializeField] 
    private float _upForce = 200f;
    [SerializeField] 
    private MusicScriptableObject MusicScriptableObject;
    
    private static readonly int Flap = Animator.StringToHash("Flap");
    private static readonly int Die = Animator.StringToHash("Die");
    
    private bool _isDead;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private AudioSource _audioSource;

    private void Awake()
    {
        Column.BirdScored += BirdScoredHandler;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (_isDead) return;
        
        Fly();
    }

    private void Fly()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(new Vector2(0,_upForce));
            
            _audioSource.PlayOneShot(MusicScriptableObject.GetAudioClipByType(AudioType.Flap));
            _animator.SetTrigger(Flap);
        }
    }

    private void BirdScoredHandler()
    {
        _audioSource.PlayOneShot(MusicScriptableObject.GetAudioClipByType(AudioType.Scored));
    }
    
    private void OnCollisionEnter2D()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _isDead = true;
        
        _audioSource.PlayOneShot(MusicScriptableObject.GetAudioClipByType(AudioType.BirdDied));
        _animator.SetTrigger(Die);

        BirdDied?.Invoke();
    }

    private void OnDestroy()
    {
        Column.BirdScored -= BirdScoredHandler;
    }
}
