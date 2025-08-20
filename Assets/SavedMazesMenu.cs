using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavedMazesMenu : MonoBehaviour
{
    public Transform mazeListContent;        
    public GameObject mazeButtonPrefab;     
    public MazeSpawner mazeSpawner;          

    private void Start()
    {
        LoadMazeList();
    }

    public void LoadMazeList()
    {
        if (mazeListContent == null || mazeButtonPrefab == null)
        {
            Debug.LogError("Не назначены необходимые компоненты!");
            return;
        }

        foreach (Transform child in mazeListContent)
        {
            Destroy(child.gameObject);
        }

        string[] files = MazeSaveSystem.GetSavedMazeFiles();
        if (files == null || files.Length == 0)
        {
            Debug.Log("Нет сохранённых лабиринтов.");
            return;
        }

        foreach (string filePath in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            GameObject button = Instantiate(mazeButtonPrefab, mazeListContent);


            TMP_Text textComponent = button.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                textComponent.text = fileName;
            }
            else
            {
                Debug.LogError("Не найден компонент TMP_Text в префабе кнопки!");
            }

            button.GetComponent<Button>().onClick.AddListener(() => OnMazeSelected(fileName));
        }
    }

    private void OnMazeSelected(string fileName)
    {
        ClearExistingMaze();

        Maze loaded = MazeSaveSystem.LoadMaze(fileName);
        if (loaded != null)
        {
            mazeSpawner.SpawnMaze(loaded);

            ResetPlayerPosition();
        }
        
        GameObject panel = GameObject.Find("SavedMazesPanel"); 
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    private void ClearExistingMaze()
    {
        foreach (Transform child in mazeSpawner.transform)
        {
            Destroy(child.gameObject);
        }

        var oldCells = FindObjectsOfType<Cell>();
        foreach (var cell in oldCells)
        {
            Destroy(cell.gameObject);
        }
    }

    private void ResetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(0.48f, 0.4812f, 0f);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
