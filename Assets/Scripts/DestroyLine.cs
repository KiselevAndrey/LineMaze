using UnityEngine;

public class DestroyLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Lean.Pool.LeanPool.Despawn(collision.gameObject);
        }
    }
}
