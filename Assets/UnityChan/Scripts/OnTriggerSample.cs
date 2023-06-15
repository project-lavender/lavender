using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerSample : MonoBehaviour
{
    //OnTriggerEnter関数
    //接触したオブジェクトが引数otherとして渡される
    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if(other.CompareTag("Enemy")){
            //オブジェクトの色を赤に変更する
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
