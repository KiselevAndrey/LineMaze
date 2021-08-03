using UnityEngine;

public class StrongFollowTarget : MonoBehaviour
{
    [SerializeField] Transform target;

    void LateUpdate()
    {
        transform.position = target.position;
    }
}
