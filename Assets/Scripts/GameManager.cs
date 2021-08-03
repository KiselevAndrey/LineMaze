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
        DotManager.IHit += EnableAfterGame;
    }

    private void OnDestroy()
    {
        DotManager.IHit -= EnableAfterGame;
    }

    private void OnEnable()
    {
        //EnableMenu();
        EnableGame();
    }
    #endregion

    #region Activate
    private void EnableListContent(List<GameObject> list, bool value)
    {
        for (int i = 0; i < list.Count; i++)
            list[i].SetActive(value);
    }

    private void EnableMenu()
    {
        EnableListContent(activateMenu, true);
        EnableListContent(activateGame, false);
        EnableListContent(activateAfterGame, false);
        Time.timeScale = 0f;
    }

    private void EnableGame()
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
        Time.timeScale = 0f;
    }
    #endregion
}
