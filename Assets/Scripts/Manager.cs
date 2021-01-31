using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{


    #region Game

    List<GameListener> GameListeners = new List<GameListener>();

    public void AddGameListener(GameListener listener)
    {
        if (!GameListeners.Contains(listener))
            GameListeners.Add(listener);
    }

    public void RemoveGameListener(GameListener listener)
    {
        GameListeners.Remove(listener);
    }

    public void TriggerStartGameListeners()
    {
        foreach (GameListener listener in GameListeners.ToArray())
        {
            listener.OnStartGame();
        }
    }

    public void TriggerEndGameListeners()
    {
        foreach (GameListener listener in GameListeners.ToArray())
        {
            listener.OnEndGame();
        }
    }

    public interface GameListener
    {
        void OnStartGame();
        void OnEndGame();
    }

    #endregion
    

    public static Manager Instance;

    public Transform uiParent;
    public Transform cameraParent;

    public Placer Placer { get; private set; }
    public Child Child { get; private set; }
    public Find Find { get; private set; }
    public LanternManager LanternManager { get; private set; }
    public UI UI { get; private set; }
    public CameraManager CameraManager { get; private set; }


    public GameResult gameResult;

    private void Awake()
    {
        Instance = this;

        Placer = GetComponentInChildren<Placer>();
        Child = GetComponentInChildren<Child>();
        LanternManager = GetComponentInChildren<LanternManager>();
        Find = GetComponentInChildren<Find>();
        UI = uiParent.GetComponentInChildren<UI>();
        UI.Initialize();
        CameraManager = cameraParent.GetComponentInChildren<CameraManager>();
    }

    public void FadeAndBegin()
    {
        float delay = UI.Fader.Fade(Color.black);
        Invoke(nameof(StartGame), delay);
    }

    public void StartGame()
    {
        Camera.main.GetComponent<CameraController>().enabled = true;
        TriggerStartGameListeners();
    }

    public void EndGame(bool win)
    {
        gameResult = new GameResult()
        {
            win = win
        };

        TriggerEndGameListeners();
    }

    public struct GameResult
    {
        public bool win;
    }

}
