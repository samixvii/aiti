using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindButton : MonoBehaviour
{

    private void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        Manager.Instance.Find.StartFinding();
    }

}
