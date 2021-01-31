using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public PlacerOverlay overlay;

    bool placing = false;
    
    GameObject placePreviewValid;
    GameObject placePreviewInvalid;

    Placable waitingPlacable;
    CheckPlace waitingCheckPlace;

    public void Place(GameObject placePreviewPrefab, GameObject placePreviewInvalidPrefab, Placable placable, CheckPlace checkPlace, bool showOverlay)
    {
        placePreviewValid = Instantiate(placePreviewPrefab);

        if (placePreviewInvalidPrefab != null)
            placePreviewInvalid = Instantiate(placePreviewInvalidPrefab);

        placing = true;
        waitingPlacable = placable;
        waitingCheckPlace = checkPlace;

        if (showOverlay)
            overlay.StartOverlay();
    }

    GameObject GetPlacePreview(bool valid)
    {
        placePreviewValid.gameObject.SetActive(valid);
        placePreviewInvalid.gameObject.SetActive(!valid);
        if (valid)
            return placePreviewValid;
        else
            return placePreviewInvalid;
    }

    private void Update()
    {
        if (placing)
        {
            RaycastHit hit;
            bool valid = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, 1<<9))
            {
                GameObject placePreview;

                if (waitingCheckPlace != null)
                {
                    valid = waitingCheckPlace.CanPlace(hit.point);
                    placePreview = GetPlacePreview(valid);
                } else
                {
                    placePreview = placePreviewValid;
                }

                placePreview.transform.position = hit.point;
            }

            if (Input.GetAxis("Place") > 0)
            {
                if (valid)
                    FinishPlacing(hit.point);
                else
                    waitingPlacable.OnTryPlaced(hit.point);
            }

            if (Input.GetAxis("Cancel") > 0)
            {
                CancelPlacing();
            }
        }
    }

    void FinishPlacing(Vector3 location)
    {
        CancelPlacing();
        waitingPlacable.OnPlaced(location);
    }

    void CancelPlacing()
    {
        Destroy(placePreviewValid);
        Destroy(placePreviewInvalid);
        placing = false;
        overlay.StopOverlay();
    }

    public interface Placable
    {
        void OnTryPlaced(Vector3 placedLocation);
        void OnPlaced(Vector3 placedLocation);
    }

    public interface CheckPlace
    {
        bool CanPlace(Vector3 location);
    }
}
