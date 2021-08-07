public class DestroyLine : UnityEngine.MonoBehaviour
{
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case TagNames.Block:
            case TagNames.Bonus:
                Lean.Pool.LeanPool.Despawn(collision.gameObject);
                break;
        }
    }
}
