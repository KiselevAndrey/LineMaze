using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    [SerializeField, Min(0)] private float speed;

    private void Update()
    {
        transform.DOMoveY(transform.position.y - speed * Time.deltaTime, Time.deltaTime);
    }

    public void SetParameters(float speed, Vector2 position)
    {
        this.speed = speed;
        transform.position = position;
    }
}
