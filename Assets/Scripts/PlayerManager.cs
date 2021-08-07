using DG.Tweening;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float speed;
    [SerializeField, Min(0), Tooltip("How fast the speed increases")] private float increasingSpeed;
    [SerializeField, Min(0)] private float addScoreToSec;

    [Header("References")]
    [SerializeField] private Transform dotTransform;
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private Camera mainCamera;

    [Header("UI")]
    [SerializeField] private UnityEngine.UI.Text scoreText;
    [SerializeField] private UnityEngine.UI.Text scoreAfterGameText;
    [SerializeField] private UnityEngine.UI.Text recordText;

    private float _speed;
    private float _score;
    private bool _inGame;

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
        _score = 0;
        dotTransform.DOLocalMoveX(0, 0.1f);

        _inGame = true;
    }
    #endregion

    #region Update
    private void Update()
    {
        if (!_inGame) return;

        MoveDot();
        MoveUp();

        AddScoreFromUpdate();
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
            canvasTransform.DOMoveX(dotTransform.position.x, 0.1f);
        }
    }
    #endregion

    private void AddScoreFromUpdate()
    {
        _score += addScoreToSec * _speed * Time.deltaTime;
        scoreText.text = ((int)_score).ToString();
    }
    #endregion

    #region Actions
    private void NewLevel()
    {
        _speed += increasingSpeed;
    }

    private void EndGame()
    {
        _inGame = false;

        transform.DOLocalMoveX(transform.position.x, 0.1f);
        scoreAfterGameText.text = ((int)_score).ToString();
        CheckRecord();
    }
    #endregion

    private void CheckRecord()
    {
        int record = PlayerPrefs.GetInt("Record", 0);

        if (_score > record)
        {
            record = (int)_score;
            PlayerPrefs.SetInt("Record", record);
        }

        recordText.text = record.ToString();
    }
}