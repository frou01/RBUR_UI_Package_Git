using System.Runtime.CompilerServices;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;
using static System.Collections.Specialized.BitVector32;

namespace frou01.RBUR_UI
{
    public class BrakeGrapStation : UdonSharpBehaviour
    {
        [SerializeField] VRC.SDK3.Components.VRCStation station;
        void Start()
        {
            this.enabled = false;
        }
        public override void InputMoveHorizontal(float value, UdonInputEventArgs args)
        {
            base.InputMoveHorizontal(value, args);
            if (Mathf.Abs(value) > 0.001f && station) station.ExitStation(Networking.LocalPlayer);
        }
        public override void InputMoveVertical(float value, UdonInputEventArgs args)
        {
            base.InputMoveVertical(value, args);
            if (Mathf.Abs(value) > 0.001f && station) station.ExitStation(Networking.LocalPlayer);
        }
    }
}