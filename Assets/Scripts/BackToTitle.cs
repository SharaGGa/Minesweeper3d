using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    [SerializeField] private Button exitToTile;

    void Start()
    {
        exitToTile.onClick.AddListener(ToTitle);
    }
    void ToTitle()
    {
        SceneManager.LoadScene(0);
    }

}
