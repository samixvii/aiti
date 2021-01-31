using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour, Manager.GameListener
{
    #region ChildRise

    List<ChildRiseListener> ChildRiseListeners = new List<ChildRiseListener>();

    public void AddChildRiseListener(ChildRiseListener listener)
    {
        if (!ChildRiseListeners.Contains(listener))
            ChildRiseListeners.Add(listener);
    }

    public void RemoveChildRiseListener(ChildRiseListener listener)
    {
        ChildRiseListeners.Remove(listener);
    }

    public void TriggerChildRiseListeners()
    {
        foreach (ChildRiseListener listener in ChildRiseListeners.ToArray())
        {
            listener.OnChildRise();
        }
    }

    public interface ChildRiseListener
    {
        void OnChildRise();
    }

    #endregion

    public GameObject activeChildObject;
    public Collider spawnArea;
    public GameObject childPrefab;
    public GameObject childFoundRisePrefab;


    [Space]
    public Vector2 randomRiseDelay = new Vector2(1, 5);

    [HideInInspector] public GameObject childRiseInstance;

    private void Awake()
    {
        Manager.Instance.AddGameListener(this);
    }

    public void OnStartGame()
    {
        SpawnChild();
    }

    void SpawnChild()
    {
        Vector3 location = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), spawnArea.bounds.center.y, Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));

        Debug.Log("Spawned at " + location);

        RaycastHit hit;
        if (Physics.Raycast(location, Vector3.down, out hit))
        {
            Vector3 childLocation = hit.point;
            activeChildObject = Instantiate(childPrefab, childLocation, Quaternion.identity);
        }

    }

    public void Found()
    {
        StartCoroutine(WaitAndRise());
    }

    IEnumerator WaitAndRise()
    {
        yield return new WaitForSeconds(Random.Range(randomRiseDelay.x, randomRiseDelay.y));
        childRiseInstance = Instantiate(childFoundRisePrefab);
        childRiseInstance.transform.position = activeChildObject.transform.position;

        Invoke(nameof(TriggerChildRiseListeners), 1);
    }



    public void OnEndGame()
    {

    }



}
