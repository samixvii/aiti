using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour, Placer.Placable, Manager.GameListener, Placer.CheckPlace
{
    #region LanternPlaced

    List<LanternPlacedListener> LanternPlacedListeners = new List<LanternPlacedListener>();

    public void AddLanternPlacedListener(LanternPlacedListener listener)
    {
        if (!LanternPlacedListeners.Contains(listener))
            LanternPlacedListeners.Add(listener);
    }

    public void RemoveLanternPlacedListener(LanternPlacedListener listener)
    {
        LanternPlacedListeners.Remove(listener);
    }

    public void TriggerLanternPlacedListeners(Lantern lantern)
    {
        foreach (LanternPlacedListener listener in LanternPlacedListeners.ToArray())
        {
            listener.OnLanternPlaced(lantern);
        }
    }

    public interface LanternPlacedListener
    {
        void OnLanternPlaced(Lantern lantern);
    }

    #endregion

    public Sprite symbol;
    public Color color;

    public GameObject lanternPlacePreviewValid;
    public GameObject lanternPlacePreviewInvalid;

    public LanternObject lanternObjectPrefab;
    public LanternObject activeLanternObject;

    private void Awake()
    {
        Manager.Instance.AddGameListener(this);
    }

    public void ActivateLantern()
    {
        Manager.Instance.Placer.Place(lanternPlacePreviewValid, lanternPlacePreviewInvalid,  this, this, true);
    }

    public void OnPlaced(Vector3 placedLocation)
    {
        activeLanternObject = Instantiate(lanternObjectPrefab);
        activeLanternObject.BuildFor(this);
        activeLanternObject.transform.position = placedLocation;

        TriggerLanternPlacedListeners(this);
    }

    public void OnStartGame()
    {

    }

    public void OnEndGame()
    {
        if (activeLanternObject)
            Destroy(activeLanternObject.gameObject);
    }

    public bool CanPlace(Vector3 location)
    {
        Texture2D map = Manager.Instance.LanternManager.lanternMap;

        Vector2 location2D = new Vector2(location.x, location.z);
        Vector2 pixelPosition = (Vector2.Scale(location2D, new Vector2(map.width, map.height)) / 1000);

        Color pixel = map.GetPixel((int)pixelPosition.x, (int)pixelPosition.y);

        return IsSimilar(pixel, color, 0.1f);
    }

    bool IsSimilar(Color a, Color b, float limit)
    {
        return Mathf.Abs(a.r - b.r) + Mathf.Abs(a.g - b.g) + Mathf.Abs(a.b - b.b) <= limit;
    }

    public void OnTryPlaced(Vector3 placedLocation)
    {
        Manager.Instance.UI.Commenter.Comment(placedLocation, "The lanterns must harmonize with their surroundings...");
    }
}
