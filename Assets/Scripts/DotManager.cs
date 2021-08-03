using System;
using UnityEngine;

public class DotManager : MonoBehaviour
{
    public static Action LevelComplete;
    public static Action IHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Block Water")
        {
            LevelComplete?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Block Standart")
        {
            IHit?.Invoke();
        }
    }
}
