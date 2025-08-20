using System.IO;
using UnityEngine;

public static class MazeSaveSystem
{
    private static string SaveDirectory => Path.Combine(Application.persistentDataPath, "SavedMazes");

    public static void SaveMaze(Maze maze, string fileName)
    {
        if (!Directory.Exists(SaveDirectory))
            Directory.CreateDirectory(SaveDirectory);

        SerializableMaze serializable = MazeConverter.ToSerializable(maze);
        string json = JsonUtility.ToJson(serializable, true);
        File.WriteAllText(Path.Combine(SaveDirectory, fileName + ".json"), json);
    }

    public static Maze LoadMaze(string fileName)
    {
        string path = Path.Combine(SaveDirectory, fileName + ".json");
        if (!File.Exists(path)) return null;

        string json = File.ReadAllText(path);
        SerializableMaze serializable = JsonUtility.FromJson<SerializableMaze>(json);
        return MazeConverter.FromSerializable(serializable);
    }

    public static string[] GetSavedMazeFiles()
    {
        if (!Directory.Exists(SaveDirectory))
            return new string[0];

        return Directory.GetFiles(SaveDirectory, "*.json");
    }
}
