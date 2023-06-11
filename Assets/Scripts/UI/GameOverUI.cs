using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeDeliveredText;
    [SerializeField] private UnityEngine.UI.Button playAgainButton;

    private void Start() {
    GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
    playAgainButton.onClick.AddListener(PlayAgainClick);
    Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
{
  // Hides the game over if the state changes from the game over state
    if (GameManager.Instance.IsGameOver())
    {
        Show();
        recipeDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipeAmount().ToString();
    }
    else
    {
        Hide();
    }
}

  private void PlayAgainClick()
{
    Loader.Load(Loader.Scene.GameScene);
}

  private void Show() {
    gameObject.SetActive(true);
  }

  private void Hide() {
    gameObject.SetActive(false);
  }
}
