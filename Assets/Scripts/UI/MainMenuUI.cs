using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button playButton;
    [SerializeField] private UnityEngine.UI.Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(playClick);
        quitButton.onClick.AddListener(quitClick);
    }

    private void playClick() {
        // SceneManager.LoadScene(1);
        Loader.Load(Loader.Scene.GameScene);
    }

    private void quitClick() {
        Application.Quit();
    }
}