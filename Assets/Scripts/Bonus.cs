using System;
using System.Reflection.Emit;
using UnityEngine;

public class Bonus : MonoBehaviour
{
        
    protected virtual void OnTake(Character character)
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        var player = other.GetComponent<PlayerCharecter>();
        if (player != null)
        {
            OnTake(player);
        }
    }
}
