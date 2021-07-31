using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header("Blocks")]
    [SerializeField] private GameObject standartBlock;

    [Header("Parameters")]
    [SerializeField, Min(5), Tooltip("How many lines of blocks are there at the start")] private int startCountBlocksLines;
    [SerializeField, Min(5), Tooltip("How fast are the blocks falling")] private float fallingSpeed;
    [SerializeField, Min(0), Tooltip("How many lines to add")] private int howManyLinesToAdd;
    [SerializeField, Min(0), Tooltip("How fast the speed increases")] private float increasingSpeed;

    private int _level;

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
        Block block = Lean.Pool.LeanPool.Spawn(standartBlock, transform.position, Quaternion.identity).GetComponent<Block>();
        block.SetParameters(fallingSpeed + _level * increasingSpeed, transform.position);
    }
}
