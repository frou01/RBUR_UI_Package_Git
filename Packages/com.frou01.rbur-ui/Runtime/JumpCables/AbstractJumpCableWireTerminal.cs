
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AbstractJumpCableWireTerminal : LUPickUpRC_RootChangeable
{
    [SerializeField] AbstractJumpCableWireTerminal PairTerminal;
    [SerializeField] float CableLength = 0.7f;

    public AbstractJumpCableBoxTerminal connectedBoxTerminal;//LUPickUpRCの子クラスとして接続・切断に合わせて参照切り替えを実施

    protected override void onPickInit_OwnerOnly()
    {
        base.onPickInit_OwnerOnly();
        CallOppositeTerminal();
    }
    protected override void onDropInit_OwnerOnly()
    {
        base.onDropInit_OwnerOnly();
        CallOppositeTerminal();
    }

    protected virtual void CallOppositeTerminal()
    {
        if (!PairTerminal.connectedBoxTerminal && !PairTerminal.pickedFlag)
        {
            Networking.SetOwner(LocalPlayer, PairTerminal.gameObject);
            PairTerminal.SetPositionAndRotation_OwnerOnly(this.transform.position + (PairTerminal.transform.position - this.transform.position).normalized * CableLength, PairTerminal.transform.rotation);
        }
    }

    public override void OnDeserialization()
    {
        base.OnDeserialization();

        connectedBoxTerminal = crntCatcher.GetComponent<AbstractJumpCableBoxTerminal>();

    }
}
