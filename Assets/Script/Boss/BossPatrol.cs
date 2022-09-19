using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossPatrol : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 PatrolPos;
    StateEnemy stateEnemy;

    public float SpeedPatrol;
    public float SpeedChasing;
    public float RangePatrol = 2.0f;
    public float RangeChasing;

    public GameObject UIBattle;

    player m_player;
    Tweener tweener;

    // public GameObject GroupEnemyBased;
    public GameObject TeamEnemy;
    public List<EnemyBased> L_EnemyBased;
    public List<GameObject> L_Enemy;

    /*    public Vector2 PosGroupEnemyBased;
        public Transform PosEnemyBased;*/

    public bool isTrigger = false;
    void Start()
    {
        startPos = transform.position;
        PatrolPos = startPos;
        stateEnemy = StateEnemy.PATROL;
        m_player = FindObjectOfType<player>();
        CharacterManager.Instance.L_Enemy.Add(this.gameObject);
        /*  for(int i = 0; i < L_EnemyBased.Count; i++)
          {
              L_EnemyBased[i].gameObject.SetActive(false);
          }*/
    }
    bool isPatrol = false;
    private void Update()
    {
        switch (stateEnemy)
        {
            case StateEnemy.PATROL:
                if (Vector2.Distance(transform.position, m_player.transform.position) < RangePatrol)
                {
                    if (tweener != null)
                    {
                        KillTwen();
                    }
                    stateEnemy = StateEnemy.CHASING;
                }
                if (!isPatrol)
                {
                    if (tweener != null)
                    {
                        KillTwen();
                    }
                    isPatrol = true;
                    Patrol();
                }
                break;
            case StateEnemy.CHASING:
                if (Vector2.Distance(startPos, m_player.transform.position) > RangeChasing)
                {
                    stateEnemy = StateEnemy.PATROL;
                    isPatrol = false;
                }
                else
                {
                    if (tweener != null)
                    {
                        KillTwen();
                    }
                    transform.position = Vector3.MoveTowards(transform.position, m_player.transform.position, SpeedChasing * Time.deltaTime);
                }
                break;
        }
    }
    private void KillTwen()
    {
        tweener.Kill();
    }
    private void Patrol()
    {
        Vector2 RangePatrol = GetPatrolPos();
        tweener = transform.DOMove(RangePatrol, SpeedPatrol).SetSpeedBased(true).OnComplete(() =>
        {
            tweener.Kill();
            isPatrol = false;
        });
    }
    private Vector2 GetPatrolPos()
    {
        float Rand = Random.Range(0.0f, RangePatrol);
        PatrolPos = Random.insideUnitCircle * Rand + startPos;
        return PatrolPos;
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangePatrol);
        Gizmos.color = Color.green;
        if (startPos == Vector2.zero)
        {
            Gizmos.DrawWireSphere(transform.position, RangeChasing);
        }
        else
        {
            Gizmos.DrawWireSphere(startPos, RangeChasing);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player m_player = collision.GetComponent<player>();
        if (m_player != null)
        {
            UIBattle.SetActive(true);
            isTrigger = true;

            UIBattle.GetComponent<UI_Battle>().LoadEnemyBasedElement();
            StartCoroutine(IE_LoadAllidBaseElement());
            SpeedChasing = 0;
            SpeedPatrol = 0;
        }
    }

    IEnumerator IE_LoadAllidBaseElement()
    {
        yield return null;
        //  UIBattle.GetComponent<UI_Battle>().LoadAllidStart();
        UIBattle.GetComponent<UI_Battle>().LoadAlliedBasedElement();
        UIBattle.GetComponent<UI_Battle>().LoadStartAllidBase();
        UIBattle.GetComponent<UI_Battle>().SpawnEnemyBase();
        UIBattle.GetComponent<UI_Battle>().SetHPEnemyBase();
        // UIBattle.GetComponent<UI_Battle>().LoadStartALliBaseElement();
    }
#endif
}
public enum StateEnemy
{
    PATROL,
    CHASING
}
