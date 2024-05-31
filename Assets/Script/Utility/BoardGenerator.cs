using System.Collections;
using System.Collections.Generic;
using Game.Data;
using Game.Element;
using Game.Enum;
using Game.Interface;
using Game.Setting;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Utility
{
    public static class BoardGenerator
    {
        public static Board GenerateBoard(Vector2Int size,Tile tilePrefab,GameSetting gameSetting)
        {
            var board = new Tile[size.y, size.x];
            var container = new GameObject("Board").transform;
            container.position = new Vector3((float)size.x / 2,(float)size.y / 2,0);
            
            for (int i = 0; i < size.y; i++)
            {
                for (int j = 0; j < size.x; j++)
                {
                    var tile = Object.Instantiate(tilePrefab,container.transform);
                    var coordinate = new Vector2Int(j, i);
                    var adjacentCoordinate = GetAdjacentCoordinate(coordinate, size);
                    tile.Initialize(coordinate, adjacentCoordinate);
                    board[i, j] = tile;
                }
            }

            container.position = Vector3.zero;
            
            return new Board(board,container,gameSetting);
        }

        private static List<Vector2Int> GetAdjacentCoordinate(Vector2Int coordinate, Vector2Int size)
        {
            List<Vector2Int> result = new List<Vector2Int>();

            if (coordinate.x + 1 < size.x)
                result.Add(new Vector2Int(coordinate.x + 1, coordinate.y));
            if (coordinate.x - 1 >= 0)
                result.Add(new Vector2Int(coordinate.x - 1, coordinate.y));
            if (coordinate.y + 1 < size.y)
                result.Add(new Vector2Int(coordinate.x, coordinate.y + 1));
            if (coordinate.y - 1 >= 0)
                result.Add(new Vector2Int(coordinate.x, coordinate.y - 1));

            return result;
        }
    }
}