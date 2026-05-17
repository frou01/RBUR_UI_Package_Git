
using frou01.GrabController;
using frou01.RigidBodyTrain;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.UdonNetworkCalling;
using VRC.SDKBase;
using VRC.Udon;

namespace frou01.RBUR_UI
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class BrakeValve_GACLink : BrakeConnectorValve
    {
        [SerializeField] Controller_Base GAC_Controller;
        [SerializeField] float Open_Position;
        [SerializeField] float Close_Position;

        public override void Init(Train train)
        {
            base.Init(train);
            bool hasThisUB = false;
            foreach (UdonBehaviour ub in brakeModule.indicateUdons)
            {
                if (ub == this.GetComponent<UdonBehaviour>())
                {
                    hasThisUB = true;
                    break;
                }
            }
            if (!hasThisUB)
            {
                UdonBehaviour[] newUBarr = new UdonBehaviour[brakeModule.indicateUdons.Length + 1];
                brakeModule.indicateUdons.CopyTo(newUBarr, 0);
                newUBarr[brakeModule.indicateUdons.Length] = this.GetComponent<UdonBehaviour>();
                brakeModule.indicateUdons = newUBarr;
            }
        }
        public void GAC_OpenPos()
        {
            GAC_Controller.SetPosition(Open_Position);
            this.OpenBrakeValve();
        }
        public void GAC_ClosePos()
        {
            GAC_Controller.SetPosition(Close_Position);
            this.CloseBrakeValve();
        }

        [NetworkCallable]
        public void BrakeVavleUpdated(bool isFront, bool isOpen)
        {
            if (isFront == coupler.FrontOrBack)
            {
                GAC_Controller.SetPosition(isOpen ? Open_Position : Close_Position);
            }
        }
    }
}