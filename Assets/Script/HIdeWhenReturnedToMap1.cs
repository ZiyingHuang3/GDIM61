using UnityEngine;

public class HideWhenReturnedToMap1 : MonoBehaviour
{
    public GameObject targetObject;

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
        if (targetObject != null)
            targetObject.SetActive(!GameProgress.returnedToMap1);
    }
}