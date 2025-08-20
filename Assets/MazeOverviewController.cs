using UnityEngine;

public class MazeOverviewController : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public MazeSpawner mazeSpawner;

    public void ToggleMazeOverview()
    {
        if (mazeSpawner.maze == null) return;

        Vector3 mazeCenter = new Vector3(
            mazeSpawner.maze.cells.GetLength(0) * mazeSpawner.CellSize.x * 0.5f,
            mazeSpawner.maze.cells.GetLength(1) * mazeSpawner.CellSize.y * 0.5f,
            0
        );

        float mazeWidth = mazeSpawner.maze.cells.GetLength(0) * mazeSpawner.CellSize.x;
        float mazeHeight = mazeSpawner.maze.cells.GetLength(1) * mazeSpawner.CellSize.y;

        cameraFollow.ToggleOverview(mazeCenter, mazeWidth, mazeHeight);
    }
}