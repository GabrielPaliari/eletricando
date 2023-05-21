using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RotationUtil
{
    public static Quaternion GetRotationAngle(RotationDir dir)
    {
        switch (dir)
        {
            default:
            case RotationDir.Left:
                return Quaternion.Euler(0, 0, 0);
            case RotationDir.Down:
                return Quaternion.Euler(0, 90, 0);
            case RotationDir.Right:
                return Quaternion.Euler(0, 180, 0);
            case RotationDir.Up:
                return Quaternion.Euler(0, 270, 0);
        }
    }
    public static Vector2Int GetRotationOffset(RotationDir dir, Vector2Int objectSize)
    {
        switch (dir)
        {
            default:
            case RotationDir.Left:
                return new Vector2Int(0, 0);
            case RotationDir.Down:
                return new Vector2Int(0, objectSize.x);
            case RotationDir.Right:
                return new Vector2Int(objectSize.x, objectSize.y);
            case RotationDir.Up:
                return new Vector2Int(objectSize.y, 0);
        }
    }

    public static RotationDir GetNextRotation(RotationDir rotationDir)
    {
        switch (rotationDir)
        {
            case RotationDir.Left:
                return RotationDir.Down;
            case RotationDir.Down:
                return RotationDir.Right;
            case RotationDir.Right:
                return RotationDir.Up;
            case RotationDir.Up:
                return RotationDir.Left;
        }
        return RotationDir.Left;
    }

    public static void GetObjectWidthAndHeight(Vector2Int objectSize, RotationDir dir, out int width, out int height)
    {
        switch (dir)
        {
            case RotationDir.Left:
            case RotationDir.Right:
                width = objectSize.x;
                height = objectSize.y;
                break;
            case RotationDir.Up:
            case RotationDir.Down:
            default:
                width = objectSize.y;
                height = objectSize.x;
                break;
        }
    }
}
public enum RotationDir
{
    Up,
    Left,
    Down,
    Right,
}
