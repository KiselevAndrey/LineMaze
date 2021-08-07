using System;
using DG.Tweening;
using UnityEngine;

public class DotManager : MonoBehaviour
{
    [SerializeField] private Collider2D dotCollider;
    [SerializeField, Range(0, 10)] private float bonusDuration;

    public static Action LevelComplete;
    public static Action IHit;

    private Sequence _scaleSequence;
    private Sequence _destructionSequence;

    private void Awake()
    {
        _scaleSequence = DOTween.Sequence();
        _destructionSequence = DOTween.Sequence();
    }

    private void OnEnable()
    {
        transform.DOScale(1f, 0.1f);
        dotCollider.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case ObjectNames.BlockWater:
                LevelComplete?.Invoke();
                break;

            case ObjectNames.BonusShrinking:
                _scaleSequence.Kill();
                _scaleSequence = DOTween.Sequence();
                _scaleSequence
                    .Append(transform.DOScale(0.5f, 0.1f))
                    .AppendInterval(bonusDuration)
                    .Append(transform.DOScale(1f, 0.1f));
                break;

            case ObjectNames.BonusDestruction:
                _destructionSequence.Kill();
                _destructionSequence = DOTween.Sequence();
                _destructionSequence
                    .AppendCallback(() => dotCollider.isTrigger = true)
                    .AppendInterval(bonusDuration)
                    .AppendCallback(() => dotCollider.isTrigger = false);
                break;

            case ObjectNames.BlockStandart:
                Lean.Pool.LeanPool.Despawn(collision.gameObject);
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
