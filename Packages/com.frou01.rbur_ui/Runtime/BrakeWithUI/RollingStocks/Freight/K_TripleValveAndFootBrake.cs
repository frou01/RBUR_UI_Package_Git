
using frou01.GrabController;
using frou01.RigidBodyTrain;
using UnityEngine;
using UnityEngine.Serialization;

public class K_TripleValveAndFootBrake : K_TripleValve
{
    [SerializeField] Controller_Screw HandBrakeScrew;
    [SerializeField] Controller_Base HandBrakeController;
    [SerializeField] Controller_Base[] HandBrakeControllers;
    protected float[][] handBrakeState = new float[1][] { new float[1] };
    [SerializeField] protected float handBrakePower = 10000;
    protected override void Start()
    {
        base.Start();

        if (HandBrakeScrew != null) handBrakeState = new float[1][] { HandBrakeScrew.normScrewPosition };
        else
        {
            if (HandBrakeController)
            {
                Controller_Base[] NewHandBrakeControllers = new Controller_Base[HandBrakeControllers.Length+1];
                HandBrakeControllers.CopyTo(NewHandBrakeControllers, 0 );
                NewHandBrakeControllers[HandBrakeControllers.Length] = HandBrakeController;
                HandBrakeControllers = NewHandBrakeControllers;
            }
            handBrakeState = new float[HandBrakeControllers.Length][];
            int idx = 0;
            foreach (Controller_Base handBrake in HandBrakeControllers)
            {
                handBrakeState[idx] = handBrake.currentNormalizePosition_Exposed;
                idx++;
            }
        }
    }

    protected float getHandBrakePosition()
    {
        float ans = 0;
        foreach (float[] AnHandBrakeState in handBrakeState)
        {
            ans = Mathf.Max(ans, AnHandBrakeState[0]);
        }
        return ans;
    }

    protected float temp_brake;
    protected override void ApplyForceToWheel()
    {
        brakeFactor[0] = 0;
        if (isOwnerState)
            for (int index = 0; index < wheelBrakes.Length; index++)
            {
                temp_brake = (CylinderPressure - 0.1f) * wheelMultiplier[index];
                wheelBrakes[index][0] = Mathf.Max(temp_brake, getHandBrakePosition() * handBrakePower);
                brakeFactor[0] += wheelBrakes[index][0] / wheelMultiplier[index];
                wheelBrakes[index][0] += Mathf.Lerp(StaticFriction, DynamicFriction, Mathf.Abs(wheelTreadSpeeds[index][0]) / DynamicFrictionSpeed);
            }
        else
            for (int index = 0; index < wheelBrakes.Length; index++)
            {
                temp_brake = (CylinderPressure - 0.1f) * wheelMultiplier[index];
                brakeFactor[0] += Mathf.Max(temp_brake, getHandBrakePosition() * handBrakePower) / wheelMultiplier[index];
            }
    }
}
