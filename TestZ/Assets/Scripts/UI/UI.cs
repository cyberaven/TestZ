using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private MainMenuPanel MainMenuPanel;


    private void OnEnable()
    {
        StartButton.StartButtonClickEvent += GameStart;
        Player.FinishTouchEvent += WinGame;
        Player.EnemyTouchEvent += LoseGame;
        Player.MinSizeEvent += LoseGame;
    }
    private void OnDisable()
    {
        StartButton.StartButtonClickEvent -= GameStart;
        Player.EnemyTouchEvent -= LoseGame;
        Player.MinSizeEvent -= LoseGame;
    }

    private void GameStart()
    {
        MainMenuPanel.Hide();
    }
    private void WinGame()
    {
        MainMenuPanel.Show();
    }
    private void LoseGame()
    {
        MainMenuPanel.Show();
    }

}
