using Core;
using UnityEngine;

namespace Helpers
{
    public static class VectorHelper
    {
        public static Vector2 GetVectorFromDirection(Direction direction) => direction switch
        {
            Direction.Up => Vector2.up,
            Direction.Right => Vector2.right,
            Direction.Down => Vector2.down,
            Direction.Left => Vector2.left,
            _ => Vector2.zero
        };
    }
}