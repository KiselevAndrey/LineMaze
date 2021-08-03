public class DestroyLine : UnityEngine.MonoBehaviour
{
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Lean.Pool.LeanPool.Despawn(collision.gameObject);
        }
    }
}
