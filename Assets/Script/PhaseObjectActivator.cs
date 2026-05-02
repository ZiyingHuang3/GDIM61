using UnityEngine;

public class PhaseObjectActivator : MonoBehaviour
{
    public bool showWhenReturnedToMap1 = true;

    private void Start()
    {
        UpdateState();
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (showWhenReturnedToMap1)
        {
            gameObject.SetActive(GameProgress.returnedToMap1);
        }
    }
}