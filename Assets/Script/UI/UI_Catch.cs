using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;
public class UI_Catch : MonoBehaviour
{
    public Image UFO;
    public SkeletonGraphic Critter;
    public Transform Pos_UFO_Catch;
    public Transform Pos_Critter_Suck;
    public Transform StartPosCritter;

    public GameObject PopUpCatch;
    public UI_Battle m_UIBattle;

    public int Speed;

    public void FallPosCritter()
    {
        Debug.Log("da false");
        Critter.transform.DOMove(StartPosCritter.position, Speed * 0.8f).SetSpeedBased(true);/*.SetSpeedBased(true).OnComplete(() =>*/
        m_UIBattle.Catch_Chance_Plus += 30;
        m_UIBattle.LoadCatchChance(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased);
    }

    public void MovePosCritterSuck()
    {
        UFO.transform.DOMove(Pos_UFO_Catch.position, Speed * 0.5f).SetSpeedBased(true).OnComplete(() =>
        {
            Critter.transform.DOMove(Pos_Critter_Suck.position, Speed).SetSpeedBased(true).OnComplete(() =>
            {
                Catch();
            });
        });
    }
    public void BtnPlusChanceAds()
    {
        m_UIBattle.Catch_Chance_Plus += 5;
        if(m_UIBattle.Catch_Chance_Plus > 100)
        {
            m_UIBattle.Catch_Chance_Plus = 100;
        }
        m_UIBattle.LoadCatchChance(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased);
        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%");
    }
    public void BtnPlusChanceCoin()
    {
        m_UIBattle.Catch_Chance_Plus += 10;
        if (m_UIBattle.Catch_Chance_Plus > 100)
        {
            m_UIBattle.Catch_Chance_Plus = 100;
        }
        m_UIBattle.LoadCatchChance(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased);
        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%");
    }
    public void BtnPlusChanceGem()
    {
        m_UIBattle.Catch_Chance_Plus += 30;
        if (m_UIBattle.Catch_Chance_Plus > 100)
        {
            m_UIBattle.Catch_Chance_Plus = 100;
        }
        m_UIBattle.LoadCatchChance(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased);
        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%");
    }
    public void Catch()
    {
        m_UIBattle.CatchEnemy(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased);
    }

    public void SetSkeletonCritter()
    {
        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased.Type);
        Critter.skeletonDataAsset = null;
        this.Critter.skeletonDataAsset = enemyStat.ICON;
        Critter.Initialize(true);
        Critter.AnimationState.SetAnimation(0, "Idle", true);
    }
}
