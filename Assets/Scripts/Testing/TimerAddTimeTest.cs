using UnityEngine;

public class TimerAddTimeTest : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.Instance.UpdateMaskSuccess();
            Debug.Log($"[TimerTest] Called UpdateMaskSuccess, adding bonus time. Score: {GameManager.Instance.GameState}");
        }
    }
}