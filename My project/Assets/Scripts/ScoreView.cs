using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PixelCrushers.DialogueSystem;

public class ScoreView : MonoBehaviour
{
    public enum ScoreSource
    {
        CPS = 0,
        ACT = 1,
        CLC = 2,
    }

    [SerializeField] ScoreSource scoreSource;
    [SerializeField] TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        switch(scoreSource)
        {
            case ScoreSource.CPS:
                SetScoreText("CPS");
                break;
            case ScoreSource.ACT:
                SetScoreText("ACT");
                break;
            case ScoreSource.CLC:
                SetScoreText("CLC");
                break;
            default:
                Debug.Log(scoreSource.ToString() + "not registered");
                break;
        }
    }

    private void SetScoreText(string code)
    {
        if(ScoreRecorder.Instance.ScoreKeeper.ContainsKey(code))
        {
            if (scoreText != null)
            {
                scoreText.text = ScoreRecorder.Instance.ScoreKeeper[code].ToString();
            }
        }

    }
}

