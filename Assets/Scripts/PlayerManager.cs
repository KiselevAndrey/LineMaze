using DG.Tweening;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float speed;
    [SerializeField, Min(0), Tooltip("How fast the speed increases")] private float increasingSpeed;

    [Header("References")]
    [SerializeField] private Transform dotTransform;
    [SerializeField] private Camera mainCamera;

    private float _speed;

    #region Awake Destroy OnEnable
    private void Awake()
    {
        DotManager.LevelComplete += NewLevel;
        DotManager.IHit += EndGame;
    }

    private void OnDestroy()
    {
        DotManager.LevelComplete -= NewLevel;
        DotManager.IHit -= EndGame;
    }

    private void OnEnable()
    {
        _speed = speed;
    }
    #endregion

    private void Update()
    {
        MoveDot();
        MoveUp();
    }

    #region Move
    private void MoveUp()
    {
        transform.DOMoveY(transform.position.y + _speed * Time.deltaTime, Time.deltaTime);
    }

    private void MoveDot()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            dotTransform.DOMoveX(mainCamera.ScreenToWorldPoint(touch.position).x, 0f);
        }
    }
    #endregion

    #region Actions
    private void NewLevel()
    {
        Time.timeScale += increasingSpeed;
    }

    private void EndGame()
    {
        _speed = 0f;
    }
    #endregion
}
