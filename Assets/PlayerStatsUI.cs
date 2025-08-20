using UnityEngine;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    public TMP_Text completedText;
    public TMP_Text bestTimeText;
    public TMP_Text averageTimeText;
    public TMP_Text lastTimeText;
    public TMP_Text totalPlayTimeText;

    public void Start()
    {
        int completed = PlayerPrefs.GetInt("CompletedMazes", 0);
        float best = PlayerPrefs.GetFloat("BestTime", 0f);
        float average = PlayerPrefs.GetFloat("AverageTime", 0f);
        float last = PlayerPrefs.GetFloat("LastTime", 0f);
        float totalPlayTime = PlayerPrefs.GetFloat("TotalPlayTime", 0f);

        completedText.text = $"Пройдено лабиринтов: {completed}";
        bestTimeText.text = best > 0 ? $"Лучшее время: {best:F2} сек" : "Лучшее время: —";
        averageTimeText.text = completed > 0 ? $"Среднее время: {average:F2} сек" : "Среднее время: —";
        lastTimeText.text = last > 0 ? $"Последнее время: {last:F2} сек" : "Последнее время: —";
        totalPlayTimeText.text = $"Всего игрового времени: {FormatTime(totalPlayTime)}";
    }

    private string FormatTime(float seconds)
    {
        int h = Mathf.FloorToInt(seconds / 3600);
        int m = Mathf.FloorToInt((seconds % 3600) / 60);
        int s = Mathf.FloorToInt(seconds % 60);
        return $"{h:D2}:{m:D2}:{s:D2}";
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteKey("CompletedMazes");
        PlayerPrefs.DeleteKey("BestTime");
        PlayerPrefs.DeleteKey("TotalTime");
        PlayerPrefs.DeleteKey("AverageTime");
        PlayerPrefs.DeleteKey("LastTime");
        PlayerPrefs.DeleteKey("TotalPlayTime");
        PlayerPrefs.Save();
        Start(); 
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu"); 
    }
}
