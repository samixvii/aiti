using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Find : MonoBehaviour, Placer.Placable, Manager.GameListener
{
    #region ConsumeEssence

    List<ConsumeEssenceListener> ConsumeEssenceListeners = new List<ConsumeEssenceListener>();

    public void AddConsumeEssenceListener(ConsumeEssenceListener listener)
    {
        if (!ConsumeEssenceListeners.Contains(listener))
            ConsumeEssenceListeners.Add(listener);
    }

    public void RemoveConsumeEssenceListener(ConsumeEssenceListener listener)
    {
        ConsumeEssenceListeners.Remove(listener);
    }

    public void TriggerConsumeEssenceListeners()
    {
        foreach (ConsumeEssenceListener listener in ConsumeEssenceListeners.ToArray())
        {
            listener.OnConsumeEssence();
        }
    }

    public interface ConsumeEssenceListener
    {
        void OnConsumeEssence();
    }

    #endregion

    public GameObject findPlacePreview;
    public GameObject findEffect;

    public float findTime = 10;

    GameObject findEffectInstance;
    public int maxEssence = 3;
    
    int essence;

    private void Awake()
    {
        Manager.Instance.AddGameListener(this);
    }

    internal int GetEssencesLeft()
    {
        return essence;
    }

    public void StartFinding()
    {
        if (findEffectInstance != null)
            return;

        if (essence == 1)
            Manager.Instance.UI.Commenter.Comment(Vector3.zero, "My last essence, I must use it wisely.", true);

        if (Manager.Instance.LanternManager.GetLanterns().Select(lantern => lantern.activeLanternObject).All(lanternObject => lanternObject == null))
        {
            NoLanterns();
            return;
        }

        if (essence > 0)
            Manager.Instance.Placer.Place(findPlacePreview, null, this, null, false);
    }

    void NoLanterns()
    {
        Manager.Instance.UI.Commenter.Comment(Vector3.zero, "I must place a lantern first...", true);
    }

    public void OnTryPlaced(Vector3 placedLocation)
    {

    }

    public void OnPlaced(Vector3 placedLocation)
    {
        Debug.Log("Placed");
        findEffectInstance = Instantiate(findEffect);
        findEffectInstance.transform.position = placedLocation;

        essence--;
        TriggerConsumeEssenceListeners();

        RaycastHit[] hits = Physics.SphereCastAll(placedLocation, 50, Vector3.up);
        bool found = false;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == Manager.Instance.Child.activeChildObject)
            {
                ChildFound();
                found = true;
                break;
            }
        }

        StartCoroutine(FindFinished(found));
    }

    void ChildFound()
    {
        Manager.Instance.Child.Found();
        Debug.Log("Child Found");
    }

    void ChildNotFound()
    {

    }

    IEnumerator FindFinished(bool found)
    {
        yield return new WaitForSeconds(findTime);

        if (!found)
            Manager.Instance.UI.Commenter.Comment(findEffectInstance.transform.position, "My néño is not here...");

        Destroy(findEffectInstance);

        if (!found && essence == 0)
            Manager.Instance.EndGame(false);
    }

    public void OnStartGame()
    {
        essence = maxEssence;
    }

    public void OnEndGame()
    {

    }
}
