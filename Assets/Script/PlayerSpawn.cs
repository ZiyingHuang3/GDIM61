using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Start()
    {
        if (SceneTransitionData.hasSpawnPosition)
        {
            transform.position = SceneTransitionData.spawnPosition;
            SceneTransitionData.hasSpawnPosition = false;
        }
    }
}