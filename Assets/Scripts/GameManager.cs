using System;
using CameraScripts;
using Enemy;
using Obstacles;
using Player;
using UnityEngine;

public class GameManager : MonoBehaviour, IDisposable
{
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private ObstacleManager _obstacleManager;
    [SerializeField] private EndGameManager _endGameManager;
    [SerializeField] private UIManager _uiManager;

    private void Awake()
    {
        _enemyManager.OnEnemyReachPlayerEvent += EndGame;
        _obstacleManager.OnPlayerCollisionEvent += EndGame;
        _uiManager.OnRestartButtonPressedEvent += RestartGame;

        SpawnPlayerAndSetUpCamera();
    }

    private void SpawnPlayerAndSetUpCamera()
    {
        _playerManager.SpawnPlayer();
        SetUpCamera(_playerManager.Player);
    }

    private void SetUpCamera(PlayerController player)
    {
        _cameraManager.SetPlayer(player);
    }

    private void RestartGame()
    { 
        SpawnPlayerAndSetUpCamera();
        
        _enemyManager.StartEnemySpawn();
        _obstacleManager.StartSpawn();
    }

    private void EndGame()
    {
        _enemyManager.StopEnemySpawn();
        _enemyManager.KillAllEnemies();
        
        _obstacleManager.StopSpawn();
        _obstacleManager.DestroyAllObstacles();
        
        _endGameManager.EndGame(_playerManager.Player.transform);
        
        _playerManager.KillPlayer();
        
        _uiManager.ShowEndGameScreen();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    public void Dispose()
    {
        _enemyManager.OnEnemyReachPlayerEvent -= EndGame;
        _obstacleManager.OnPlayerCollisionEvent -= EndGame;
        _uiManager.OnRestartButtonPressedEvent -= RestartGame;

        _enemyManager?.Dispose();
        _obstacleManager?.Dispose();
    }
}
