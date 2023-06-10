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

    // Keyboard keybinds
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
    

    // Rebinding
    [SerializeField] private Transform pressToRebindKeyTransform;

    // Gamepad keybinds
    [SerializeField] private UnityEngine.UI.Button gamepadInteractButton;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;

    [SerializeField] private UnityEngine.UI.Button gamepadInteractAlternateButton;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;

    [SerializeField] private UnityEngine.UI.Button gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    private Action onCloseButtonAction;



    private void Awake() {
        Instance = this;

        closeButton.onClick.AddListener(closeClick);
        // Keyboard
        moveUpButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.MoveUp); });
        moveDownButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.MoveDown); });
        moveLeftButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.MoveLeft); });
        moveRightButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.MoveRight); });
        interactButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Interact); });
        interactAlternateButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Pause); });
        
        // Gamepad
        gamepadInteractButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.GamepadInteract); });
        gamepadInteractAlternateButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.GamepadInteractAlternate); });
        gamepadPauseButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.GamepadPause); });

    }

    private void closeClick() {
       Hide();
       onCloseButtonAction();
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

        // Keyboard
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);

        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);

        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);

        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);

        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);

        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);

        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        // gamepad
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
    }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);
        gamepadInteractButton.Select();
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
