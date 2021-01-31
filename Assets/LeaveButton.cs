using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Leave);
    }

    void Leave()
    {
        Application.Quit();
    }

}
