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
            if(ObjectToggle_Knuckle) ObjectToggle_Knuckle.setState(knucleState);
        }
        [NetworkCallable]
        public void setKeyState(int keyState)
        {

            if(ObjectSwitch_Key) ObjectSwitch_Key.setState(keyState);
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