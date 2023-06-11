using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HowToPlay : MonoBehaviour
{
    public static HowToPlay Instance { get; private set; }

   private void Start() {
    GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
    Show();
   }

   private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
    if (GameManager.Instance.IsCountdownToStartActive()) {
        Hide();
    }
   }

    public void Show() {

        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    
}
