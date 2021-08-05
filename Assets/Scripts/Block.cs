using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRendrer;
    [SerializeField] Color standartColor;
    [SerializeField] Color hitColor;

    private void OnEnable()
    {
        spriteRendrer.color = standartColor;
    }
    public void ChangeToHitColor()
    {
        spriteRendrer.color = hitColor;
    }
}
