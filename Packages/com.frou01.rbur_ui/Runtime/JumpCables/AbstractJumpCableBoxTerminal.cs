
using frou01.RigidBodyTrain;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]


public class AbstractJumpCableBoxTerminal : LUP_RC_CatcherCollider
{
    [Tooltip("foward = +Z")]
    [SerializeField] bool isTrainForward;
    [SerializeField] Train train;
    AbstractJumpCableWireTerminal connectedWire;
    public override bool isCatching()
    {
        //TODO
        //pickupの状態を確認し、参照に齟齬があればPickup優先。
        //車両側連結が済んでいなければ接続しない（距離監視処理を全部に入れるのは現実的でないため）
        //未接続状態であれば接続を実施
        return true;
    }

    public virtual void TrainConnectionUpdate(Train connectedTrain, bool F_B)
    {
        //TODO
        //列車が解結した際は接続を解除する
    }
    public override void PickupEnter(LUPickUpRC_RootChangeable pickup)
    {
        //TODO
        //pickup.Owner == Localの場合のみ処理を実行
        //自身のOwnerをpickupのOwnerへ変更、LUP_RCのIDで各クライアントの参照を揃える
    }
    public override void PickupExit(LUPickUpRC_RootChangeable pickup)
    {
    }
    //TODO
    //Ondeserialize・LocalPickupのEnter/Exitで参照変更を反映
    //Hop数を任意数に制限し、前後に探索
    
}
