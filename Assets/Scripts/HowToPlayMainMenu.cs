using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayMainMenu : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button closeButton;

    private void Awake() {
        closeButton.onClick.AddListener(closeClick);
    }

    private void closeClick() {
       Hide();
    }

    public void Show() {

        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
