using UnityEngine;
using CharacterState;
using UniRx;

public class CharacterStateController : MonoBehaviour
{
    //変更前のステート名
    private string _prevStateName;

    //ステート
    public StateProcessor StateProcessor { get; set; } = new StateProcessor();
    public CharacterStateIdle StateIdle { get; set; } = new CharacterStateIdle();
    public CharacterStateRun StateRun { get; set; } = new CharacterStateRun();
    public CharacterStateWalk StateWalk { get; set; } = new CharacterStateWalk();

    //音のなる範囲
    [SerializeField] float CollisionRange;

    private void Start()
    {
        StateProcessor.State.Value = StateIdle;
        StateIdle.ExecAction = Idle;
        StateRun.ExecAction = Run;
        StateWalk.ExecAction = Walk;

        //ステートの値が変更されたら実行処理を行うようにする
        StateProcessor.State
            .Where(_ => StateProcessor.State.Value.GetStateName() != _prevStateName)
            .Subscribe(_ =>
            {
                Debug.Log("Now State:" + StateProcessor.State.Value.GetStateName());
                _prevStateName = StateProcessor.State.Value.GetStateName();
                StateProcessor.Execute();
                ChangePlayerCollisionScale();
            })
            .AddTo(this);
    }

    private void Update()
    {
        if (Input.GetButton("Vertical") && Input.GetButton("Fire3")) {
            StateProcessor.State.Value = StateRun;
        } else if ( Input.GetButton("Vertical")) {
            StateProcessor.State.Value = StateWalk;
        } else {
            StateProcessor.State.Value = StateIdle;
        }
    }

    public void Idle()
    {
        Debug.Log("StateがIdleに状態遷移しました。");
        CollisionRange = 1f;
        Debug.Log(CollisionRange);
    }

    public void Run()
    {
        Debug.Log("StateがRunに状態遷移しました。");
        CollisionRange = 5f;
        Debug.Log(CollisionRange);
    }

    public void Walk()
    {
        Debug.Log("StateがWalkに状態遷移しました。");
        CollisionRange = 3f;
        Debug.Log(CollisionRange);
    }

    void ChangePlayerCollisionScale() {
        Transform PlayerCollision = GameObject.FindWithTag("PlayerCollision").GetComponent<Transform>();
        Debug.Log(PlayerCollision.localScale);
        Vector3 localScale = PlayerCollision.localScale;
        localScale.x = CollisionRange; // ローカル座標を基準にした、x軸方向へ2倍のサイズ変更
        localScale.y = CollisionRange; // ローカル座標を基準にした、y軸方向へ2倍のサイズ変更
        localScale.z = CollisionRange; // ローカル座標を基準にした、z軸方向へ2倍のサイズ変更
        PlayerCollision.localScale = localScale;
    }
}
