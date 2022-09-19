using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Spine.Unity;
public class AllidELement : MonoBehaviour
{
    [FoldoutGroup("$Type")] public ECharacterType Type;
    [FoldoutGroup("$Type")] public int Damage;
    [FoldoutGroup("$Type")] public int HP;
    [FoldoutGroup("$Type")] public int Rarity;
    [FoldoutGroup("$Type")] public Text TxtHP;
    [FoldoutGroup("$Type")] public Text TxtDamage;
    [FoldoutGroup("$Type")] public int Speed;
    [FoldoutGroup("$Type")] public int ID;

    public Button PurchaseBtn;
    public SkeletonDataAsset ICON;
    public Image Avatar;

    public ElementData ThisElementData;
    AllidBase m_allidBase;
    EnemyBased m_enemy;
    Enemy enemy;

    private void Start()
    {
        /* m_enemy = FindObjectOfType<EnemyBased>();
         enemy = FindObjectOfType<Enemy>();*/
    }
    private void Update()
    {
        
    }
    public void Init(ElementData elemendata = null)
    {
        /*        if (PurchaseBtn != null)
                    // PurchaseBtn.onClick.AddListener(Load_Data_UI_Team);*/
        if (elemendata != null)
        {
            ID = elemendata.ID;
            HP = elemendata.HP;
            ThisElementData = elemendata;
            Type = elemendata.Type;
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
        }
        else
        {
            TxtHP.text = HP.ToString();
        }
        TxtDamage.text = Damage.ToString();
    }
    /* public void SpawnAllidBased()
     {
         PurchaseBtn.enabled = false;
         *//* if (UI_Home.Instance.m_UIBattle != null)
          {
              UI_Home.Instance.m_UIBattle.SpawnAllidBased(this.ICON.sprite);
          }*//*
         if (enemy.UIBattle != null)
         {
             enemy.UIBattle.GetComponent<UI_Battle>().SpawnAllidBased(this.ICON.sprite);
         }
         m_allidBase = FindObjectOfType<AllidBase>();
         m_allidBase.init(this, this.ThisElementData);

         m_allidBase.Attack(Speed, PurchaseBtn);
         //    TakeDamage(1, ThisElementData);
         CharacterManager.Instance.initListAllid(m_allidBase);
     }*/
    public void ActiveButton()
    {
        PurchaseBtn.enabled = true;
    }
}
