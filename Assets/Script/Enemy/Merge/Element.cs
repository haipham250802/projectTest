using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Spine.Unity;

public class Element : ElementBase
{
    public int ID;
    public Button PurchaseBtn;
    public SkeletonDataAsset ICON;
    public Image Avatar;
    private UI_Team UITeam;

    public ElementData ThisElementData;

    bool isActiveBtnTeam, isActiveBtnMerge;
    private void Start()
    {
        UITeam = FindObjectOfType<UI_Team>();
    }
    public void activeBtnTeam()
    {
        isActiveBtnTeam = true;
        isActiveBtnMerge = false;
    }
    public void activeBtnMerge()
    {
        isActiveBtnTeam = false;
        isActiveBtnMerge = true;
    }
    public void Init(ElementData elemendata = null)
    {
        if (PurchaseBtn != null)
        {
            if (UI_Home.Instance.m_UICollection != null)
            {
                if (UI_Home.Instance.m_UICollection.IsUIteam)
                {
                    PurchaseBtn.onClick.AddListener(Load_Data_UI_Team);
                }
                else if (UI_Home.Instance.m_UICollection.IsUIMerge)
                {
                    PurchaseBtn.onClick.AddListener(LoadDataButton);
                }
            }
        }


        if (elemendata != null)
        {
            ID = elemendata.ID;
            ThisElementData = elemendata;
            Type = elemendata.Type;
            Rarity = elemendata.Rarity;
        }
        EnemyStat stat = Controller.Instance.GetStatEnemy(Type);

        Damage = stat.Damage;
        Rarity = stat.Rarity;
        ICON = stat.ICON;
        Avatar.sprite = stat.Avatar;
        SetView();
    }
    public void SetView()
    {
        if (ThisElementData != null)
        {
            TxtHP.text = ThisElementData.HP.ToString();
            HP = ThisElementData.HP;
            ID = ThisElementData.ID;
            Rarity = ThisElementData.Rarity;
        }
        else
        {
            TxtHP.text = HP.ToString();
        }
        TxtDamage.text = Damage.ToString();
    }
    void LoadDataButton()
    {
        UI_Home.Instance.m_UIMerge.ADD_SLOT(this, success =>
        {
            this.gameObject.SetActive(!success);
        });
    }
    public void Load_Data_UI_Team()
    {
        Debug.Log("da ban");
        UI_Home.Instance.m_UITeam.ADD_SLOT_ELEMENT_TEAM(this, success =>
        {
            this.gameObject.SetActive(!success);
        });
    }
    public void setIcon(Sprite Avatar)
    {
        this.Avatar.sprite = Avatar;
    }
}
