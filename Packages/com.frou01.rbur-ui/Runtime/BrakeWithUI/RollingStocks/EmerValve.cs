using frou01.GrabController;
using frou01.RigidBodyTrain;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace frou01.RBUR_UI
{
    public class EmerValve : UdonSharpBehaviour
    {
        [SerializeField] Controller_Base EmerValveController;
        [SerializeField] AbstractBrake brakeModule;
        void Start()
        {
        }
        public void OnDrop_()//EmerBrakeValve release
        {
            EmerValveController.SetPosition(0);
            EmerValveController.RequestSerialization();
            this.enabled = false;
        }
        public void EmerValveClose()
        {
            this.enabled = false;
        }
        public void EmerValveOpen()
        {
            this.enabled = true;
        }
        protected virtual void LateUpdate()
        {
            brakeModule.straightBrakePressure[0] = 0.1f;
        }

    }
}