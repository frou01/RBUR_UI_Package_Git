
using frou01.GrabController;
using System.Runtime.CompilerServices;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using static VRC.SDKBase.VRCPlayerApi;

public class BrakerGrapHandle : ControllerSlider_Pickup
{
    [SerializeField] Transform stationTransform;
    [SerializeField] Transform leverTransform;
    Vector3 stationOrig;
    Quaternion leverOrig;
    [SerializeField] VRC.SDK3.Components.VRCStation station;

    protected override void Start()
    {
        stationOrig = stationTransform.localPosition;
        leverOrig = leverTransform.localRotation;
        base.Start();
    }

    public override void OnPickup()
    {
        base.OnPickup();
        station.UseStation(Networking.LocalPlayer);
    }
    protected override void ApplyToTransform()
    {
        controllerTransform.Translate(0, controllerPosition - controllerTransform.localPosition.y, 0);

        stationTransform.localPosition = stationOrig;
        stationTransform.Translate(0, -controllerPosition, 0);

        leverTransform.localRotation = leverOrig;
        leverTransform.Rotate(-Mathf.Rad2Deg * Mathf.Asin(controllerPosition/2.9f), 0, 0);
    }
}
