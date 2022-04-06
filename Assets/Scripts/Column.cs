using UnityEngine;
using System;

public class Column : MonoBehaviour
{
    public static event Action BirdScored;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bird"))
        {
            BirdScored?.Invoke();
        }
    }
}
