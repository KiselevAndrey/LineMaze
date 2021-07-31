using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header("Blocks")]
    [SerializeField] private GameObject standartBlock;

    [Header("Parameters")]
    [SerializeField, Min(5), Tooltip("How many lines of blocks are there at the start")] private int startCountBlocksLines;
    [SerializeField, Min(0), Tooltip("How fast are the blocks falling")] private float fallingSpeed;
    [SerializeField, Min(0), Tooltip("How many lines to add")] private int howManyLinesToAdd;
    [SerializeField, Min(0), Tooltip("How fast the speed increases")] private float increasingSpeed;

    private int _level;
    private int _passIndex;
    private static int s_blocksInLine = 7;

    private void Awake()
    {
        CreateLevel();  
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
        }
    }

    private void CreateLevel()
    {
        Vector2 blockPosition = transform.position;
        int startPositionX = (int)blockPosition.x - s_blocksInLine / 2;
        blockPosition.x = startPositionX;

        for (int i = 0; i < startCountBlocksLines; i++)
        {
            if (i % 2 == 0)
                _passIndex = Random.Range(1, s_blocksInLine - 1);

            for (int j = 0; j < s_blocksInLine; j++)
            {
                if (j != _passIndex)
                    CreateBlock(blockPosition);
                blockPosition.x++;
            }

            blockPosition.y++;
            blockPosition.x = startPositionX;
        }
    }

    private void CreateBlock(Vector2 blockPosition)
    {
        Block block = Lean.Pool.LeanPool.Spawn(standartBlock).GetComponent<Block>();
        block.SetParameters(fallingSpeed + _level * increasingSpeed, blockPosition);

    }
}
