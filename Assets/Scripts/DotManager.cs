using System;
using DG.Tweening;
using UnityEngine;

public class DotManager : MonoBehaviour
{
    public static Action LevelComplete;
    public static Action IHit;

    private Sequence scaleSequence;

    private void Awake()
    {
        scaleSequence = DOTween.Sequence();
    }

    private void OnEnable()
    {
        transform.DOScale(1f, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case ObjectNames.BlockWater:
                LevelComplete?.Invoke();
                break;

            case ObjectNames.BonusShrinking:
                scaleSequence.Kill();
                scaleSequence = DOTween.Sequence();
                scaleSequence
                    .Append(transform.DOScale(0.5f, 0.1f))
                    .AppendInterval(5f)
                    .Append(transform.DOScale(1f, 0.1f));
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
