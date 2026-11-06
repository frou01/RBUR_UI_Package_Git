using frou01.GrabController;
using frou01.RigidBodyTrain;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace frou01.RBUR_UI
{
    public class RemoteController : UdonSharpBehaviour
    {
        [SerializeField] TMP_InputField idInputField;
        [SerializeField] Controller_Base reverserController;
        [SerializeField] Controller_Base throttleController;

        [SerializeField] bool isAccelMode;//true = 加速(力)を制御, false = 速度を直接制御
        int targetID;
        [SerializeField][HideInInspector] public TrainManager trainManager;
        Train targetTrain;
        //対象を切り替えた時に取得する。コンポーネントが無ければ null
        ConstantForce targetForce;
        Rigidbody targetBody;
        Transform targetTransform;
        float[] throttle = new float[1];
        [SerializeField]float throttleMultiplier = 0.5f;
        int reverser = 0;

        public void Start()
        {
            throttle = throttleController.currentNormalizePosition_Exposed;
            if (!trainManager || trainManager.Trains == null)
            {
                Debug.LogWarning("[RemoteController] TrainManager is not assigned");
            }
        }

        public void ChangeTargetID()
        {
            if (targetForce)
            {
                targetForce.relativeForce = Vector3.zero;
            }
            targetID = -1;
            int.TryParse(idInputField.text, out targetID);
            Train[] trains = trainManager ? trainManager.Trains : null;
            if (trains != null && 0 <= targetID && targetID < trains.Length && trains[targetID])
            {
                targetTrain = trains[targetID];
                targetForce = targetTrain.GetComponent<ConstantForce>();
                targetBody = targetTrain.GetComponent<Rigidbody>();
                targetTransform = targetTrain.transform;

                Networking.SetOwner(Networking.LocalPlayer, targetTrain.gameObject);
            }
            else
            {
                targetTrain = null;
                targetForce = null;
                targetBody = null;
                targetTransform = null;
            }
            ApplyControl();
        }
        public void ReverseChanged()
        {
            reverser = reverserController.currentSegment_Exposed[0] - 1;
        }
        void FixedUpdate()
        {
            ApplyControl();
        }

        private void ApplyControl()
        {
            if (targetTrain)
            {
                if(Networking.IsOwner(Networking.LocalPlayer, targetTrain.gameObject))
                {
                    if (Mathf.Abs(throttle[0]) < 0.1) throttle[0] = 0;
                    if (isAccelMode)
                    {
                        if (targetForce) targetForce.relativeForce = new Vector3(0, 0, throttle[0] * throttleMultiplier * reverser);
                    }
                    else
                    {
                        if (targetBody) targetBody.velocity = targetTransform.rotation * new Vector3(0, 0, throttle[0] * throttleMultiplier * reverser);
                    }
                }
                else
                {
                    if (targetForce) targetForce.relativeForce = new Vector3(0, 0, 0);
                }
            }
        }
    }
}