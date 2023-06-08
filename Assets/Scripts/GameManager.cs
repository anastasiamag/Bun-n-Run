using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{ get; private set; }


    public event EventHandler onStateChanged;
    public event EventHandler onGamePaused;
    public event EventHandler onGameUnpaused;



    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer= 1f;
    private float countdownToStartTimer= 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax= 30f;
    private bool IsGamePaused = false;


    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        PauseGame();
    }

    private void Awake() {
        Instance=this;
        state = State.WaitingToStart;
    }

    private void Update() {
        switch (state){

            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) {
                    state = State.CountdownToStart;
                    onStateChanged?.Invoke(this, EventArgs.Empty);
                }
            break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f) {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    onStateChanged?.Invoke(this, EventArgs.Empty);
                }
            break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) {
                    state = State.GameOver;
                    onStateChanged?.Invoke(this, EventArgs.Empty);
                }
            break;

            case State.GameOver:
            break;
        }
        Debug.Log(state);
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying; 
    }

    public bool IsCountdownToStartActive() {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetGameTimerNormalized() {
        return 1- (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void PauseGame() {
        IsGamePaused = !IsGamePaused;
        if (IsGamePaused) {
            Time.timeScale = 0f;
            onGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            onGameUnpaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1f;
        }
    }
}
