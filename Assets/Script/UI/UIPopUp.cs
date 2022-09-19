using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Spine.Unity;
public class UIPopUp : MonoBehaviour
{
    public Button TabToSkipBtn;
    public GameObject UIPopUpSuccess;
   // public SkeletonDataAsset Icon;
    public Image Avatar;
    public Text NameTxt;
    public Text RarityTxt;
    public Text HpTxt;
    public Text DamageTxt;

    public void Init(MergeElement mergeElement)
    {
        //Icon = mergeElement.ICON;
        Avatar.sprite = mergeElement.Avatar.sprite;
        NameTxt.text = mergeElement.Type.ToString();
        RarityTxt.text = mergeElement.Rarity.ToString();
        HpTxt.text = mergeElement.HP.ToString();
        DamageTxt.text = mergeElement.Damage.ToString();
    }
    private void Awake()
    {
        TabToSkipBtn.onClick.AddListener(OnTabToSkipBtn);
    }
    private void Start()
    {
        UIPopUpSuccess.SetActive(false);
    }
    void OnTabToSkipBtn()
    {
        UIPopUpSuccess.SetActive(false);
    }
    public void _ShowPopUpSuccess(MergeElement mergeElement)
    {
        UIPopUpSuccess.SetActive(true);
        Init(mergeElement);
    }

}
