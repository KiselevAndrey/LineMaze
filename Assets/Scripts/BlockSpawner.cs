using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header("Blocks")]
    [SerializeField] private GameObject standartBlock;
    [SerializeField] private GameObject waterBlock;
    [SerializeField] private GameObject jumpBlock;

    [Header("Parameters")]
    [SerializeField, Min(5), Tooltip("How many lines of blocks are there at the start")] private int startCountBlocksLines;
    [SerializeField, Min(0), Tooltip("How many lines to add")] private int howManyLinesToAdd;

    private int _level;
    private int _passIndex;
    private static int s_blocksInLine = 7;

    #region Awake Destroy
    private void Awake()
    {
        DotManager.LevelComplete += NewLevel;
    }

    private void OnDestroy()
    {
        DotManager.LevelComplete -= NewLevel;
    }

    private void OnEnable()
    {
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
        _passIndex = Random.Range(1, s_blocksInLine - 1);

        for (int i = 0; i < startCountBlocksLines; i++)
        {
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
            
            for (int j = 0; j < s_blocksInLine; j++)
            {
                if (j < passIndexMin || j > passIndexMax)
                    CreateBlock(standartBlock, blockPosition);

                if (i == startCountBlocksLines - 1 && j == _passIndex)
                    CreateBlock(jumpBlock, blockPosition);

                blockPosition.x++;
            }

            blockPosition.y++;
            blockPosition.x = startPositionX;
        }

        blockPosition.x = 0;
        CreateBlock(waterBlock, blockPosition);
    }

    private void CreateBlock(GameObject spawnBlock, Vector2 blockPosition)
    {
        Lean.Pool.LeanPool.Spawn(spawnBlock, blockPosition, Quaternion.identity);
    }
    #endregion

    private void NewLevel()
    {
        startCountBlocksLines += howManyLinesToAdd;
        CreateLevel();
    }
}
