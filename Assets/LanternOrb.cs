using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanternOrb : MonoBehaviour
{
    public Image image;
    public ParticleSystem energy;

    public void BuildFor(Lantern lantern)
    {
        image.sprite = lantern.symbol;

        foreach (ParticleSystem particleSystem in GetComponentsInChildren<ParticleSystem>())
        {
            ParticleSystem.MainModule mainModule = particleSystem.main;
            mainModule.startColor = lantern.color;
        }
    }

    private void Update()
    {
        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = energy.velocityOverLifetime;

        Vector3 childOrbLocation = transform.InverseTransformPoint(Manager.Instance.UI.OrbManager.childOrb.transform.position);
        float maxLocation = Mathf.Max(childOrbLocation.x, childOrbLocation.y, childOrbLocation.z);
        childOrbLocation /= maxLocation;

        //velocityOverLifetimeModule.orbitalOffsetX = childOrbLocation.x;
        //velocityOverLifetimeModule.orbitalOffsetY = childOrbLocation.y;
        //velocityOverLifetimeModule.orbitalOffsetZ = childOrbLocation.z;

        ParticleSystem.MinMaxCurve x = velocityOverLifetimeModule.orbitalOffsetX;
        x.curve.RemoveKey(1);
        x.curve.AddKey(1, childOrbLocation.x);
        x.curveMultiplier = maxLocation;
        velocityOverLifetimeModule.orbitalOffsetX = x;

        ParticleSystem.MinMaxCurve y = velocityOverLifetimeModule.orbitalOffsetY;
        y.curve.RemoveKey(1);
        y.curve.AddKey(1, childOrbLocation.y);
        y.curveMultiplier = maxLocation;
        velocityOverLifetimeModule.orbitalOffsetY = y;

        ParticleSystem.MinMaxCurve z = velocityOverLifetimeModule.orbitalOffsetZ;
        z.curve.RemoveKey(1);
        z.curve.AddKey(1, childOrbLocation.z);
        z.curveMultiplier = maxLocation;
        velocityOverLifetimeModule.orbitalOffsetZ = z;

    }

}
