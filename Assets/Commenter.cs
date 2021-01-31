using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commenter : MonoBehaviour
{
    public GameObject commentPrefab;

    public void Comment(Vector3 location, string text, bool center = false)
    {
        StartCoroutine(ShowComment(location, text, center));
    }

    IEnumerator ShowComment(Vector3 location, string text, bool center)
    {
        GameObject comment = Instantiate(commentPrefab, transform);
        Destroy(comment, comment.GetComponent<Animation>().clip.length);
        TMPro.TMP_Text textObject = comment.GetComponentInChildren<TMPro.TMP_Text>();
        textObject.text = text;

        while (comment != null)
        {
            Vector3 projected = Camera.main.WorldToScreenPoint(location);

            if (center)
                projected = transform.position;
            
            comment.transform.position = projected;
            yield return null;
        }

    }

}
