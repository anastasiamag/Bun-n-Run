using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HowToPlay : MonoBehaviour
{
   public static HowToPlay Instance { get; private set; }

   [SerializeField] private UnityEngine.UI.Button closeButton;

   private void Awake() {
    closeButton.onClick.AddListener(closeClick);
   }

    private void closeClick() {
       Hide();
    //    onCloseButtonAction();
    }


    public void Show() {

        gameObject.SetActive(true);
        // gamepadInteractButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
