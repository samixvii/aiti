using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public OrbManager OrbManager { get; private set; }
    public Fader Fader { get; private set; }
    public Commenter Commenter { get; private set; }

    public void Initialize()
    {
        OrbManager = GetComponentInChildren<OrbManager>(true);
        Fader = GetComponentInChildren<Fader>();
        Commenter = GetComponentInChildren<Commenter>();
    }
}
