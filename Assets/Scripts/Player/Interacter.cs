using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//�M�~�b�N�ɃA�N�Z�X���@UI�R���g���[���ɏ��𑗐M
public class Interacter : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float interactCD = 1f, scanlength = 0.8f;
    [SerializeField] bool canTouch = true;
    [SerializeField] UIcontroller uictr;
    [SerializeField] ItemStack itemStack;
    [SerializeField] DT_Items dtitem;
    [SerializeField] DemoPlayer demoPlayer;
    [SerializeField] Gimicks gimicks = null;
    [SerializeField] UIcontroller uic;
    private Lavender action;


    // Start is called before the first frame update
    RaycastHit hit;
    void Start()
    {
        //�R�[���o�b�N�ݒ�
        action = new Lavender();
        action.Enable();
        //demoPlayer = FindAnyObjectByType<DemoPlayer>();
    }
    IEnumerator SetCanTouch()
    {
        canTouch = false;
        yield return new WaitForSeconds(interactCD);
        canTouch = true;
    }
    // Update is called once per frame
    void Update()
    {
        //�G�����M�~�b�N�ɑ΂��āA�F��ύX������

        Ray ray = new();
        ray.origin = cam.position;
        ray.direction = cam.forward;
        Debug.DrawRay(cam.position, cam.forward * 0.8f);
        //uic.nowID != 0 && uic.nowID != 1 && uic.nowID != 3 && Physics.Raycast(ray, out hit, 0.8f) && hit.collider.CompareTag("Gimick")
        if ((uic.nowID == -1 || uic.nowID == 2) && Physics.Raycast(ray, out hit, scanlength) && hit.collider.CompareTag("Gimick"))
        {

            gimicks = hit.collider.GetComponent<Gimicks>();
            //�����\��
            gimicks.EmitColor();
        }
        else if (gimicks != null)
        {
            //�M�~�b�N���痣�ꂽ��I�t
            gimicks.TurnOffColor();
            gimicks = null;
        }
    }
    //�M�~�b�N�ɃA�N�Z�X
    public void OnFire(InputAction.CallbackContext context)
    {
        // && canTouch && gimicks != null
        Debug.Log("Fire");
        if (context.performed && gimicks != null)
        {
            StartCoroutine(SetCanTouch());
            GimickStructure gimickData;
            gimickData = gimicks.ReturnGimickInfo();
            if (gimickData != null)
            {
                uictr.ActiveUI(gimickData.textID);
                itemStack.EnableItem(gimickData.itemID);
                itemStack.DisableItem(gimickData.downFrag);
                demoPlayer.DemoPlay(gimickData.demoID);
            }
        }
    }
}
