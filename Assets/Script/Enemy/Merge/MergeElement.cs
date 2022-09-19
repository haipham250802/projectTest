using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Spine.Unity;
public class MergeElement : ElementBase
{
    [FoldoutGroup("$Type")] public string Name;
  //  [FoldoutGroup("$Type")] public SkeletonDataAsset ICON;
    [FoldoutGroup("$Type")] public Image Avatar;

    public ElementData ThisElementData;

    private void Start()
    {
        EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
        Name = Type.ToString();
        Damage = stat.Damage;
        Rarity = stat.Rarity;
    //    ICON = stat.ICON;
        Avatar.sprite = stat.Avatar;
        SetView();
    }
    public void Init(ElementData elemendata = null)
    {
        if (elemendata != null)
        {
            Type = elemendata.Type;
            ThisElementData = elemendata;
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            HP = elemendata.HP;
            Damage = stat.Damage;
            Rarity = stat.Rarity;
         //   ICON = stat.ICON;
            Avatar.sprite = stat.Avatar;
        }
        else
        {
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            HP = stat.HP;
            Damage = stat.Damage;
            Rarity = stat.Rarity;
            //ICON = stat.ICON;
            Avatar.sprite = stat.Avatar;
        }
        SetView();
    }
    public void SetView()
    {
        if (ThisElementData != null)
            TxtHP.text = ThisElementData.HP.ToString();
        else TxtHP.text = HP.ToString();
        TxtDamage.text = Damage.ToString();
    }
    public void SetIcon(Sprite Avatar)
    {
        this.Avatar.sprite = Avatar;
    }
}
