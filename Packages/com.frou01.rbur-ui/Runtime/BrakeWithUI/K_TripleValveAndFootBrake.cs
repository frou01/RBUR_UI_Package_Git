
using frou01.GrabController;
using frou01.RigidBodyTrain;
using UnityEngine;

public class K_TripleValveAndFootBrake : K_TripleValve
{
    [SerializeField] Controller_Base EmerValve;
    [SerializeField] Controller_Screw HandBrakeScrew;
    [SerializeField] Controller_Base HandBrakeController;
    protected int[] valvePos = new int[1];
    protected float[] handBrakeState = new float[1];
    [SerializeField] protected float handBrakePower = 10000;
    protected override void Start()
    {
        base.Start();

        if (EmerValve != null) valvePos = EmerValve.currentSegment_Exposed;

        if (HandBrakeScrew != null) handBrakeState = HandBrakeScrew.normScrewPosition;
        else if (HandBrakeController != null) handBrakeState = HandBrakeController.currentNormalizePosition_Exposed;
    }

    public void OnDrop_()//EmerBrakeValve release
    {
        EmerValve.SetPosition(0);
        EmerValve.RequestSerialization();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void LateUpdate()
    {
        if (valvePos[0] == 1)
        {
            straightBrakePressure[0] = m_straightBrakePressure = 0.1f;
        }
        base.LateUpdate();
    }

    protected float temp_brake;
    protected override void ApplyForceToWheel()
    {
        brakeFactor[0] = 0;
        if (isOwnerState)
            for (int index = 0; index < wheelBrakes.Length; index++)
            {
                temp_brake = (CylinderPressure - 0.1f) * wheelMultiplier[index];
                wheelBrakes[index][0] = Mathf.Clamp(temp_brake, handBrakeState[0] * handBrakePower, temp_brake);
                brakeFactor[0] += wheelBrakes[index][0] / wheelMultiplier[index];
                wheelBrakes[index][0] += Mathf.Lerp(StaticFriction, DynamicFriction, Mathf.Abs(wheelTreadSpeeds[index][0]) / DynamicFrictionSpeed);
            }
        else
            for (int index = 0; index < wheelBrakes.Length; index++)
            {
                temp_brake = (CylinderPressure - 0.1f) * wheelMultiplier[index];
                brakeFactor[0] += Mathf.Clamp(temp_brake, handBrakeState[0] * handBrakePower, temp_brake) / wheelMultiplier[index];
            }
    }
}
