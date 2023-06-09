using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ControlsUI : MonoBehaviour
{

    public static ControlsUI Instance { get; private set; }


    [SerializeField] private UnityEngine.UI.Button closeButton;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private TextMeshProUGUI moveUpText;

    [SerializeField] private UnityEngine.UI.Button moveDownButton;
    [SerializeField] private TextMeshProUGUI moveDownText;

    [SerializeField] private UnityEngine.UI.Button moveLeftButton;
    [SerializeField] private TextMeshProUGUI moveLeftText;

    [SerializeField] private UnityEngine.UI.Button moveRightButton;
    [SerializeField] private TextMeshProUGUI moveRightText;

    [SerializeField] private UnityEngine.UI.Button interactButton;
    [SerializeField] private TextMeshProUGUI interactText;

    [SerializeField] private UnityEngine.UI.Button interactAlternateButton;
    [SerializeField] private TextMeshProUGUI interactAlternateText;

    [SerializeField] private UnityEngine.UI.Button pauseButton;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Transform pressToRebindKeyTransform;


    private void Awake() {
        Instance = this;

        closeButton.onClick.AddListener(closeClick);

        moveUpButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Interact); });
        interactAlternateButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Pause); });

    }

    private void closeClick() {
       Hide();
    }
    private void Start() {
        UpdateVisual();
        GameManager.Instance.onGameUnpaused += GameManager_OnGameUnpaused;
        Hide();
        HidePressToRebindKey();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);

        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);

        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);

        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);

        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);

        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);

        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public void ShowPressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding) {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();    
        }); 
    }
//  
}
