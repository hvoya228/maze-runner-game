using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GreenScoresSpawner _greenScoresSpawner;
    [SerializeField] private PinkScoreSpawner _pinkScoreSpawner;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private KeySpawner _keySpawner;
    [SerializeField] private LockSpawner _lockSpawner;
    [SerializeField] private MazeAppearanceAnimation _mazeAppearanceAnimation;

    public int MazeWidth { get; private set; } 
    public int MazeHeight { get; private set; } 
    public int MazeCyclesCount { get; private set; }
    public int MazeGreenScoresCount { get; private set; }
    public string LevelDescription { get; private set; }

    public void SpawnLevel()
    {
        int levelType = LevelRarityController.GetLevelType();

        switch (levelType)
        {
            case 1: SpawnLuckyLevel(); break;
            case 2: SpawnDefaultLevel(); break;
            case 3: SpawnAbandonedLevel(); break;
            case 4: SpawnEnemyLevel(); break;
            case 5: SpawnLockedLevel(); break;

            default:
                SpawnLevel();
                Debug.Log("- level type error, trying to regenerate...");  break;
        }

        MazeWidth = _mazeSpawner.MazeWidth - 1;
        MazeHeight = _mazeSpawner.MazeHeight - 1;
        MazeCyclesCount = _mazeSpawner.SpawnedCyclesCount;
        MazeGreenScoresCount = _greenScoresSpawner.GreenScoresCount;
        LevelDescription = LevelDescriptionController.GetDescription(levelType);

        if (FindObjectOfType<Player>() == null)
        _playerSpawner.Spawn(_mazeSpawner.FirstCellCoordinates);
    }

    private void SpawnDefaultLevel()
    {
        _mazeSpawner.Spawn(Random.Range(3, 7));
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
    }

    private void SpawnAbandonedLevel()
    {
        _mazeSpawner.Spawn(Random.Range(15, 20), Random.Range(30, 36), Random.Range(15, 19));
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.GreenScoresCount = 0;
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
    }

    private void SpawnEnemyLevel()
    {
        _mazeSpawner.Spawn(Random.Range(3, 7));
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _enemySpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
    }

    private void SpawnLockedLevel()
    {
        _mazeSpawner.Spawn(Random.Range(7, 10));
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _lockSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _keySpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
    }

    private void SpawnLuckyLevel()
    {
        _mazeSpawner.Spawn(0, Random.Range(5, 9), Random.Range(5, 9));
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.LuckySpawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
    }

    public void SpawnTestLevel()
    {
        _mazeSpawner.Spawn(0, 30, 18);
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
    }
}
