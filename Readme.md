# RBUR-UI
## 概要
RBUR-UIはRBUR用の共通ユーザーインターフェースを実装するパッケージです。

# 目次
- [ビルドプロセス](#ビルドプロセス)
- [コンポーネント](#コンポーネント)
- [Prefab](#prefab)
# ビルドプロセス

# コンポーネント
## AV_Brake_WithHandBrake

AVブレーキ装置にGACを介して操作する手ブレーキを追加した物です。

### 仕様
AVブレーキ装置の仕様も同時に記載します。

AVブレーキ装置は補助空気溜めを用いた通常の三動弁ブレーキ装置に急動部と付加空気溜めを追加した物です。

定速減圧する独立した急動空気溜めとの差圧を用いる急動（非常ブレーキ部）を追加されており、付加空気溜めを圧力源とした非常制動が可能です。

また制動管増圧時に付加空気溜め圧が釣り合うまで補助・付加空気溜めと制動管との連絡を絶ち、独立して補助空気溜め増圧を行う事で、増圧に応じた時間だけ制動筒より排気することで、各車概ね同じだけの制動筒弛めを行う事ができます。


|英語名|日本語名|
|---:|:---|
-|作者が独自に設けた対訳です
SupportTank|補助空気溜め
AdditionalTank|付加空気溜め
EmerTank|急動空気溜め
Emer|非常(制動)
release|弛め
refill|込め
brake|制動
imme(diate)|急(制動)
straight|直通

|設定値|概要|
|---:|:---|
SupportTankSize|補助空気溜めの大きさ[$\mathrm{m^3}$]
AdditionalTankSize|付加空気溜めの大きさ[$\mathrm{m^3}$]
EmerTankSize|急動空気溜めの大きさ[$\mathrm{m^3}$]
CylinderSize|シリンダの大きさ[$\mathrm{m^3}$]
release_constriction|シリンダ弛め絞り[$\mathrm{mm^2}$]
refill_constriction|付加<->込め(補助空気溜め間)絞り[$\mathrm{mm^2}$]
brake_constriction|制動(補助空気溜め->シリンダ間)絞り[$\mathrm{mm^2}$]
SupportPressure|補助空気溜め圧[$\mathrm{MPa}$]<br>所定圧0.49[$\mathrm{MPa}$]<br>同期変数
AdditionalPressure|補助空気溜め圧[$\mathrm{MPa}$]<br>所定圧0.49[$\mathrm{MPa}$]<br>同期変数
EmerPressure|急動空気溜め圧[$\mathrm{MPa}$]<br>所定圧0.49[$\mathrm{MPa}$]、留置時は0.1推奨（読み込み時にブレーキ管圧が0だと非常制動が掛かってしまうため）<br>同期変数
CylinderPressure|制動筒圧[$\mathrm{MPa}$]<br>同期変数
piston_Position|ピストン位置。0-弛め込め,階段緩め,弛め重なり/急制動重なり,急制動,全制動重なり,全制動,非常制動-6<br>同期変数
refill_sensitivity|込め移行差圧閾値[$\mathrm{MPa}$]
release_sensitivity|弛め移行差圧閾値[$\mathrm{MPa}$]
releaseLap_sensitivity|弛め重なり移行差圧閾値[$\mathrm{MPa}$]
immeBrakeLap_sensitivity|急制動重なり移行差圧閾値[$\mathrm{MPa}$]
immeBrake_sensitivity|急制動移行差圧閾値[$\mathrm{MPa}$]
brake_sensitivity|全制動移行差圧閾値[$\mathrm{MPa}$]
emer_refill_sensitivity|急動空気溜め込め移行差圧閾値[$\mathrm{MPa}$]
emer_release_sensitivity|急動空気溜め排気移行差圧閾値[$\mathrm{MPa}$]
emer_sensitivity|非常制動移行差圧（急動空気溜め-制動管）閾値[$\mathrm{MPa}$]
StaticFriction|静止摩擦力[$\mathrm{N}$]
DynamicFriction|動摩擦力[$\mathrm{N}$]
DynamicFrictionSpeed|動摩擦移行速度[$\mathrm{m/s}$]<br>0~DynamicFrictionSpeedまで線形補間
controlledWheel|ブレーキ力の表現に使う車軸(複数化)
wheelMultiplier|ブレーキ倍率[$\mathrm{N/MPa}$]
HandBrakeScrew|手ブレーキ用ネジ。ScrewPosition(0-1)を参照
HandBrakeController|手ブレーキ用コントローラー。NormalizePosition(0-1)を参照。手ブレーキ用ネジと両方設定した場合は手ブレーキ用ネジが優先して使われる。
handBrakePower|手ブレーキ倍率。[$コントローラー入力*\mathrm{N}$]

## K_TripleValveAndFootBrake

KC/KDブレーキ装置にGACを介して操作する手ブレーキを追加した物です。

### 仕様
KC/KDブレーキ装置の仕様も同時に記載します。

KC/KDブレーキ装置は概ね補助空気溜めを用いた通常の三動弁ブレーキ装置です。

|設定値|概要|
|---:|:---|
SupportTankSize|補助空気溜めの大きさ[$\mathrm{m^3}$]
CylinderSize|シリンダの大きさ[$\mathrm{m^3}$]
SupportPressure|補助空気溜め圧[$\mathrm{MPa}$]<br>所定圧0.49[$\mathrm{MPa}$]<br>同期変数
CylinderPressure|制動筒圧[$\mathrm{MPa}$]<br>同期変数
piston_Position|ピストン位置。0-減速弛め/込め,全弛め込め,急制動重なり,急制動,全制動重なり,全制動,非常制動-6<br>同期変数
slowRefill_sensitivity|減速込め移行差圧閾値[$\mathrm{MPa}$]
slowRelease_sensitivity|減速弛め移行差圧閾値[$\mathrm{MPa}$]
release_sensitivity|全弛め込め移行差圧閾値[$\mathrm{MPa}$]
immediateBrake_lap_sensitivity|急制動重なり移行差圧閾値[$\mathrm{MPa}$]
immediateBrake_sensitivity|急制動移行差圧閾値[$\mathrm{MPa}$]
brake_sensitivity|全制動移行差圧閾値[$\mathrm{MPa}$]
emer_sensitivity|非常制動移行差圧閾値[$\mathrm{MPa}$]
emer_sensitivity_2|非常制動局部減圧の参照圧[$\mathrm{MPa}$]
StaticFriction|静止摩擦力[$\mathrm{N}$]
DynamicFriction|動摩擦力[$\mathrm{N}$]
DynamicFrictionSpeed|動摩擦移行速度[$\mathrm{m/s}$]<br>0~DynamicFrictionSpeedまで線形補間
controlledWheel|ブレーキ力の表現に使う車軸(複数化)
wheelMultiplier|ブレーキ倍率[$\mathrm{N/MPa}$]
HandBrakeScrew|手ブレーキ用ネジ。ScrewPosition(0-1)を参照
HandBrakeController|手ブレーキ用コントローラー。NormalizePosition(0-1)を参照。手ブレーキ用ネジと両方設定した場合は手ブレーキ用ネジが優先して使われる。
handBrakePower|手ブレーキ倍率。[$コントローラー入力*\mathrm{N}$]

## BrakerGrapHandle

貨車用の駐車ブレーキとして、側面を掴みながら上がり下がりすることでブレーキするためのGAC拡張スクリプトです。

### 仕様

ApplyToTransformにおいてStationを移動し、コントローラールートを逆に移動する事で自然な操作を実現しています。leverTransformに設定したオブジェクトを回転させることで参照も可能です。
GACの詳細はGACパッケージを参照して下さい。
|設定値|概要|
|---:|:---|
stationTransform|VRCStationのTransformです。
indicateLeverTransform|表示用オブジェクトです、X軸を回転させて掛かり具合を表します。
indicateLeverLength|表示用オブジェクトの長さです。Station位置までの水平距離を設定すると正確に降ろした量を表現できるようになります。
station|連動するStationです。


|関数|概要|同期|
|---:|:---|:---|
|

## BrakeValve_GACLink

GACを介してブレーキパイプを開閉するための拡張スクリプトです。
RBUR.BrakeConnectorValveの内容を合わせて確認して下さい。

### 仕様
|設定値|概要|
|---:|:---|
GAC_Controller|開閉に用いるコントローラー
Open_Position|開時の位置
Close_Position|閉時の位置


|関数|概要|同期|
|---:|:---|:---|
GAC_OpenPos|GACのイベント呼び出し機能で呼ぶ想定の、コック開時イベント受けです。|Owner側イベントを呼び出し、Ownerから同期
GAC_ClosePos|GACのイベント呼び出し機能で呼ぶ想定の、コック閉時イベント受けです。|Owner側イベントを呼び出し、Ownerから同期

## EmerValve
GACを介してBP管開放による非常制動を行うスクリプトです。

同期をGACに任せ、有効無効切り替えでUpdateを起動/停止しています。

### 仕様
|設定値|概要|
|---:|:---|
EmerValveController|開閉に用いるコントローラー<br>segment=0:閉鎖、segment=1:開放
brakeModule|制御の対象になるブレーキ装置


|関数|概要|同期|
|---:|:---|:---|
OnDrop_|GACを手から落とした際のイベント。復位に使用。|GAC側が実施
EmerValveClose|閉鎖時イベント|GAC側が実施
EmerValveOpen|解放時イベント|GAC側が実施

## CouplerEventListener_ObjectToggle

Couplerからのイベントを受けて、UtilのToggleを用いてモデル表示を行うイベントリスナー実装です。
CouplerEventListenersに設定して用います。

### 仕様
SyncedObject~を用いますが、SyncmodeをNoneとしてローカル動作としています。同期はCouplerObj側が担当しています。

|設定値|概要|
|---:|:---|
ObjectToggle_Knuckle|ナックル開閉用のToggleです。
SyncedObjectSwitch|錠状態の表示用スイッチです。

# Prefab 

## GAC_BrakeConnectorValve

GACを用いてブレーキコックを操作するためのPrefabです。

### 仕様
Coupler下に入れる事でビルド時に各種参照が自動構築されます。

## PassCarController

客車向けのコントローラー群です。

### 仕様
TrainのGameObject下に入れることで、ビルド時に一部参照が自動構築されます。
ControlledWheelとWheelMultiplierは自身で設定して下さい。

## Uncoupler

GACを用いたカプラーの開放装置です。

### 仕様

Controller_PickupのEventReceiversにCouplerObjを設定することで、イベントを呼び出し動作します。