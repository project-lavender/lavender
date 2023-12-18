using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class UIcontroller : MonoBehaviour
{
    public int nowID = -1;
    [SerializeField] DT_Text dttext;
    [SerializeField] DT_Item dtitem;
    [SerializeField] ItemStack itemStack;
    [SerializeField] TextAsset playesence;
    [SerializeField] string pathsence = "Assets/Player/scripts/DataTable/PlayerSence.json";
    [SerializeField] Vector2 maxSence;
    [SerializeField] RectTransform[] UIs;
    [SerializeField] RectTransform[] choices;
    [SerializeField] TMP_Text[] uitexts;
    [SerializeField] TMP_Text dialogPageView;
    //テキストダイアログのページ
    [SerializeField] int pageNum = 0;
    [SerializeField] List<string> dialog;
    [SerializeField] Cinemachine.CinemachineVirtualCamera vc;
    Cinemachine.CinemachinePOV pov;
    [SerializeField] Slider xslider, yslder;

    [SerializeField] DemoPlayer demoPlayer;

    [SerializeField] float voiceSpeed = 0.2f,finishWait = 2.5f;
    private Lavender action;
    //
    int beforeOpenedID = -1;
    
    [System.Serializable]
    private class Sence
    {
        public float Xsence;
        public float Ysence;
    }

    [SerializeField] Sence sence;
    Vector2 nowvec;

    Coroutine E;

    //コントローラーのマウス
    

    //ギミックのイベント呼び出し
    //汎用の選択肢で使う
    void ChosesAction(DTText dT)
    {
        //chose の nextTextがある->テキスト表示イベント
        Debug.Log("Choise Act ->" + dT.id);
        if (dT.nextText != "")
        {
            //テキスト
            ActiveUI(dT.nextText);
        }
        //アイテム入手イベントはここから
        //
        //GameObject[] itemchosen = new GameObject[4];
        if (dT.choise0 != "")
        {
            itemStack.EnableItem(dT.choise0);
            demoPlayer.DemoPlay(dT.choise0);
        }
        if (dT.choise1 != "")
        {
            itemStack.EnableItem(dT.choise1);
            demoPlayer.DemoPlay(dT.choise1);
        }
        if (dT.choise2 != "")
        {
            itemStack.EnableItem(dT.choise2);
            demoPlayer.DemoPlay(dT.choise2);
        }
        if (dT.choise3 != "")
        {
            itemStack.EnableItem(dT.choise3);
            demoPlayer.DemoPlay(dT.choise3);
        }
    }

    //テキストダイアログのページ送り
    public void PageSend(int p)
    {
        Debug.Log("next page->"+(pageNum + p).ToString());
        if(pageNum+p<0 || pageNum + p >= dialog.Count)
        {
            return;
        }
        pageNum += p;
        uitexts[1].text = dialog[pageNum];
        dialogPageView.text = (pageNum + 1).ToString() + "/" + dialog.Count.ToString();
    }

    //Uiを閉じる
    public void CloseUIs()
    {
        Debug.Log("close ui");
        nowID = -1;
        //interactColider.enabled = true;
        pageNum = 0;
        SetVirtualCamera(true);
        Cursor.visible = false;
        StopAllCoroutines();
        Cursor.lockState = CursorLockMode.Locked;
        foreach (RectTransform r in UIs)
        {
            r.gameObject.SetActive(false);
        }
    }
    public void ActiveUI(string textid)
    {
        //インタラクトコライダーを無効か
        //interactColider.enabled = false;
        
        int i = -1;
        DTText dT = dttext.Find(textid);
        //カーソルロックを外す
        Cursor.lockState = CursorLockMode.None;
        //カメラ固定
        SetVirtualCamera(false);

        
        if (textid == "esc")
        {
            i = 3;
        }
        else if (dT != null)
        {
            
            i = dT.ui;
            //前のuiと読み込んだuiのidが違う場合いったん消す
            if (beforeOpenedID != i)
            {
                CloseUIs();
                beforeOpenedID = i;
            }
        }
        else
        {
            return;
        }
        Debug.Log(textid + " ui" + i.ToString());
        int j = 0;
        foreach (RectTransform r in UIs)
        {
            r.gameObject.SetActive(false);
            if (i == j)
            {
                
                r.gameObject.SetActive(true);
            }
            j += 1;
        }

        if (i == 0)
        {
            nowID = 0;
            //カーソルロックを外す
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //カメラ固定
            SetVirtualCamera(false);
            //汎用
            uitexts[i].text = dT.text;
            //Cursor.lockState = CursorLockMode.None;
            List<string> choiseID = new()
            {
                dT.choise0,
                dT.choise1,
                dT.choise2,
                dT.choise3
            };

            for (int c = 0; c < choiseID.Count; c++)
            {
                if (choiseID[c] == "")
                {
                    choices[c].gameObject.SetActive(false);
                }
                else
                {
                    
                    DTText choiseText = dttext.Find(choiseID[c]);
                    Debug.Log(choiseID[c]);
                    if (choiseText != null)
                    {
                        choices[c].gameObject.SetActive(true);
                        choices[c].GetComponentInChildren<TMP_Text>().text = choiseText.text;
                        //イベントを登録
                        Button button = choices[c].GetComponent<Button>();
                        button.onClick.RemoveAllListeners();
                        button.onClick.AddListener(() => ChosesAction(choiseText));
                        //uiが-1ならuiを選択したら閉じる
                        if (choiseText.ui == -1)
                        {
                            button.onClick.AddListener(() => CloseUIs());
                        }
                    }
                }
            }
        }
        else if (i == 1)
        {
            //テキストダイアログ

            //カーソルロックを外す

            nowID = 1;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //カメラ固定
            SetVirtualCamera(false);

            //本文に代入
            int pagenum;
            dialog = new List<string>();
            (pagenum, dialog) = dttext.Pages(textid);
            uitexts[i].text = dialog[0];
            dialogPageView.text = (pageNum + 1).ToString() + "/" + dialog.Count.ToString();

        }
        else if (i == 2)
        {
            //voice

            nowID = 2;
            if (E != null)
            {
                StopCoroutine(E);
                E = null;
            }
            int pagenum;
            dialog = new List<string>();
            (pagenum, dialog) = dttext.Pages(textid);
            //カメラ設定
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            SetVirtualCamera(true);
            E = StartCoroutine(VoiceText());
        }
        else if (i == 3)
        {
            //esc
            //読み込み
            //カーソルロックを外す
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            StreamReader rd = new(pathsence);
            string json = rd.ReadToEnd();
            rd.Close();
            sence = JsonUtility.FromJson<Sence>(json);
            //スライダーに反映
            xslider.value = sence.Xsence;
            yslder.value = sence.Ysence;
        }
        else
        {
            CloseUIs();
            return;
            
        }
    }
    public void XSence()
    {
        nowvec.x = xslider.value * maxSence.x;
    }
    public void YSence()
    {
        nowvec.y = yslder.value * maxSence.y;
    }
    public void WriteSence()
    {
        pov.m_HorizontalAxis.m_MaxSpeed = nowvec.x;
        pov.m_VerticalAxis.m_MaxSpeed = nowvec.y;
        sence.Xsence = xslider.value;
        sence.Ysence = yslder.value;
        string json = JsonUtility.ToJson(sence);
        StreamWriter wr = new(pathsence);
        Debug.Log(json);
        wr.WriteLine(json);
        wr.Close();
    }
    void SetVirtualCamera(bool b)
    {
        if (b) {
            pov.m_HorizontalAxis.m_MaxSpeed = nowvec.x;
            pov.m_VerticalAxis.m_MaxSpeed = nowvec.x;
        }
        else
        {
            pov.m_HorizontalAxis.m_MaxSpeed = 0f;
            pov.m_VerticalAxis.m_MaxSpeed = 0f;
        }
    }

    IEnumerator VoiceText()
    {
        WaitForSeconds delay;
        foreach (string d in dialog)
        {
            uitexts[2].text = d;
            int textLength = uitexts[2].text.Length;
            delay = new WaitForSeconds(voiceSpeed);
            Debug.Log(textLength);
            for (int i = 0; i <= textLength; i++)
            {
                uitexts[2].maxVisibleCharacters = i;
                yield return delay;
            }
            delay = new WaitForSeconds(finishWait);
            yield return delay;
        }
        uitexts[2].text = "";
        CloseUIs();
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pov = vc.GetCinemachineComponent<Cinemachine.CinemachinePOV>();
        sence = JsonUtility.FromJson<Sence>(playesence.text);
        nowvec.x = sence.Xsence * maxSence.x;
        nowvec.y = sence.Ysence * maxSence.y;
        pov.m_HorizontalAxis.m_MaxSpeed = nowvec.x;
        pov.m_VerticalAxis.m_MaxSpeed = nowvec.y;
        demoPlayer = FindAnyObjectByType<DemoPlayer>();
        // WriteSence();
        action = new Lavender();
        action.Enable();
    }
    // Update is called once per frame
    void Update()
    {
        //Esc
        if (action.UI.finishUI.triggered){

            bool allNonActive = true;
            foreach(RectTransform rect in UIs)
            {
                Debug.Log(rect.name+rect.gameObject.activeSelf);
                allNonActive = allNonActive && !rect.gameObject.activeSelf;
            }
            if (allNonActive)
            {
                ActiveUI("esc");
            }
            else
            {
               CloseUIs();
            }
        }
        if (action.UI.CloseUI.triggered)
        {
            CloseUIs();
        }
        if (nowID == 1)
        {
            if (action.UI.NextPage.triggered)
            {
                PageSend(1);
            }
            else if(action.UI.BeforePage.triggered)
            {
                PageSend(-1);
            }
        }
    }
}
