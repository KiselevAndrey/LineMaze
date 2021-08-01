using System;
using UnityEngine;

public class DotManager : MonoBehaviour
{
    public static Action LevelComplete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Block Water")
        {
            LevelComplete?.Invoke();
        }
    }
}
