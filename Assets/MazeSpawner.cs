using Assets;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MazeSpawner : MonoBehaviour
{
    public Cell CellPrefab;
    public Vector3 CellSize = new Vector3(1, 1, 0);
    public HintRenderer HintRenderer;
    public TMP_InputField widthInputField;
    public TMP_InputField heightInputField;
    public TMP_Dropdown difficultyDropdown;

    public GameObject exitPrefab;
    private GameObject currentExit;
    public float startTime;

    public Maze maze;
    private MazeGenerator generator = new MazeGenerator();

    private void Start()
    {
        widthInputField.text = "10";
        heightInputField.text = "10";

        widthInputField.onValidateInput += ValidateNumberInput;
        heightInputField.onValidateInput += ValidateNumberInput;
        widthInputField.onValueChanged.AddListener(ValidateMaxValue);
        heightInputField.onValueChanged.AddListener(ValidateMaxValue);

        if (difficultyDropdown != null)
        {
            difficultyDropdown.ClearOptions();
            difficultyDropdown.AddOptions(new List<string> { "Легкий", "Средний", "Сложный" });
            difficultyDropdown.value = (int)DifficultyLevel.Medium;
        }

        GenerateNewMaze();
    }

    private char ValidateNumberInput(string text, int charIndex, char addedChar)
    {
        if (!char.IsDigit(addedChar))
        {
            return '\0'; 
        }
        return addedChar;
    }

    private void ValidateMaxValue(string value)
    {
        if (string.IsNullOrEmpty(value)) return;

        if (int.TryParse(value, out int number))
        {
            if (number > 1000)
            {
                if (this.widthInputField.isFocused)
                    widthInputField.text = "1000";
                else if (this.heightInputField.isFocused)
                    heightInputField.text = "1000";
            }
            else if (number < 1)
            {
                if (this.widthInputField.isFocused)
                    widthInputField.text = "1";
                else if (this.heightInputField.isFocused)
                    heightInputField.text = "1";
            }
        }
    }

    public void GenerateNewMaze()
    {
        int width = 10;
        int height = 10;

        if (int.TryParse(widthInputField.text, out int w))
            width = Mathf.Clamp(w, 1, 1000);
        if (int.TryParse(heightInputField.text, out int h))
            height = Mathf.Clamp(h, 1, 1000);

        widthInputField.text = width.ToString();
        heightInputField.text = height.ToString();

        generator.SetSize(width, height);
        DifficultyLevel difficulty = DifficultyLevel.Medium;

        if (difficultyDropdown != null)
        {
            difficulty = (DifficultyLevel)difficultyDropdown.value;
        }

        generator.SetDifficulty(difficulty);
        maze = generator.GenerateMaze();

        SpawnMaze(maze);
    }

    public void SpawnMaze(Maze mazeToSpawn)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (currentExit != null)
        {
            Destroy(currentExit);
            currentExit = null;
        }

        maze = mazeToSpawn;

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                MazeGeneratorCell cellData = maze.cells[x, y];
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, 0), Quaternion.identity, transform);
                c.WallLeft.SetActive(cellData.WallLeft);
                c.WallBottom.SetActive(cellData.WallBottom);
            }
        }

        Vector2Int exitPos = maze.finishPosition;
        Vector3 worldPos = new Vector3(
            (exitPos.x + 0.5f) * CellSize.x,
            (exitPos.y + 0.5f) * CellSize.y,
            0
        );

        currentExit = Instantiate(exitPrefab, worldPos, Quaternion.identity);
        startTime = Time.time;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(0.48f, 0.4812f, 0f);
        }

        GameObject panel = GameObject.Find("GameMenu_Panel");
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}