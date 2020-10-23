using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : UIWindow
{
    [SerializeField] private Text endGameText;

    private void OnEnable()
    {
        if(GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.OnGameEnd += UpdateText;
        }
    }

    private void UpdateText(Player winner)
    {
        if (winner != null)
        {
            endGameText.text = winner.Name + ReferenceHolder.Instance.GameSettings.WinString;
        }
        else
        {
           endGameText.text = ReferenceHolder.Instance.GameSettings.DrawString;
        }
    }
}
