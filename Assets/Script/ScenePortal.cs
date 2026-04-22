using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public string targetSceneName;
    public Vector2 targetSpawnPosition;

    [Header("Unlock Condition")]
    public bool requireIntroDialogueFinished = true;

    [Header("Optional Visual")]
    public GameObject portalVisual;

    private bool isTransitioning = false;

    private void Start()
    {
        UpdatePortalState();
    }

    private void Update()
    {
        UpdatePortalState();
    }

    private bool IsUnlocked()
    {
        if (requireIntroDialogueFinished && !GameProgress.introDialogueFinished)
            return false;

        return true;
    }

    private void UpdatePortalState()
    {
        bool unlocked = IsUnlocked();

        if (portalVisual != null)
            portalVisual.SetActive(unlocked);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTransitioning) return;
        if (!IsUnlocked()) return;

        if (other.CompareTag("Player"))
        {
            isTransitioning = true;

            SceneTransitionData.spawnPosition = targetSpawnPosition;
            SceneTransitionData.hasSpawnPosition = true;

            SceneManager.LoadScene(targetSceneName);
        }
    }
}
