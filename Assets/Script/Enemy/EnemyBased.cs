using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
using Spine;

public class EnemyBased : MonoBehaviour
{
    const string attackName = "Attack";
    const string MoveName = "Move";
    const string IdleName = "Idle";

    public int Speed;

    public float JumpPower;
    public int JumCount;
    public float Duration;
    public int HP;
    public int Damage;
    public int Rarity;
    public int ID;

    public ECharacterType Type;
    public UI_Battle m_UIBattle;


    private bool isTurnPlayer = false;

    public Transform StartPos;

    StateEnemyBased _StateEnemy;

    public SkeletonGraphic skeletonGraphic;
    private System.Action EventCallbackAnimationComplete;

    Tweener tweener;
    private void Start()
    {
        CharacterManager.Instance.AddListEnemyBased(this);
        //  skeletonGraphic.AnimationState.SetAnimation(0, IdleName, true);
        //    skeletonGraphic.AnimationState.Complete += AnimationState_Complete;
    }
    /* private void OnDisable()
     {
         if (skeletonGraphic != null)
             skeletonGraphic.AnimationState.Complete -= AnimationState_Complete;
     }*/

    public bool IsMoveToPosCatched = false;
    public void MoveToPosCatchedCritter()
    {
        transform.DOMove(m_UIBattle.PosEnemyWhenPartEnemy.transform.position, Speed).SetSpeedBased(true).OnComplete(() =>
        {
          //  IsMoveToPosCatched = true;
            m_UIBattle.CatchEnemy(this);
        });
    }
    public void PlayAnimation(string _animationName, bool _Loop, System.Action _animationCallback)
    {
        EventCallbackAnimationComplete = null;
        skeletonGraphic.AnimationState.SetAnimation(0, _animationName, _Loop);

        if (_animationCallback == null) return;
        if (skeletonGraphic.AnimationState.GetCurrent(0).Animation.Name == attackName)
        {
            EventCallbackAnimationComplete = _animationCallback;
            skeletonGraphic.AnimationState.Complete += AnimationState_Complete;
        }
        else
        {
            skeletonGraphic.AnimationState.Complete -= AnimationState_Complete;
        }

    }
    public void AnimationState_Complete(TrackEntry _trackEntry)
    {
        if (_trackEntry.Animation.Name == attackName)
        {
            EventCallbackAnimationComplete?.Invoke();
            skeletonGraphic.AnimationState.Complete -= AnimationState_Complete;
        }
    }
    public void SetData(SkeletonDataAsset _data)
    {
        skeletonGraphic.skeletonDataAsset = null;
        skeletonGraphic.skeletonDataAsset = _data;
        skeletonGraphic.Initialize(true);
    }
    public void init(EnemyBaseElement enemyBaseElement)
    {
        Type = enemyBaseElement.Type;
        Damage = enemyBaseElement.Damage;
        HP = enemyBaseElement.HP;
        ID = enemyBaseElement.ID;
        Rarity = enemyBaseElement.Rarity;
    }
    private void Update()
    {
        if (HP < 1)
        {
            HP = 0;
        }

    }
    public void UpdateState()
    {
        switch (_StateEnemy)
        {
            case StateEnemyBased.IDLE:
                if (m_UIBattle.AllidBase != null)
                {
                    if (m_UIBattle.AllidBase.GetComponent<AllidBase>().GetTurnEnemyBased())
                    {
                        _StateEnemy = StateEnemyBased.ATTACK;
                    }
                }
                break;
            case StateEnemyBased.ATTACK:
                Attack();
                break;
        }
    }
    private void KillTweener()
    {
        if (tweener == null)
            return;
        tweener.Kill();
        tweener = null;
    }
    public void Attack()
    {
        if (m_UIBattle.AllidBase != null)
        {
            KillTweener();
            /*   transform.DOMove(m_UIBattle.EnemyMoveToPosAllidBase.position, Speed).SetSpeedBased(true).OnComplete(() =>
                {

                });
   */
            transform.DOJump(m_UIBattle.EnemyMoveToPosAllidBase.position, JumpPower, JumCount, Duration)
                .OnComplete(() =>
            {

                //PlayAnimation(attackName, false, DoSomthingWhenAttack);
                StartCoroutine(IE_PlayeAnimationAttack());
            });
            m_UIBattle.AllidBase.GetComponent<AllidBase>().SetIsTurnEnemy(false);
        }
        /*  if (_StateEnemy == StateEnemyBased.ATTACK)*/
        //  _StateEnemy = StateEnemyBased.IDLE;
        //  UpdateState();
    }
    public void DoSomthingWhenAttack()
    {
        KillTweener();
        m_UIBattle.TakeDamageAllid(Damage, m_UIBattle.AllidBase.GetComponent<AllidBase>());
        m_UIBattle.GameOver();
        transform.DOMove(StartPos.position, JumpPower * 2.2f).SetSpeedBased(true).OnComplete(() =>
        {
            isTurnPlayer = true;
            isButton = true;
            m_UIBattle.EnableButton();
            StartCoroutine(IE_PlayAnimationIdle());
            m_UIBattle.ChangeAllidBase(m_UIBattle.AllidBase.GetComponent<AllidBase>());

        });
        //StartCoroutine(WaitMoveToStartPos());
    }
    IEnumerator IE_PlayAnimationIdle()
    {
        yield return null;
        skeletonGraphic.AnimationState.SetAnimation(0, IdleName, true);
    }
    IEnumerator IE_PlayeAnimationAttack()
    {
        yield return null;
        PlayAnimation(attackName, false, DoSomthingWhenAttack);
    }
    bool isButton = false;
    public void SetTurnPlayerBased(bool isTurnPlayer)
    {
        this.isTurnPlayer = isTurnPlayer;
    }
    public bool GetTurnPlayerBased()
    {
        return isTurnPlayer;
    }
    public void SetBtn(bool isBtn)
    {
        isButton = isBtn;
    }
    public bool IsBtn()
    {
        return isButton;
    }


}
public enum StateEnemyBased
{
    IDLE,
    ATTACK
}
