using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject playBtn;
    public GameObject ExitBtn;
    public GameObject GenerateBtn;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gridManager.BackToMenu();
            BackToMenu();
        }
    }
    public void GenerateDungerion()
    {
        gridManager.GenerateDungeon();
    }
    public void StartGame()
    {
        gridManager.SpawnPlayer();
        if (gridManager.playerSpawned)
        {
            playBtn.SetActive(false);
            ExitBtn.SetActive(false);
            GenerateBtn.SetActive(false);
        }
    }
    private void Start()
    {
        BackToMenu();
    }

    public void BackToMenu()
    {
        playBtn.SetActive(true);
        ExitBtn.SetActive(true);
        GenerateBtn.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
