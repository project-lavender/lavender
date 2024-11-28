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
    //�e�L�X�g�_�C�A���O�̃y�[�W
    [SerializeField] int pageNum = 0;
    [SerializeField] List<string> dialog;
    [SerializeField] Cinemachine.CinemachineVirtualCamera vc;
    Cinemachine.CinemachinePOV pov;
    [SerializeField] Slider xslider, yslder,sslider;

    [SerializeField] DemoPlayer demoPlayer;

    [SerializeField] float voiceSpeed = 0.2f, finishWait = 2.5f;
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

    //�R���g���[���[�̃}�E�X


    //�M�~�b�N�̃C�x���g�Ăяo��
    //�ėp�̑I�����Ŏg��
    void ChosesAction(DTText dT)
    {
        //chose �� nextText������->�e�L�X�g�\���C�x���g
        Debug.Log("Choise Act ->" + dT.id);
        if (dT.nextText != "")
        {
            //�e�L�X�g
            ActiveUI(dT.nextText);
        }
        //�A�C�e������C�x���g�͂�������
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

    //�e�L�X�g�_�C�A���O�̃y�[�W����
    public void PageSend(int p)
    {
        Debug.Log("next page->" + (pageNum + p).ToString());
        if (pageNum + p < 0 || pageNum + p >= dialog.Count)
        {
            return;
        }
        pageNum += p;
        uitexts[1].text = dialog[pageNum];
        dialogPageView.text = (pageNum + 1).ToString() + "/" + dialog.Count.ToString();
    }

    //Ui�����
    public void CloseUIs()
    {
        Debug.Log("close ui");
        nowID = -1;
        //interactColider.enabled = true;
        pageNum = 0;
        SetVirtualCamera(true);
        Cursor.visible = false;
        StopAllCoroutines();
        //E = null;
        Cursor.lockState = CursorLockMode.Locked;
        foreach (RectTransform r in UIs)
        {
            r.gameObject.SetActive(false);
        }
    }
    public void ActiveUI(string textid)
    {
        //�C���^���N�g�R���C�_�[�𖳌���
        //interactColider.enabled = false;

        int i = -1;
        if (textid == "")
        {
            return;
        }
        DTText dT = dttext.Find(textid);
        //�J�[�\�����b�N���O��
        Cursor.lockState = CursorLockMode.None;
        //�J�����Œ�
        SetVirtualCamera(false);


        if (textid == "esc")
        {
            i = 3;
        }
        else if (dT != null)
        {

            i = dT.ui;
            //�O��ui�Ɠǂݍ���ui��id���Ⴄ�ꍇ�����������
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
            //�J�[�\�����b�N���O��
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //�J�����Œ�
            SetVirtualCamera(false);
            //�ėp
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
                        //�C�x���g��o�^
                        Button button = choices[c].GetComponent<Button>();
                        button.onClick.RemoveAllListeners();
                        button.onClick.AddListener(() => ChosesAction(choiseText));
                        //ui��-1�Ȃ�ui��I�����������
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
            //�e�L�X�g�_�C�A���O

            //�J�[�\�����b�N���O��

            nowID = 1;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //�J�����Œ�
            SetVirtualCamera(false);

            //�{���ɑ��
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
            //�J�����ݒ�
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            SetVirtualCamera(true);
            E = StartCoroutine(VoiceText());
        }
        else if (i == 3)
        {
            //esc
            //�ǂݍ���
            //�J�[�\�����b�N���O��
            nowID = 3;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            StreamReader rd = new(pathsence);
            string json = rd.ReadToEnd();
            rd.Close();
            sence = JsonUtility.FromJson<Sence>(json);
            //�X���C�_�[�ɔ��f
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
        if (b)
        {
            pov.m_HorizontalAxis.m_MaxSpeed = nowvec.x;
            pov.m_VerticalAxis.m_MaxSpeed = nowvec.x;
        }
        else
        {
            pov.m_HorizontalAxis.m_MaxSpeed = 0f;
            pov.m_VerticalAxis.m_MaxSpeed = 0f;
        }
    }

    public void Sound()
    {
        AudioListener.volume = sslider.value;
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
        if (action.UI.finishUI.triggered)
        {

            bool allNonActive = true;
            foreach (RectTransform rect in UIs)
            {
                Debug.Log(rect.name + rect.gameObject.activeSelf);
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
        else if (action.UI.CloseUI.triggered)
        {
            CloseUIs();
        }

        if (action.UI.NextPage.triggered)
        {
            if (nowID == 1)
            {
                PageSend(1);
            }
        }
        else if (action.UI.BeforePage.triggered)
        {
            if (nowID == 1)
            {
                PageSend(-1);
            }
        }

    }
}
