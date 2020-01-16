using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private int boardSize;
    [SerializeField] private GameObject tilePrefab;

    // 임시로 넣어둔 요소들
    [SerializeField] private GameObject pawnPrefab;
    [SerializeField] private GameObject knightPrefab;

    private Tile[,] tiles;

    const float tileSize = 5f;

    private void Awake()
    {
        CreateBoard();
        CreateUnits();
    }

    private void CreateBoard()
    {
        tiles = new Tile[boardSize, boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Vector3 spawnPos = new Vector3(i, 0, j) * tileSize;
                GameObject tileObject = Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);
                Tile tile = tileObject.GetComponent<Tile>();
                tile.Initialize(i, j, this);
                tiles[i, j] = tile;
            }
        }
    }

    private void CreateUnit(int x, int y, GameObject unit)
    {
        Vector3 spawnPos = new Vector3(x, 0, y) * tileSize + new Vector3(-tileSize, 1, -tileSize) / 2;
        Unit nowUnit = Instantiate(unit, spawnPos, Quaternion.identity).GetComponent<Unit>();
        tiles[x, y].AssignUnit(nowUnit);
    }

    private void CreateUnits()
    {
        CreateUnit(0, 0, pawnPrefab);
        CreateUnit(0, 1, pawnPrefab);
        CreateUnit(1, 0, pawnPrefab);
        CreateUnit(1, 1, pawnPrefab);
        CreateUnit(7, 0, pawnPrefab);
        CreateUnit(7, 1, pawnPrefab);
        CreateUnit(6, 0, pawnPrefab);
        CreateUnit(6, 1, pawnPrefab);
    }
}
