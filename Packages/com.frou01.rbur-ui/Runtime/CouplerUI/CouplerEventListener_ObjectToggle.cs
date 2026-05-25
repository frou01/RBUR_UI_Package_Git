using frou01.util;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.UdonNetworkCalling;
using VRC.SDKBase;
using VRC.Udon;

namespace frou01.RBUR_UI
{
    public class CouplerEventListener_ObjectToggle : UdonSharpBehaviour
    {
        [SerializeField] SyncedObjectToggle ObjectToggle_Knuckle;
        [SerializeField] SyncedObjectSwitch ObjectSwitch_Key;
        [NetworkCallable]
        public void setKnuckleState(bool knucleState)
        {
            ObjectToggle_Knuckle.setState(knucleState);
        }
        [NetworkCallable]
        public void setKeyState(int knucleState)
        {

            ObjectSwitch_Key.setState(knucleState);
        }
        public void OnOpening()
        {

        }
        public void OnOpened()
        {

        }
        public void OnUnlocking()
        {

        }
        public void OnKnuckleClosing()
        {

        }
        public void OnCouplerCrashing()
        {

        }
        public void OnDecoupling()
        {

        }
        public void OnCoupling()
        {

        }
    }
}