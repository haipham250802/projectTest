using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
using Spine;

public class AllidBase : MonoBehaviour
{
    const string AttackName = "Attack";
    const string IdleName = "Idle";
    const string Move = "Move";

    [FoldoutGroup("$Type")] public ECharacterType Type;
    [FoldoutGroup("$Type")] public int Damage;
    [FoldoutGroup("$Type")] public int Speed;
    [FoldoutGroup("$Type")] public int HP;
    [FoldoutGroup("$Type")] public int CurHP;
    [FoldoutGroup("$Type")] public int ID;
    [FoldoutGroup("$Type")] public SkeletonDataAsset ICON;

    public int JumpCount;
    public float Duration;

    private Vector3 StartPos;
    public Vector3 Offset;

    private bool isTurnDone = false;
    public List<AllidELement> L_AllidElement = new List<AllidELement>();
    public UI_Battle m_UIBattle;

    public bool isTurnEnemy = false;

    public SkeletonGraphic skeletonGraphic;
    private System.Action EventCallbackAnimationComplete;
    private void Start()
    {
        StartPos = transform.position;
    }
    public void init(AllidELement allidELement, ElementData elementData)
    {
        Damage = allidELement.Damage;
        HP = elementData.HP;
        Type = elementData.Type;
        ID = elementData.ID;
    }
    public void PlayAnimation(string _animationName, bool _loop, System.Action _animationCallback)
    {
        EventCallbackAnimationComplete = null;
        EventCallbackAnimationComplete = _animationCallback;
        skeletonGraphic.AnimationState.Complete += Animation_Oncomplete;
        skeletonGraphic.AnimationState.SetAnimation(0, _animationName, _loop);
    }
    private void Animation_Oncomplete(TrackEntry trackEntry)
    {
        skeletonGraphic.AnimationState.Complete -= Animation_Oncomplete;
        EventCallbackAnimationComplete?.Invoke();
    }
    private void SetData(SkeletonDataAsset skeleton)
    {
        skeletonGraphic.skeletonDataAsset = null;
        skeletonGraphic.skeletonDataAsset = skeleton;
        skeletonGraphic.Initialize(true);
    }
    public void Attack()
    {
        if (!isTurnDone)
        {
            if (m_UIBattle.enemyBased != null)
            {
                transform.DOJump(m_UIBattle.AllidMoveToPosEnemyBase.position, Speed, JumpCount, Duration).OnComplete(() =>
                {
                    StartCoroutine(IE_AnimAttackEnemyBased());
                });
            }
        }
    }

    public EnemyBased m_EnemyBased;

    public void Do_Something_When_Animation_Attack_EnemyBased_Complete()
    {
        m_UIBattle.TakeDamageEnemy(Damage, m_UIBattle.enemyBased.GetComponent<EnemyBased>());
        m_UIBattle.UpdateViewEnemyBaseElement();
        transform.DOMove(StartPos, Speed * 2).SetSpeedBased(true).OnComplete(() =>
        {
            skeletonGraphic.AnimationState.SetAnimation(0, IdleName, true);
            isTurnDone = false;

            if (m_UIBattle.enemyBased.Type == m_UIBattle.L_EnemyBaseElement[m_UIBattle.L_EnemyBaseElement.Count - 1].Type
            && m_UIBattle.enemyBased.ID == m_UIBattle.L_EnemyBaseElement[m_UIBattle.L_EnemyBaseElement.Count - 1].ID)
            {
                if (m_UIBattle.enemyBased.HP < 1)
                {
                    m_EnemyBased = m_UIBattle.enemyBased;
                    m_UIBattle.enemyBased.JumpPower = 0;
                    m_UIBattle.GameWin();
                    if (m_UIBattle.isPartEnemy)// da giet chet con cuoi
                    {
                        m_UIBattle.LoadCatchChance(m_UIBattle.enemyBased);
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(m_UIBattle.enemyBased.Type);

                        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%"); // set text ti le

                        Debug.Log("ti le: " + m_UIBattle.Catch_Chance);
                        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetValueBar(100, m_UIBattle.Catch_Chance); // set gia tri slider
                        Debug.Log("da den !");
                        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetCritterSkeleton(enemyStat.ICON);
                        m_UIBattle.PopUpVictory.SetActive(true); // hien thi popUp victory
                        // tap skip trong event button
                    }

                    return;
                }
            }
            isTurnEnemy = true;
            m_UIBattle.switchEnemy();
            m_UIBattle.enemyBased.Attack();
        });
    }
    public void ShowPopUpUICatch()
    {
        m_UIBattle.PopUpVictory.GetComponent<PopUp_Victory>().IsTap = true;
        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().ShowUICatch(m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>());
    }
    IEnumerator IE_MoveToPosCatch(string ChanceTxt, SkeletonDataAsset skeletonData)
    {
        yield return null;
        Debug.Log("da k");
    }
    IEnumerator IE_AnimAttackEnemyBased()
    {
        yield return null;
        PlayAnimation(AttackName, false, Do_Something_When_Animation_Attack_EnemyBased_Complete);
    }
    public bool GetTurnEnemyBased()
    {
        return isTurnEnemy;
    }
    public void SetTurnEnemyBased(bool isTurnEnemy)
    {
        this.isTurnEnemy = isTurnEnemy;
    }
    public bool GetIsTurnAllid()
    {
        return isTurnDone;
    }
    public void SetIsTurnAllid(bool IsTurn)
    {
        isTurnDone = IsTurn;
    }
    public bool GetIsTurnEnemy()
    {
        return isTurnEnemy;
    }
    public void SetIsTurnEnemy(bool _IsTurnEnemy)
    {
        isTurnEnemy = _IsTurnEnemy;
    }
}
