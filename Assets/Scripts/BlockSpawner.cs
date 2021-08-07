using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header("Blocks")]
    [SerializeField] private GameObject standartBlock;
    [SerializeField] private GameObject waterBlock;

    [Header("Bonuses")]
    [SerializeField, Range(0, 100)] private int bonusChance;
    [SerializeField] private List<GameObject> bonusesList;

    [Header("Parameters")]
    [SerializeField, Min(5), Tooltip("How many lines of blocks are there at the start")] private int startCountBlocksLines;
    [SerializeField, Min(0), Tooltip("How many lines to add")] private int howManyLinesToAdd;

    public float UpperLinePosition { get; private set; }

    private int _passIndex;
    private int _countBlocksLines;
    private static int s_blocksInLine = 7;
    private Vector2 _step;

    #region Awake Destroy
    private void Awake()
    {
        _step = standartBlock.transform.localScale;

        DotManager.LevelComplete += NewLevel;
    }

    private void OnDestroy()
    {
        DotManager.LevelComplete -= NewLevel;
    }

    private void OnEnable()
    {
        _countBlocksLines = startCountBlocksLines;
        CreateLevel();
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CreateLevel();
        }
    }

    #region Create
    private void CreateLevel()
    {
        Vector2 blockPosition = transform.position;
        int startPositionX = (int)blockPosition.x - s_blocksInLine / 2;
        blockPosition.x = startPositionX;

        int passIndexMin, passIndexMax;
        int bonusCount = 1;

        _passIndex = Random.Range(1, s_blocksInLine - 1);

        for (int i = 0; i < _countBlocksLines; i++)
        {
            #region Сalculating the passage
            if (i % 2 != 0)
            {
                passIndexMin = _passIndex;
                passIndexMax = Random.Range(1, s_blocksInLine - 1);
                _passIndex = passIndexMax;

                if (passIndexMin > passIndexMax)
                {
                    int temp = passIndexMin;
                    passIndexMin = passIndexMax;
                    passIndexMax = temp;
                }
            }
            else
            {
                passIndexMin = passIndexMax = _passIndex;
            }
            #endregion

            #region Create Maze's line
            for (int j = 0; j < s_blocksInLine; j++)
            {
                // create standart block
                if (j < passIndexMin || j > passIndexMax)
                    SpawnObject(standartBlock, blockPosition);

                // create random bonus
                else if (bonusCount > 0 && Random.value * 100 <= bonusChance)
                {
                    GameObject bonus = bonusesList[Random.Range(0, bonusesList.Count)];
                    SpawnObject(bonus, blockPosition);

                    bonusCount--;
                }

                blockPosition.x += _step.x;
            }
            #endregion

            blockPosition.y += _step.y;
            blockPosition.x = startPositionX;
        }

        blockPosition.x = 0;
        SpawnObject(waterBlock, blockPosition);

        UpperLinePosition = blockPosition.y;
    }

    private void SpawnObject(GameObject spawnObject, Vector2 blockPosition)
    {
        Lean.Pool.LeanPool.Spawn(spawnObject, blockPosition, Quaternion.identity);
    }
    #endregion

    private void NewLevel()
    {
        _countBlocksLines += howManyLinesToAdd;
        CreateLevel();
    }
}
