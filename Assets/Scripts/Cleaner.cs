using DG.Tweening;

public class Cleaner : UnityEngine.MonoBehaviour
{
    private void OnEnable()
    {
        Sequence moveSequence = DOTween.Sequence();
        moveSequence
            .Append(transform.DOLocalMoveY(-10, 0.1f))
            .Append(transform.DOLocalMoveY(10, 0.1f));
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Lean.Pool.LeanPool.Despawn(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Lean.Pool.LeanPool.Despawn(collision.gameObject);
        }
    }
}
