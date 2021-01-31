using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EssenceBar : MonoBehaviour, Find.ConsumeEssenceListener
{
    public List<Image> images;

    public Color lowColor;
    public Color usedColor;

    private void Start()
    {
        Manager.Instance.Find.AddConsumeEssenceListener(this);
    }

    public void OnConsumeEssence()
    {

        for (int i = 0; i < images.Count; i++)
        {
            int essencesLeft = Manager.Instance.Find.GetEssencesLeft();
            if (i >= essencesLeft)
            {
                images[i].CrossFadeColor(usedColor, 0.3f, true, false);
            } else if (i == 0 && essencesLeft == 1)
            {
                images[i].CrossFadeColor(lowColor, 0.3f, true, false);
            }
        }

    }
}
