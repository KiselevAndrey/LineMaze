using DG.Tweening;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private bool isTest;
    [SerializeField] private float speed;
    [SerializeField, Min(0), Tooltip("How fast the speed increases")] private float increasingSpeed;

    [Header("References")]
    [SerializeField] private Transform dotTransform;

    private void Update()
    {
        MoveDot();
        MoveUp();
    }

    private void MoveUp()
    {
        transform.DOMoveY(transform.position.y + speed * Time.deltaTime, Time.deltaTime);
    }

    private void MoveDot()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            print(touch.position);
            dotTransform.DOMoveX(touch.position.x, 0.1f);
        }

        if(isTest && Input.GetMouseButtonUp(0))
        {
            dotTransform.DOMoveX(Input.mousePosition.x, 0.1f);
        }
    }
}
