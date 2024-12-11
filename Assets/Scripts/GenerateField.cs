using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;

public class GenerateField : MonoBehaviour
{
    private int[,] intMap;
    private Cell[,] cellMap;
    [SerializeField] private int minesCount;
    [SerializeField] private Vector2Int SizeMap;
    [SerializeField] private GameObject Cell;
    [SerializeField] private float margin;
    [SerializeField] private bool isMine = true;
    [SerializeField] GameObject gmvr;
    [SerializeField] GameObject gmvr1;

    void Start()
    {
        gmvr.SetActive(false);
        gmvr1.SetActive(false);
        intMap = new int[SizeMap.x, SizeMap.y];
        cellMap = new Cell[SizeMap.x, SizeMap.y];
        genMines();
        genNumbers();
        genRealMap();
    }
    public void OpenMap()
    {
        if (isMine)
        {
            for (int x = 0; x < SizeMap.x; x++)
            {
                for (int y = 0; y < SizeMap.y; y++)
                {
                    if (intMap[x, y] < -900)
                        cellMap[x, y].OpenCell();
                    isMine = false;
                    gmvr.SetActive(true);
                    gmvr1.SetActive(true);

                }

            }
        }
    }
    void genMines()
    {
        int generated = 0;
        while (generated < minesCount)
        {

            Vector2Int pos = new Vector2Int(Random.Range(0, SizeMap.x), Random.Range(0, SizeMap.y));
            if (intMap[pos.x, pos.y] >= 0)
            {
                intMap[pos.x, pos.y] = -999;
                generated++;

            }
        }

    }
    void genNumbers()
    {
        for (int x = 0; x < SizeMap.x; x++)
        {
            for (int y = 0; y < SizeMap.y; y++)
            {
                if (intMap[x, y] < 0)
                {
                    Vector2Int cellMine = new Vector2Int(x, y);

                    setNeighbourCell(cellMine, new Vector2Int(1, 1));
                    setNeighbourCell(cellMine, new Vector2Int(0, 1));
                    setNeighbourCell(cellMine, new Vector2Int(-1, 1));
                    setNeighbourCell(cellMine, new Vector2Int(-1, 0));
                    setNeighbourCell(cellMine, new Vector2Int(-1, -1));
                    setNeighbourCell(cellMine, new Vector2Int(0, -1));
                    setNeighbourCell(cellMine, new Vector2Int(1, -1));
                    setNeighbourCell(cellMine, new Vector2Int(1, 0));


                }
            }
        }
    }
    void setNeighbourCell(Vector2Int origin, Vector2Int offset)
    {
        bool isSpaceX = origin.x + offset.x <= intMap.GetLength(0) - 1 && origin.x + offset.x >= 0;
        bool isSpacey = origin.y + offset.y <= intMap.GetLength(1) - 1 && origin.y + offset.y >= 0;
        if (isSpaceX && isSpacey)
            intMap[origin.x + offset.x, origin.y + offset.y]++;
    }
    public void openEmpty(Vector2Int genPos)
    {
        openNeighbourCell(genPos, new Vector2Int(1, 1));
        openNeighbourCell(genPos, new Vector2Int(0, 1));
        openNeighbourCell(genPos, new Vector2Int(-1, 1));
        openNeighbourCell(genPos, new Vector2Int(-1, 0));
        openNeighbourCell(genPos, new Vector2Int(-1, -1));
        openNeighbourCell(genPos, new Vector2Int(0, -1));
        openNeighbourCell(genPos, new Vector2Int(1, -1));
        openNeighbourCell(genPos, new Vector2Int(1, 0));

    }
    void openNeighbourCell(Vector2Int origin, Vector2Int offset)
    {
        bool isSpaceX = origin.x + offset.x <= intMap.GetLength(0) - 1 && origin.x + offset.x >= 0;
        bool isSpacey = origin.y + offset.y <= intMap.GetLength(1) - 1 && origin.y + offset.y >= 0;
        if (isSpaceX && isSpacey)
            cellMap[origin.x + offset.x, origin.y + offset.y].OpenCell();
    }
    void genRealMap()
    {
        for (int x = 0; x < SizeMap.x; x++)
        {
            for (int y = 0; y < SizeMap.y; y++)
            {
                {
                    Cell c = Instantiate(Cell, new Vector3(x, 0, y) * margin, Quaternion.identity, transform).GetComponent<Cell>();
                    c.SetNumber(intMap[x, y]);
                    c.Gen(this, new Vector2Int(x, y));
                    cellMap[x, y] = c;
                }
            }
        }


    }
}
