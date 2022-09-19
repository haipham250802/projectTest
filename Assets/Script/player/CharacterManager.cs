using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;
    public static CharacterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CharacterManager();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    public List<EnemyBased> L_enemyBased;
    public List<AllidBase> L_AllidBase = new List<AllidBase>();
    public List<GameObject> L_Enemy = new List<GameObject>();
    public List<AllidsBaseShow> L_AllidBaseShow = new List<AllidsBaseShow>();


    [FoldoutGroup("List Character")]
    public List<BossPatrol> List_Boss = new List<BossPatrol>();
    [FoldoutGroup("List Character")]
    public List<Enemy> List_Enemy = new List<Enemy>();

    public Action A_GameWin;

    private void Start()
    {
        A_GameWin += GameWin;
    }

    public void GameWin()
    {
        Debug.Log("Thang Roi !!!");
    }
    public void AddListEnemyBased(EnemyBased enemubased)
    {
        L_enemyBased.Add(enemubased);
    }
    public void initListAllid(AllidBase tmp)
    {
        L_AllidBase.Add(tmp);
    }
    public List<EnemyBased> ListEnemyBased()
    {
        return L_enemyBased;
    }
    public List<AllidBase> ListAllidBased()
    {
        return L_AllidBase;
    }
    private void OnDisable()
    {
        Destroy(gameObject);
        A_GameWin -= GameWin;
    }
}
