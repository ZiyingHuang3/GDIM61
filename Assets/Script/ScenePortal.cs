using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public string targetSceneName;
    public Vector2 targetSpawnPosition;

    [Header("Unlock Condition")]
    public bool requireIntroDialogueFinished = false;
    public bool requirePart1Complete = false;
    public bool requireReturnedToMap1 = false;

    [Header("Visual")]
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

        if (requirePart1Complete && !GameProgress.CanGoToNextMap())
            return false;

        if (requireReturnedToMap1 && !GameProgress.returnedToMap1)
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