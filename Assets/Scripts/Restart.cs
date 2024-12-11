using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    [SerializeField] private Button restart;

    void Start()
    {
        restart.onClick.AddListener(RestartGame);
    }
    void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    // Start is called before the first frame update

}
