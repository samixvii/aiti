using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour, Manager.GameListener
{
    public bool shortcutToGame;

    public List<RectTransform> mainMenu;
    public List<RectTransform> inGame;
    public List<RectTransform> credits;

    public List<RectTransform> loss;
    public List<RectTransform> win;

    public Animation intro;
    public Animation lossAnim;
    public Animation winAnim;

    private void Awake()
    {
        Manager.Instance.AddGameListener(this);
    }

    private void Start()
    {
        if (!shortcutToGame)
            OpenMenu();
        else
            StartGame();
    }

    public void Leave()
    {
        Application.Quit();
    }

    public void MenuClick()
    {
        float delay = Manager.Instance.UI.Fader.Fade(Color.black);
        Invoke(nameof(OpenMenu), delay);
    }

    public void TryAgain()
    {
        FadeAndStartGame();
    }

    public void Play()
    {
        float delay = Manager.Instance.UI.Fader.Fade(Color.black);
        Invoke(nameof(PlayIntro), delay);
    }

    void PlayIntro()
    {
        SetActive(mainMenu, false);
        intro.gameObject.SetActive(true);
        intro.Play();
        Invoke(nameof(FadeAndStartGame), intro.clip.length);
    }

    void FadeAndStartGame()
    {
        float delay = Manager.Instance.UI.Fader.Fade(Color.black);
        Invoke(nameof(StartGame), delay);
    }

    void StartGame()
    {
        Manager.Instance.StartGame();
        SetActive(inGame, true);
        intro.gameObject.SetActive(false);
    }


    public void Credits()
    {
        float delay = Manager.Instance.UI.Fader.Fade(Color.black);
        Invoke(nameof(OpenCredits), delay);
    }

    void OpenCredits()
    {
        SetActive(credits, true);
    }

    public void OpenMenu()
    {
        SetActive(mainMenu, true);
    }

    void SetActive(List<RectTransform> menus, bool active)
    {
        foreach (List<RectTransform> menuSets in new List<List<RectTransform>>() { mainMenu, inGame, credits, win, loss })
            foreach (RectTransform rectTransform in menuSets)
                if (menus.Contains(rectTransform))
                    rectTransform.gameObject.SetActive(active);
                else if (active == true)
                    rectTransform.gameObject.SetActive(false);
    }

    public void OnStartGame()
    {

    }

    public void OnEndGame()
    {
        if (Manager.Instance.gameResult.win)
        {
            WinGame();
        } else
        {
            LostGame();
        }
    }


    void WinGame()
    {
        float delay = Manager.Instance.UI.Fader.Fade(Color.white);
        Invoke(nameof(OpenFound), delay);
    }

    void OpenFound()
    {
        Manager.Instance.CameraManager.GoToStill();
        SetActive(win, true);
        winAnim.Play();
    }

    void LostGame()
    {
        float delay = Manager.Instance.UI.Fader.Fade(Color.black);
        Invoke(nameof(OpenLoss), delay);
    }

    void OpenLoss()
    {
        Manager.Instance.CameraManager.GoToStill();
        SetActive(loss, true);
        lossAnim.Play();
    }
}
