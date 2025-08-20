using System.Collections.Generic;

public static class MazeConverter
{
    public static SerializableMaze ToSerializable(Maze maze)
    {
        var serializable = new SerializableMaze
        {
            width = maze.cells.GetLength(0),
            height = maze.cells.GetLength(1),
            finishPosition = maze.finishPosition,
            cells = new List<SerializableCell>()
        };

        for (int x = 0; x < serializable.width; x++)
        {
            for (int y = 0; y < serializable.height; y++)
            {
                var cell = maze.cells[x, y];
                serializable.cells.Add(new SerializableCell
                {
                    x = cell.X,
                    y = cell.Y,
                    wallLeft = cell.WallLeft,
                    wallBottom = cell.WallBottom,
                    distanceFromStart = cell.DistanceFromStart
                });
            }
        }

        return serializable;
    }

    public static Maze FromSerializable(SerializableMaze serializable)
    {
        var maze = new Maze
        {
            finishPosition = serializable.finishPosition,
            cells = new MazeGeneratorCell[serializable.width, serializable.height]
        };

        foreach (var sCell in serializable.cells)
        {
            maze.cells[sCell.x, sCell.y] = new MazeGeneratorCell
            {
                X = sCell.x,
                Y = sCell.y,
                WallLeft = sCell.wallLeft,
                WallBottom = sCell.wallBottom,
                DistanceFromStart = sCell.distanceFromStart
            };
        }

        return maze;
    }
}
