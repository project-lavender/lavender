using System;
using UnityEngine;
using UniRx;

namespace CharacterState
{
    //ステートの実行を管理するクラス
    public class StateProcessor
    {
        public ReactiveProperty<CharacterState> State { get; set; } = new ReactiveProperty<CharacterState>();

        public void Execute() => State.Value.Execute();
    }

    //ステートのクラス
    public abstract class CharacterState
    {
        //デリゲート
        public Action ExecAction { get; set; }

        //実行処理
        public virtual void Execute()
        {
            if (ExecAction != null) ExecAction();
        }

        //ステートを取得する関数
        public abstract string GetStateName();
    }

    //何もしていない状態
    public class CharacterStateIdle : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Idle";
        }
    }

    //走っている状態
    public class CharacterStateRun : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Run";
        }
 
        public override void Execute()
        {
            Debug.Log("なにか特別な処理をしたいときは派生クラスにて処理をしても良い");
            if (ExecAction != null) ExecAction();
        }
    }

    //歩いている状態
    public class CharacterStateWalk : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Walk";
        }
    }
}
