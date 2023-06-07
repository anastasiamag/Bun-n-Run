using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{ get; private set; }


    public event EventHandler onStateChanged;

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
    private float gamePlayingTimerMax= 10f;


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
}