using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    private Dictionary<string, int> _scoreKeeper = new Dictionary<string, int>();

    public Dictionary<string, int> ScoreKeeper => _scoreKeeper;
    
    [Tooltip("Typically leave unticked so temporary Dialogue Managers don't unregister your functions.")]
    public bool unregisterOnDisable = false;

    public const string CPS_NAME = "CPS";
    public const string ACT_NAME = "ACT";
    public const string CLC_NAME = "CLC";

    static ScoreRecorder _instance;
    public static ScoreRecorder Instance => _instance;

    public void ResetScore()
    {
        _scoreKeeper = new Dictionary<string, int>();

        _scoreKeeper.Add(CPS_NAME, 0);
        _scoreKeeper.Add(ACT_NAME, 0);
        _scoreKeeper.Add(CLC_NAME, 0);

        Debug.Log("All Score Reset");
    }

    public void AddScore(string category, double addition)
    {
        if(!_scoreKeeper.ContainsKey(category))
        {
            _scoreKeeper.Add(category, 0);
        }

        _scoreKeeper[category] += (int)addition;

        Debug.Log(string.Format("Score {0} + {1} ", category, addition.ToString()));
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void OnEnable()
    {
        // Make the functions available to Lua: (Replace these lines with your own.)
        Lua.RegisterFunction(nameof(AddScore), this, SymbolExtensions.GetMethodInfo(() => AddScore(string.Empty, 0)));
        Lua.RegisterFunction(nameof(ResetScore), this, SymbolExtensions.GetMethodInfo(() => ResetScore()));
    }
    void OnDisable()
    {
        if (unregisterOnDisable)
        {
            // Remove the functions from Lua: (Replace these lines with your own.)
            Lua.UnregisterFunction(nameof(AddScore));
            Lua.UnregisterFunction(nameof(ResetScore));
        }
    }


}
