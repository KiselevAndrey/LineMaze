using UnityEngine;

public class DestroyLine : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            Lean.Pool.LeanPool.Despawn(collision.gameObject);
        }
    }
}
