using UnityEngine;
using DG.Tweening;

public class DestroyLine : MonoBehaviour
{
    [SerializeField] private BlockSpawner spawnLine;

    private float _startLocalY;

    #region Awake OnDestroy
    private void Awake()
    {
        _startLocalY = transform.localPosition.y;
        
        DotManager.IHit += ClearField;
    }

    private void OnDestroy()
    { 
        DotManager.IHit -= ClearField;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Block":
            case "Bonus":
                Lean.Pool.LeanPool.Despawn(collision.gameObject);
                break;
        }
    }

    private void ClearField()
    {
        float upperPosition = spawnLine.UpperLinePosition;
        Sequence moveSequence = DOTween.Sequence();
        moveSequence
            .Append(transform.DOMoveY(upperPosition, 1f))
            .Append(transform.DOLocalMoveY(_startLocalY, 1f));
    }
}
