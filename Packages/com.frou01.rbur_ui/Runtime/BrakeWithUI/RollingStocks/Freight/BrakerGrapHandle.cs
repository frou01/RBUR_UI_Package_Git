
using frou01.GrabController;
using System.Runtime.CompilerServices;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;
using static VRC.SDKBase.VRCPlayerApi;

namespace frou01.RBUR_UI
{
    public class BrakerGrapHandle : ControllerSlider_Pickup
    {
        [SerializeField] Transform stationTransform;
        [SerializeField] Transform indicateLeverTransform;
        [SerializeField] float indicateLeverLength = 2.9f;
        Vector3 stationOrig;
        Quaternion leverOrig;
        [SerializeField] VRC.SDK3.Components.VRCStation station;
        [SerializeField] BrakeGrapStation grapStation;

        protected override void Start()
        {
            stationOrig = stationTransform.localPosition;
            leverOrig = indicateLeverTransform.localRotation;
            base.Start();
        }

        public override void OnPickup()
        {
            base.OnPickup();
            if (station) station.UseStation(Networking.LocalPlayer);
            grapStation.enabled = false;
        }
        public override void OnDrop()
        {
            base.OnDrop();
            grapStation.enabled = true;
        }
        public override void Interact()
        {
            base.Interact();
            if (station) station.UseStation(Networking.LocalPlayer);
        }
        protected override void ApplyToTransform()
        {
            controllerTransform.Translate(0, controllerPosition - controllerTransform.localPosition.y, 0);

            stationTransform.localPosition = stationOrig;
            stationTransform.Translate(0, -controllerPosition, 0);

            indicateLeverTransform.localRotation = leverOrig;
            indicateLeverTransform.Rotate(-Mathf.Rad2Deg * Mathf.Asin(controllerPosition / indicateLeverLength), 0, 0);
        }
    }
}
