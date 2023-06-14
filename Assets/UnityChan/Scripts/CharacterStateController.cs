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
            })
            .AddTo(this);
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal") && Input.GetButton("Fire3")) {
            StateProcessor.State.Value = StateRun;
        } else if ( Input.GetButton("Horizontal")) {
            StateProcessor.State.Value = StateWalk;
        } else {
            StateProcessor.State.Value = StateIdle;
        }
    }

    public void Idle()
    {
        Debug.Log("StateがIdleに状態遷移しました。");
        CollisionRange = 0f;
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


    void OnDrawGizmosSelected()
    {
        //CollisionRangeの範囲を赤いワイヤーフレームで示す
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CollisionRange);
    }
}
