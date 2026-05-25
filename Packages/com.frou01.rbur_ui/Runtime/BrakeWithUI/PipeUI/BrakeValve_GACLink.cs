
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

        public override void PostProcessOnBuildProcess()
        {
            base.PostProcessOnBuildProcess();
            GAC_Controller.controllerTransform.localRotation = Quaternion.identity;
            GAC_Controller.controllerTransform.localPosition = Vector3.zero;
        }
        public void GAC_OpenPos()
        {
            GAC_Controller.SetPosition(Open_Position);
            this.OpenValve();
        }
        public void GAC_ClosePos()
        {
            GAC_Controller.SetPosition(Close_Position);
            this.CloseValve();
        }

        protected override void onUpdateConnectState()
        {
            base.onUpdateConnectState();
            GAC_Controller.SetPosition(OpenState ? Open_Position : Close_Position);
        }
    }
}