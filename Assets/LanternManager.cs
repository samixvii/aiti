using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LanternManager : MonoBehaviour
{
    public Texture2D lanternMap;

    public List<Lantern> GetLanterns()
    {
        return GetComponentsInChildren<Lantern>().ToList();
    }


}
