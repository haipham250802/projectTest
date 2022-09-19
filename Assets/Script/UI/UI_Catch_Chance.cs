using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;

public class UI_Catch_Chance : MonoBehaviour
{
    public Text Chance_Txt;
    public Slider Chance_Bar;
    public Image Fill_Chance_Img;
    public SkeletonGraphic Critter_Skeleton;

    public Transform Pos_Critter_Of_Catch;
    public UI_Battle m_UIBattle;

    public Color LowColor;
    public Color HightColor;

    public int JumPower;
    public int JumCount;
    public float Duration;

    public void SetTextChance(string ChanceTxt)
    {
        Chance_Txt.text = ChanceTxt;
    }
    public void SetCritterSkeleton(SkeletonDataAsset skeletonData)
    {
        this.Critter_Skeleton.skeletonDataAsset = skeletonData;
    }
    public void MoveToPosCatch()
    {
        Debug.Log("da den cho nhay !!!");
        Critter_Skeleton.transform.DOJump(Pos_Critter_Of_Catch.position, JumPower, JumCount, Duration);
    }
    public void SetValueBar(int MaxValue, int Value)
    {
        Chance_Bar.maxValue = MaxValue;
        Chance_Bar.value = Value;

        Image img = Fill_Chance_Img.GetComponent<Image>();
        img.color = Color.Lerp(LowColor, HightColor, Chance_Bar.normalizedValue);
        var tempColor = img.color;
        tempColor.a = 1f;
        img.color = tempColor;
    }
    public void ShowUICatch(UI_Catch_Chance uI_Catch_Chance)
    {
      
        uI_Catch_Chance.gameObject.SetActive(true);
    }
}
