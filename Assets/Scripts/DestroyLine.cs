using UnityEngine;

public class DestroyLine : MonoBehaviour
{
    [SerializeField] private Transform spawnLine;

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
}
