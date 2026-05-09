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
    public bool requireAllSuspectDialogueComplete = false;
    public bool markReturnedToDialogueSceneAgain = false;

public bool requireSecondInvestigationComplete = false;   
 public bool requirePart2EvidenceComplete = false;

    [Header("Mark Progress")]
    public bool markReturnedToMap1 = false;

    [Header("Hide Condition")]
    public bool hideAfterReturnedToMap1 = false;

    [Header("Visual / Collider")]
    public GameObject portalVisual;
    public Collider2D portalCollider;

    private bool isTransitioning = false;
    private bool lastUnlocked;

    private void Start()
    {
        lastUnlocked = IsUnlocked();
        UpdatePortalState();
    }

    private void Update()
    {
        bool unlocked = IsUnlocked();

        if (unlocked != lastUnlocked)
        {
            lastUnlocked = unlocked;
            UpdatePortalState();
        }
    }

    private bool IsUnlocked()
    {
        if (hideAfterReturnedToMap1 && GameProgress.returnedToMap1)
            return false;

        if (requireIntroDialogueFinished && !GameProgress.introDialogueFinished)
            return false;

        if (requirePart1Complete && !GameProgress.CanGoToNextMap())
            return false;

        if (requireReturnedToMap1 && !GameProgress.returnedToMap1)
            return false;

        if (requireAllSuspectDialogueComplete && !GameProgress.CanReturnToMap1())
            return false;

        if (requirePart2EvidenceComplete && !GameProgress.part2EvidenceComplete)
            return false;
        if (requireSecondInvestigationComplete && !GameProgress.CanGoToDialogueSceneAgain())
            return false;

        return true;
    }

    private void UpdatePortalState()
    {
        bool unlocked = IsUnlocked();

        if (portalVisual != null)
            portalVisual.SetActive(unlocked);

        if (portalCollider != null)
            portalCollider.enabled = unlocked;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTransitioning) return;
        if (!IsUnlocked()) return;

        if (other.CompareTag("Player"))
        {
            isTransitioning = true;

            if (markReturnedToMap1)
            {
                GameProgress.returnedToMap1 = true;
            }

            if (markReturnedToDialogueSceneAgain)
        {
            GameProgress.returnedToDialogueSceneAgain = true;
        }

            SceneTransitionData.spawnPosition = targetSpawnPosition;
            SceneTransitionData.hasSpawnPosition = true;

            SceneManager.LoadScene(targetSceneName);
        }
    }
}