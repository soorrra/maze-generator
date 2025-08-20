using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float sessionStartTime;

    private void Start()
    {
        sessionStartTime = Time.time;
    }

    private void OnApplicationQuit()
    {
        SaveSessionTime();
    }

    private void OnDisable()
    {
        SaveSessionTime(); 
    }

    void SaveSessionTime()
    {
        float sessionDuration = Time.time - sessionStartTime;
        float totalPlayTime = PlayerPrefs.GetFloat("TotalPlayTime", 0f);
        PlayerPrefs.SetFloat("TotalPlayTime", totalPlayTime + sessionDuration);
        PlayerPrefs.Save();
    }
}
