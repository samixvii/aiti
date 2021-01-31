using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OrbManager : MonoBehaviour, Lantern.LanternPlacedListener, IDragHandler
{
    public LanternOrb lanternOrbPrefab;
    public Transform lanternOrbsParent;

    [Space]
    public ChildOrb childOrb;

    public float distanceScale = 0.1f;
    public float rotationSpeed = 10;

    [Space]

    public float veryCloseDistance;
    public float closeDistance;
    public float veryFarDistance;

    List<LanternOrb> lanternOrbs = new List<LanternOrb>();

    bool informedOfRotation = false;

    private void Awake()
    {
        foreach (Lantern lantern in Manager.Instance.LanternManager.GetLanterns())
            lantern.AddLanternPlacedListener(this);
    }

    void Update()
    {
    }



    public void OnLanternPlaced(Lantern lantern)
    {
        float distance = Vector3.Distance(lantern.activeLanternObject.transform.position, Manager.Instance.Child.activeChildObject.transform.position) * distanceScale;
        CreateLanternOrb(lantern, distance);
    }

    void CreateLanternOrb(Lantern lantern, float distance)
    {
        Debug.Log(distance);
        Vector3 position = GetNewPosition(distance);
        LanternOrb newLanternOrb = Instantiate(lanternOrbPrefab, lanternOrbsParent);
        newLanternOrb.BuildFor(lantern);
        newLanternOrb.transform.position = position;
        lanternOrbs.Add(newLanternOrb);

        if (distance < veryCloseDistance)
            Manager.Instance.UI.Commenter.Comment(lantern.activeLanternObject.transform.position, "My néño is very close...");
        else if (distance < closeDistance)
            Manager.Instance.UI.Commenter.Comment(lantern.activeLanternObject.transform.position, "My néño is close...");
        else if (distance > veryFarDistance)
            Manager.Instance.UI.Commenter.Comment(lantern.activeLanternObject.transform.position, "My néño is very far...");
        else
            Manager.Instance.UI.Commenter.Comment(lantern.activeLanternObject.transform.position, "My néño is far...");
    }

    Vector3 GetNewPosition(float distance)
    {
        List<Vector3> candidatePositions = new List<Vector3>();

        for (int i = 0; i < 10; i++)
        {
            Quaternion rotation = Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
            Vector3 position = childOrb.transform.position + (rotation * Vector3.up) * distance;
            candidatePositions.Add(position);
        }

        if (lanternOrbs.Count == 0)
            return candidatePositions[0];

        Dictionary<float, Vector3> maxLookup = candidatePositions.ToDictionary(position => lanternOrbs.Min(orb => Vector3.Distance(orb.transform.position, position)));
        return maxLookup[maxLookup.Keys.Max()];
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.Rotate((Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime), (-Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime), 0, Space.World);
    }
}
