using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClockUI : MonoBehaviour
{
   [SerializeField] private UnityEngine.UI.Image timerImage;

   private void Update() {
    timerImage.fillAmount = GameManager.Instance.GetGameTimerNormalized();
   }
}
