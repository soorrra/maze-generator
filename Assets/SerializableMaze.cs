using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableMaze
{
    public List<SerializableCell> cells;
    public int width;
    public int height;
    public Vector2Int finishPosition;
}

[Serializable]
public class SerializableCell
{
    public int x, y;
    public bool wallLeft, wallBottom;
    public int distanceFromStart;
}
