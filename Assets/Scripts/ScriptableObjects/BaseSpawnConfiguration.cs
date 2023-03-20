using UnityEngine;

public class BaseSpawnConfiguration : ScriptableObject
{
    [Tooltip("Amount of enemies to spawn. Set to -1 to make endless spawn")]
    public int SpawnCount = -1;
    
    public float SpawnDelay = 3f;
    public float SpawnRadius = 10f;
}
