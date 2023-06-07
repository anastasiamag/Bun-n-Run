using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI countdownText;


  private void Start() {
    GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
    Hide();
  }

  private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
    if(GameManager.Instance.IsCountdownToStartActive()) {
      Show();
    }else {
      Hide();
    }
  }

  private void Update() {
    countdownText.text = GameManager.Instance.GetCountdownToStartTimer().ToString("#");
  }

  private void Show() {
    gameObject.SetActive(true);
  }

  private void Hide() {
    gameObject.SetActive(false);
  }
}
