using UnityEngine;
using TMPro;
using System; 

public class MazeManager : MonoBehaviour
{
    public MazeSpawner mazeSpawner;
    public TMP_InputField saveInputField; 

    public void SaveMaze()
    {
        if (string.IsNullOrEmpty(saveInputField.text))
        {
            Debug.LogError("Введите имя лабиринта!");
            return;
        }

        MazeSaveSystem.SaveMaze(mazeSpawner.maze, saveInputField.text);
        Debug.Log($"Лабиринт сохранён как: {saveInputField.text}");

        string fileName = saveInputField.text;

        if (string.IsNullOrEmpty(fileName))
        {
            fileName = $"Maze_{DateTime.Now:yyyyMMdd_HHmmss}"; 
        }

        MazeSaveSystem.SaveMaze(mazeSpawner.maze, fileName);
    }

    public void LoadMaze(string fileName) 
    {
        Maze loaded = MazeSaveSystem.LoadMaze(fileName);
        if (loaded != null)
        {
            mazeSpawner.SpawnMaze(loaded);
        }
    }
}