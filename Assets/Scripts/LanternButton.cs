using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanternButton : MonoBehaviour, Lantern.LanternPlacedListener
{
    public Image icon;

    public Lantern lantern;

    private void Start()
    {
        lantern.AddLanternPlacedListener(this);
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        icon.sprite = lantern.symbol;
    }

    void OnButtonClicked()
    {
        lantern.ActivateLantern();
    }

    public void OnLanternPlaced(Lantern lantern)
    {
        Disable();
    }

    void Disable()
    {
        GetComponent<Button>().interactable = false;
        icon.CrossFadeColor(Color.black, 0.3f, true, false);
    }

    void Enable()
    {
        GetComponent<Button>().interactable = true;
        icon.CrossFadeColor(Color.white, 0.3f, true, false);
    }
}
