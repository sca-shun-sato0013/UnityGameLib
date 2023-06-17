using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using CommonlyUsed;
using UnityEngine.UI;

public class SetDisplayImage : MonoBehaviour,IUpdateManager
{
    [SerializeField, Header("���͒[��")]
    InputType inputType;

    [SerializeField, Header("Addressables�̓ǂݍ��݃t�H���_��path")]
    string pathName;

    [SerializeField, Header("�\��������摜��")]
    Image[] images;

    [SerializeField, Header("�摜�̃t�@�C�����擾")]
    string[] iDatas;

    [SerializeField, Header("�L�����̕\���A��\��")]
    Image charaImage;
    [SerializeField, Header("�V�i���I�w�i")]
    Image backGroundImage;
    [SerializeField]
    ImageTransparencyAnimation charaAnimation;
    [SerializeField]
    ScenarioManager scenarioManager;

    bool fadeCheck = false;

    string past  = "";
    
    WaitForSeconds w2;
        

    public string[] ImageDatas
    {
        get { return iDatas; }
        internal set { iDatas = value; }
    }

    public bool FadeCheck { 
        
        get => fadeCheck; 
        set => fadeCheck = value; 
    }

    void Start()
    {
        w2 = new WaitForSeconds(0.2f);
        iDatas = new string[images.Length];
        UpdateManager.Instance.Bind(this, FrameControl.ON);
    }

    public void OnUpdate(double deltaTime)
    {
        if (backGroundImage.sprite.name == "Black(Clone)")fadeCheck = true;
        
        if (scenarioManager.LoadCheck)
        {
            scenarioManager.LoadCheck = false;
            
            for (int i = 0; i < images.Length; i++)
            {
                if (iDatas[i] != "" && iDatas[i] != "NONE")
                {
                    if (iDatas[i] != past)
                    {
                        charaAnimation.enabled = false;
                        ImageLoading.ImageLoadingAsync(images[i], StringComponent.AddString(pathName, iDatas[i]));
                        past = iDatas[i];
                    }
                }

                if (iDatas[i] == "NONE")
                {
                    charaAnimation.enabled = false;
                }
                else
                {
                    StartCoroutine(WaitLoad());
                }
            }
        }
    }

    private IEnumerator WaitLoad()
    {
        string s = iDatas[1] + "(Clone)";

        yield return w2;

        if (charaImage.sprite.name == s.Replace(".png", ""))
        {
            charaAnimation.enabled = false;
            charaAnimation.enabled = true;
        }
    }
}