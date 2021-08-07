using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> activateMenu;
    [SerializeField] private List<GameObject> activateGame;
    [SerializeField] private List<GameObject> activateAfterGame;

    #region Awake Destroy OnEnable
    private void Awake()
    {
        DotManager.IHit += EndGame;
    }

    private void OnDestroy()
    {
        DotManager.IHit -= EndGame;
    }

    private void OnEnable()
    {
        EnableMenu();
        //EnableGame();
    }
    #endregion

    #region Activate
    private void EnableListContent(List<GameObject> list, bool value)
    {
        for (int i = 0; i < list.Count; i++)
            list[i].SetActive(value);
    }

    public void EnableMenu()
    {
        EnableListContent(activateMenu, true);
        EnableListContent(activateGame, false);
        EnableListContent(activateAfterGame, false);
    }

    public void EnableGame()
    {
        EnableListContent(activateMenu, false);
        EnableListContent(activateGame, true);
        EnableListContent(activateAfterGame, false);
        Time.timeScale = 1f;
    }

    private void EnableAfterGame()
    {
        EnableListContent(activateMenu, false);
        EnableListContent(activateGame, false);
        EnableListContent(activateAfterGame, true);

        Lean.Pool.LeanPool.DespawnAll();
    }
    #endregion

    private void EndGame() 
    {
        Invoke(nameof(EnableAfterGame), 2f);
    }
}
