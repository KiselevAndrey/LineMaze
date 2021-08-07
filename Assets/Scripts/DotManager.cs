using System;
using UnityEngine;

public class DotManager : MonoBehaviour
{
    public static Action LevelComplete;
    public static Action IHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case ObjectNames.BlockWater:
                LevelComplete?.Invoke();
                break;

            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Block block))
        {
            block.ChangeToHitColor();
            IHit?.Invoke();
        }
    }
}
