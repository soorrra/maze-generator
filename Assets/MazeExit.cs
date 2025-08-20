using UnityEngine;
using TMPro;

public class MazeExit : MonoBehaviour
{
    private GameObject winPanel;
    private bool hasFinished = false;

    private void Start()
    {

        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var obj in allObjects)
        {
            if (obj.name == "WinPanel")
            {
                winPanel = obj;
                break;
            }
        }

        if (winPanel != null)
        {
            winPanel.SetActive(false); 
        }
        else
        {
            Debug.LogWarning("Панель победы (WinPanel) не найдена в сцене!");
        }



    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasFinished) return;

        if (other.CompareTag("Player"))
        {
            hasFinished = true;

            float finishTime = Time.time - FindObjectOfType<MazeSpawner>().startTime;

            int completed = PlayerPrefs.GetInt("CompletedMazes", 0) + 1;
            PlayerPrefs.SetInt("CompletedMazes", completed);

            float total = PlayerPrefs.GetFloat("TotalTime", 0f) + finishTime;
            PlayerPrefs.SetFloat("TotalTime", total);
            PlayerPrefs.SetFloat("AverageTime", total / completed);

            float best = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
            if (finishTime < best)
            {
                PlayerPrefs.SetFloat("BestTime", finishTime);
            }

            PlayerPrefs.SetFloat("LastTime", finishTime);
            PlayerPrefs.Save();

            if (winPanel != null)
            {
                winPanel.SetActive(true);
                TMP_Text text = winPanel.GetComponentInChildren<TMP_Text>();
                if (text != null)
                {
                    text.text = $"Вы прошли лабиринт за {finishTime:F2} секунд!";
                }
            }
        }
    }



}
