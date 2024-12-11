using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.TestTools;

public class Cell : MonoBehaviour
{
    [SerializeField] private Transform numberSpawn;
    [SerializeField] private GameObject[] numbers;
    [SerializeField] private GameObject Mine;
    [SerializeField] private GameObject flag;
    [SerializeField] private bool isFlagged;
    [SerializeField] public int Number;
    [SerializeField] private GameObject closedCell;
    [SerializeField] private bool isOpen;
    [SerializeField] private GenerateField genField;
    [SerializeField] private Vector2Int genPos;

    public void SetNumber(int num) => Number = num;
    public void Gen(GenerateField genField, Vector2Int genPos)
    {
        this.genField = genField;
        this.genPos = genPos;

    }

    void OnMouseOver()
    {
        if (closedCell.activeSelf && !isOpen)
            if (Input.GetMouseButtonDown(0) && !isFlagged)
                OpenCell();
            else if (Input.GetMouseButtonDown(1))
                FlagCell();
    }


    public void OpenCell()
    {
        if (isOpen)
            return;
        closedCell.SetActive(false);
        isOpen = true;
        if (Number == 0)
        {
            genField.openEmpty(genPos);
        }
        if (Number > 0 && Number < numbers.Length + 1)
            Instantiate(numbers[Number - 1], numberSpawn.position, Quaternion.identity, numberSpawn);
        if (Number < 0)
        {
            Instantiate(Mine, numberSpawn.position, Quaternion.identity, numberSpawn);
            genField.OpenMap();
        }

    }
    void FlagCell()
    {
        isFlagged = !isFlagged;
        flag.SetActive(isFlagged);
    }
}
