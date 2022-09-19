using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Spine.Unity;
public class EnemyBaseElement : MonoBehaviour
{
    [FoldoutGroup("$Type")] public ECharacterType Type;
    [FoldoutGroup("$Type")] public TypeEnemy TypeEnemy;
    [FoldoutGroup("$Type")] public int Damage;
    [FoldoutGroup("$Type")] public int HP;
    [FoldoutGroup("$Type")] public int Rarity;
    [FoldoutGroup("$Type")] public Text TxtHP;
    [FoldoutGroup("$Type")] public Text TxtDamage;
    [FoldoutGroup("$Type")] public int ID;
    [FoldoutGroup("$Type")] public SkeletonDataAsset ICON;
    [FoldoutGroup("$Type")] public Image Avatar;
    public ElementData ThisElementData;

    private void Start()
    {
        Init();
    }

    public void Init(ElementData elemendata = null)
    {
        // PurchaseBtn.onClick.AddListener(Load_Data_UI_Team);
        if (elemendata != null)
        {
            ID = elemendata.ID;
            HP = elemendata.HP;
            ThisElementData = elemendata;
            Type = elemendata.Type;
        }
        if (TypeEnemy == TypeEnemy.Soldier)
        {
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            Damage = stat.Damage;
            Rarity = stat.Rarity;
            ICON = stat.ICON;
            Avatar.sprite = stat.Avatar;
            HP = stat.HP;
        }
        else if(TypeEnemy == TypeEnemy.Boss)
        {
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            Damage = stat.EnocunterATK;
            Rarity = stat.Rarity;
            ICON = stat.ICON;
            Avatar.sprite = stat.Avatar;
            HP = stat.EncounterHP;
        }

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
    public void UpdateView(EnemyBased enemyBased)
    {
        this.TxtHP.text = enemyBased.HP.ToString();
    }
}
