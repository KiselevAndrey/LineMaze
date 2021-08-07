using System;
using DG.Tweening;
using UnityEngine;

public class DotManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Range(0, 10)] private float bonusDuration;

    [Header("References")]
    [SerializeField] private Collider2D dotCollider;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private GameObject bonusCanvas;
    [SerializeField] private UnityEngine.UI.Text bonusName;

    public static Action LevelComplete;
    public static Action IHit;

    private enum Bonuses { Shrinking, Destruction };
    private Bonuses _lastBonus;

    private Sequence _bonusSequence;

    #region Awake OnEnable
    private void Awake()
    {
        _bonusSequence = DOTween.Sequence();
    }

    private void OnEnable()
    {
        Shrinking(false);
        Destruction(false);
        bonusCanvas.SetActive(false);
    }
    #endregion

    #region On Enter2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case ObjectNames.BlockWater:
                LevelComplete?.Invoke();
                break;

            case ObjectNames.BonusShrinking:
                _bonusSequence.Kill();
                _bonusSequence = DOTween.Sequence();
                _bonusSequence
                    .AppendCallback(() => NewBonus(Bonuses.Shrinking))
                    .AppendInterval(bonusDuration)
                    .AppendCallback(() => EndBonus());
                Lean.Pool.LeanPool.Despawn(collision.gameObject);
                break;

            case ObjectNames.BonusDestruction:
                _bonusSequence.Kill();
                _bonusSequence = DOTween.Sequence();
                _bonusSequence
                    .AppendCallback(() => NewBonus(Bonuses.Destruction))
                    .AppendInterval(bonusDuration)
                    .AppendCallback(() => EndBonus());
                Lean.Pool.LeanPool.Despawn(collision.gameObject);
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
    #endregion

    #region Bonuses
    private void NewBonus(Bonuses bonus)
    {
        EndBonus();

        switch (bonus)
        {
            case Bonuses.Shrinking:
                Shrinking(true);
                bonusName.text = BonusName.Shrinking;
                break;

            case Bonuses.Destruction:
                Destruction(true);
                bonusName.text = BonusName.Destruction;
                break;
        }

        _lastBonus = bonus;

        bonusCanvas.SetActive(true);
    }

    private void EndBonus()
    {

        switch (_lastBonus)
        {
            case Bonuses.Shrinking:
                Shrinking(false);
                break;

            case Bonuses.Destruction:
                Destruction(false);
                break;
        }

        bonusCanvas.SetActive(false);
    }

    private void Shrinking(bool active)
    {
        transform.DOScale(active ? 0.5f : 1f, 0.1f);
        trail.DOResize(active ? 0.25f : 0.5f, 0f, 0.1f);
    }

    private void Destruction(bool active) => dotCollider.isTrigger = active;
    #endregion

}
