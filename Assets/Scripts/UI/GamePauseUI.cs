using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button resumeButton;
    [SerializeField] private UnityEngine.UI.Button mainMenuButton;
    [SerializeField] private UnityEngine.UI.Button controlsButton;

    private void Awake() {
        resumeButton.onClick.AddListener(resumeClick);
        mainMenuButton.onClick.AddListener(mainMenuClick);
        controlsButton.onClick.AddListener(controlsClick);
    }

    private void resumeClick() {
        GameManager.Instance.PauseGame();
    }

    private void mainMenuClick() {
        Loader.Load(Loader.Scene.MainMenuScene);
    }

    private void controlsClick() {
        ControlsUI.Instance.Show();
    }

    private void Start() {
        GameManager.Instance.onGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.onGameUnpaused += GameManager_OnGameUnpaused;

        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e) {
        Show();
    }
    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
