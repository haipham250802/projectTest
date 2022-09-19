using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;
public class UI_Battle : MonoBehaviour
{
    public GameObject Eventory; // 3 allid (bottom)
    public GameObject TeamEnemyBased;

    public GameObject PosAllidBase;
    public GameObject PosEnemyBase;
    public GameObject PosEnemyWhenPartEnemy;

    public GameObject AllidBasedItem;
    public GameObject SLOT_ALLID;
    public GameObject Soldier;

    public GameObject PopUpVictory;
    public GameObject PopUpCatched;
    public GameObject UI_Catch_Pending;
    public GameObject UI_Catch;

    public SkeletonGraphic AllidBase;
    public SkeletonGraphic EnemyBase;

    public AllidELement SLOT1;
    public AllidELement SLOT2;
    public AllidELement SLOT3;

    public Button OutUIBattleBtn;

    public Button SLOT1_BTN;
    public Button SLOT2_BTN;
    public Button SLOT3_BTN;

    public EnemyBased enemyBased;

    /*    public BossBased bossBased;
        public BossBasedElement bossBaseElement;*/

    public List<AllidELement> L_AllidELements = new List<AllidELement>();
    public List<EnemyBaseElement> L_EnemyBaseElement = new List<EnemyBaseElement>();
    public List<EnemyBased> L_EnemyBase = new List<EnemyBased>();

    public GameObject GroupEnemyBasedElement;

    public Transform EnemyMoveToPosAllidBase;
    public Transform AllidMoveToPosEnemyBase;

    bool isEnemyDie;
    private void Awake()
    {
        OutUIBattleBtn.onClick.AddListener(Out_UI_Battle_Btn);
    }
    void Out_UI_Battle_Btn()
    {
        this.gameObject.SetActive(false);
    }
    private void Start()
    {
        var arrEnemyBasedElement = GroupEnemyBasedElement.GetComponentsInChildren<EnemyBaseElement>();
        for (int i = 0; i < arrEnemyBasedElement.Length; i++)
        {
            arrEnemyBasedElement[i].transform.SetParent(TeamEnemyBased.transform);
            L_EnemyBaseElement.Add(arrEnemyBasedElement[i]);
        }
        SpawnEnemyBase();

        gameObject.SetActive(false);
    }

    private void Update()
    {

    }
    public void UpdateViewEnemyBaseElement()
    {
        for (int i = 0; i < L_EnemyBaseElement.Count; i++)
        {
            if (enemyBased.ID == L_EnemyBaseElement[i].ID &&
                enemyBased.Type == L_EnemyBaseElement[i].Type)
            {
                L_EnemyBaseElement[i].HP = enemyBased.HP;
                L_EnemyBaseElement[i].TxtHP.text = enemyBased.HP.ToString();
            }
        }
    }

    public void MoveToPosEnemyWhenPartEnemy()
    {
        enemyBased.MoveToPosCatchedCritter();
    }

    public void GameOver()
    {
        int dem = 0;
        for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (L_AllidELements[i].HP < 1)
            {
                dem++;
            }
        }
        if (dem == 3)
        {
            Debug.Log("Thua roi !!!!");
        }
    }
    int count = 0;
    public bool isPartEnemy = false;
    public void GameWin()
    {
        Debug.Log("Count List Enemy: " + L_EnemyBaseElement.Count);
        for (int i = 0; i < L_EnemyBaseElement.Count; i++)
        {
            if (L_EnemyBaseElement[i].HP < 1)
            {
                count++;
                Debug.Log(count);
            }
        }
        if (count == L_EnemyBaseElement.Count)
        {
            Debug.Log("Part Enemy");
            isPartEnemy = true;
        }
    }
    public void DestroyEnemy()
    {
        Destroy(Soldier);
    }
    public void switchEnemy()
    {
        for (int i = 0; i < L_EnemyBaseElement.Count; i++)
        {
            for (int j = i + 1; j < L_EnemyBaseElement.Count; j++)
            {
                if (L_EnemyBaseElement[i].HP < 1)
                {
                    L_EnemyBaseElement[i].HP = 0;
                    EnemyBase.skeletonDataAsset = L_EnemyBaseElement[j].ICON;
                    enemyBased.init(L_EnemyBaseElement[j]);
                    break;
                }
            }
        }
    }
    public void SetHPEnemyBase()
    {
        for (int i = 0; i < L_EnemyBaseElement.Count; i++)
        {
            if (enemyBased.ID == L_EnemyBaseElement[i].ID &&
                enemyBased.Type == L_EnemyBaseElement[i].Type)
            {
                L_EnemyBaseElement[i].TxtHP.text = enemyBased.HP.ToString();
            }
        }
    }
    public void LoadAllidBase()
    {
        ResetAllidBase();
        if (DataPlayer.GetListAllid().Count > 1)
        {
            for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
            {
                if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                {
                    GameObject temp = Instantiate(AllidBasedItem);
                    temp.transform.SetParent(Eventory.transform);
                    temp.transform.localScale = Vector3.one;

                    ElementData elementData = new ElementData();

                    elementData.Type = DataPlayer.GetListAllid()[i].Type;
                    elementData.HP = DataPlayer.GetListAllid()[i].HP;
                    elementData.ID = DataPlayer.GetListAllid()[i].ID;

                    temp.GetComponent<AllidELement>().Init(elementData);
                    L_AllidELements.Add(temp.GetComponent<AllidELement>());
                    BattleController.Instance.L_AllidELement.Add(temp.GetComponent<AllidELement>());
                }
            }
        }
        else if (DataPlayer.GetListAllid().Count == 1)
        {
            if (DataPlayer.GetListAllid()[0].Type != ECharacterType.NONE)
            {
                GameObject temp = Instantiate(AllidBasedItem);
                temp.transform.SetParent(Eventory.transform);
                temp.transform.localScale = Vector3.one;

                ElementData elementData = new ElementData();

                elementData.Type = DataPlayer.GetListAllid()[0].Type;
                elementData.HP = DataPlayer.GetListAllid()[0].HP;
                elementData.ID = DataPlayer.GetListAllid()[0].ID;

                temp.GetComponent<AllidELement>().Init(elementData);
                L_AllidELements.Add(temp.GetComponent<AllidELement>());
                BattleController.Instance.L_AllidELement.Add(temp.GetComponent<AllidELement>());
            }
        }
    }
    IEnumerator IE_SpawnEnemyBase()
    {
        yield return null;
        SpawnEnemyBase();
    }
    public void SpawnEnemyBase()
    {
        for (int i = 0; i < L_EnemyBaseElement.Count; i++)
        {
            if (L_EnemyBaseElement[i].HP > 0)
            {
                enemyBased.SetData(L_EnemyBaseElement[i].ICON);
                enemyBased.init(L_EnemyBaseElement[i]);
                enemyBased.PlayAnimation("Idle", true, null);
                break;
            }
        }
    }
    public void LoadEnemyBasedElement()
    {
        if (L_EnemyBaseElement.Count > 1)
        {
            for (int i = 0; i < L_EnemyBaseElement.Count; i++)
            {
                L_EnemyBaseElement[i].transform.SetParent(TeamEnemyBased.transform);
            }
        }
        StartCoroutine(IE_SpawnEnemyBase());
    }
    public void ResetEnemyBasedELement()
    {
        Debug.Log("da destroy");
        var arr = TeamEnemyBased.GetComponentsInChildren<EnemyBaseElement>();
        for (int i = 0; i < arr.Length; i++)
        {
            Destroy(arr[i].gameObject);
        }
    }
    public void ResetAllidBase()
    {
        var arr = Eventory.GetComponentsInChildren<AllidELement>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].gameObject != null)
            {
                Destroy(arr[i].gameObject);
            }
        }
    }
    public void SpawnAllidBased(SkeletonDataAsset ICON)
    {
        AllidBase.GetComponent<AllidBase>().m_UIBattle = this;
        AllidBase.skeletonDataAsset = null;
        AllidBase.skeletonDataAsset = ICON;
        AllidBase.Initialize(true);
    }

    public void LoadAlliedBasedElement()
    {
        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
            {
                if (SLOT1 == null)
                {
                    GameObject tempItem = Instantiate(AllidBasedItem);
                    tempItem.transform.SetParent(SLOT_ALLID.transform.GetChild(i));
                    tempItem.transform.localScale = Vector3.one;
                    tempItem.transform.localPosition = Vector3.zero;
                    tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
                    ElementData element = new ElementData();
                    element.Type = DataPlayer.GetListAllid()[i].Type;
                    element.ID = DataPlayer.GetListAllid()[i].ID;
                    element.HP = DataPlayer.GetListAllid()[i].HP;
                    element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                    tempItem.GetComponent<AllidELement>().PurchaseBtn.enabled = false;
                    tempItem.GetComponent<AllidELement>().Init(element);

                    switch (i)
                    {
                        case 0:
                            SLOT1 = tempItem.GetComponent<AllidELement>();
                            L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                            break;
                        case 1:
                            SLOT2 = tempItem.GetComponent<AllidELement>();
                            L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                            break;
                        case 2:
                            SLOT3 = tempItem.GetComponent<AllidELement>();
                            L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                            break;
                    }
                }
                else if (SLOT2 == null)
                {
                    GameObject tempItem = Instantiate(AllidBasedItem);
                    tempItem.transform.SetParent(SLOT_ALLID.transform.GetChild(i));
                    tempItem.transform.localScale = Vector3.one;
                    tempItem.transform.localPosition = Vector3.zero;
                    tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
                    ElementData element = new ElementData();
                    element.Type = DataPlayer.GetListAllid()[i].Type;
                    element.ID = DataPlayer.GetListAllid()[i].ID;
                    element.HP = DataPlayer.GetListAllid()[i].HP;
                    element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                    tempItem.GetComponent<AllidELement>().PurchaseBtn.enabled = false;
                    tempItem.GetComponent<AllidELement>().Init(element);

                    switch (i)
                    {
                        case 0:
                            SLOT1 = tempItem.GetComponent<AllidELement>();
                            L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                            break;
                        case 1:
                            SLOT2 = tempItem.GetComponent<AllidELement>();
                            L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                            break;
                        case 2:
                            SLOT3 = tempItem.GetComponent<AllidELement>();
                            L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                            break;
                    }
                }
                else if (SLOT3 == null)
                {
                    GameObject tempItem = Instantiate(AllidBasedItem);
                    tempItem.transform.SetParent(SLOT_ALLID.transform.GetChild(i));
                    tempItem.transform.localScale = Vector3.one;
                    tempItem.transform.localPosition = Vector3.zero;
                    tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
                    ElementData element = new ElementData();
                    element.Type = DataPlayer.GetListAllid()[i].Type;
                    element.ID = DataPlayer.GetListAllid()[i].ID;
                    element.HP = DataPlayer.GetListAllid()[i].HP;
                    element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                    tempItem.GetComponent<AllidELement>().PurchaseBtn.enabled = false;
                    tempItem.GetComponent<AllidELement>().Init(element);

                    switch (i)
                    {
                        case 0:
                            SLOT1 = tempItem.GetComponent<AllidELement>();
                            L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                            break;
                        case 1:
                            SLOT2 = tempItem.GetComponent<AllidELement>();
                            L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                            break;
                        case 2:
                            SLOT3 = tempItem.GetComponent<AllidELement>();
                            L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                            break;
                    }
                }
            }
        }
    }

    IEnumerator IE_ResetAllidBaseElement()
    {
        yield return null;
        ResetAllidBaseElement();
        yield return null;
    }
    public void ResetAllidBaseElement()
    {
        Debug.Log("da reset");

        if (SLOT1 != null)
            SLOT1 = null;
        if (SLOT2 != null)
            SLOT2 = null;
        if (SLOT3 != null)
            SLOT3 = null;

        L_AllidELements.Clear();

        Debug.Log("da reset AllidBaseElement");
    }
    public void LoadStartALliBaseElement()
    {
        Debug.Log("da load start");
        for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (L_AllidELements[i].HP > 0)
            {
                AllidBase allidBase = AllidBase.GetComponent<AllidBase>();

                allidBase.Type = L_AllidELements[i].Type;
                AllidBase.skeletonDataAsset = null;
                allidBase.ICON = L_AllidELements[i].ICON;
                AllidBase.Initialize(true);
                allidBase.HP = L_AllidELements[i].HP;
                allidBase.ID = L_AllidELements[i].ID;
                allidBase.Damage = L_AllidELements[i].Damage;
                break;
            }
            else
            {
                L_AllidELements[i].gameObject.SetActive(false);
            }
        }
    }
    public void LoadStartAllidBase()
    {
        if (SLOT1 != null)
        {
            AllidBase alid = AllidBase.GetComponent<AllidBase>();
            alid.Type = SLOT1.Type;
            alid.ICON = SLOT1.ICON;
            AllidBase.skeletonDataAsset = null;
            AllidBase.skeletonDataAsset = SLOT1.ICON;
            AllidBase.Initialize(true);
            alid.HP = SLOT1.HP;
            alid.ID = SLOT1.ID;
            alid.Damage = SLOT1.Damage;

            alid.PlayAnimation("Idle", true, null);
        }
    }
    public void SpawnAllidBase(int index)
    {
        DisableButton();
        if (index == 1)
        {
            Debug.Log("1: " + AllidBase.GetComponent<AllidBase>().Type);
            Debug.Log("2: " + SLOT1.Type);

            AllidBase allibase = AllidBase.GetComponent<AllidBase>();

            allibase.Type = SLOT1.Type;
            allibase.HP = SLOT1.HP;
            allibase.ID = SLOT1.ID;
            allibase.Damage = SLOT1.Damage;
            // allibase.ICON = SLOT1.ICON;
            AllidBase.skeletonDataAsset = null;
            AllidBase.skeletonDataAsset = SLOT1.ICON;
            AllidBase.Initialize(true);


            if (allibase.HP > 0)
                allibase.Attack();
        }
        if (index == 2)
        {
            AllidBase allibase = AllidBase.GetComponent<AllidBase>();

            allibase.Type = SLOT2.Type;
            allibase.ICON = SLOT2.ICON;
            AllidBase.skeletonDataAsset = null;
            AllidBase.skeletonDataAsset = SLOT2.ICON;
            AllidBase.Initialize(true);
            AllidBase.GetComponent<SkeletonGraphic>().skeletonDataAsset = SLOT2.ICON;
            allibase.HP = SLOT2.HP;
            allibase.ID = SLOT2.ID;
            allibase.Damage = SLOT2.Damage;

            if (AllidBase.GetComponent<AllidBase>().HP > 0)
                AllidBase.GetComponent<AllidBase>().Attack();
        }
        if (index == 3)
        {
            AllidBase allidbase = AllidBase.GetComponent<AllidBase>();

            allidbase.Type = SLOT3.Type;
            allidbase.ICON = SLOT3.ICON;
            AllidBase.skeletonDataAsset = null;
            AllidBase.skeletonDataAsset = SLOT3.ICON;
            AllidBase.Initialize(true);
            AllidBase.GetComponent<SkeletonGraphic>().skeletonDataAsset = SLOT3.ICON;
            allidbase.HP = SLOT3.HP;
            allidbase.ID = SLOT3.ID;
            allidbase.Damage = SLOT3.Damage;

            if (allidbase.HP > 0)
                allidbase.Attack();
        }
    }
    public void ChangeAllidBase(int index)
    {
        if (index == 1)
        {
            AllidBase allidbase = AllidBase.GetComponent<AllidBase>();

            allidbase.Type = SLOT1.Type;
            allidbase.ICON = SLOT1.ICON;
            allidbase.HP = SLOT1.HP;
            allidbase.ID = SLOT1.ID;
            allidbase.Damage = SLOT1.Damage;
        }
        if (index == 2)
        {
            AllidBase allidbase = AllidBase.GetComponent<AllidBase>();

            allidbase.Type = SLOT2.Type;
            allidbase.ICON = SLOT2.ICON;
            allidbase.HP = SLOT2.HP;
            allidbase.ID = SLOT2.ID;
            allidbase.Damage = SLOT2.Damage;
        }
        if (index == 3)
        {
            AllidBase allidbase = AllidBase.GetComponent<AllidBase>();

            allidbase.Type = SLOT3.Type;
            allidbase.ICON = SLOT3.ICON;
            allidbase.HP = SLOT3.HP;
            allidbase.ID = SLOT3.ID;
            allidbase.Damage = SLOT3.Damage;
        }
    }
    public void TakeDamageAllid(int damage, AllidBase allidBase)
    {
        for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (allidBase.GetComponent<AllidBase>().ID == L_AllidELements[i].ID
                && allidBase.GetComponent<AllidBase>().Type == L_AllidELements[i].Type)
            {
                allidBase.HP -= damage;
                if (allidBase.HP <= 0)
                {
                    allidBase.HP = 0;
                }
                DataPlayer.SetHP(allidBase.HP, L_AllidELements[i].ThisElementData);
                L_AllidELements[i].Init(L_AllidELements[i].ThisElementData);
            }
        }
    }
    public void TakeDamageEnemy(int damage, EnemyBased enemyBase)
    {
        enemyBase.HP -= damage;
    }
    public void EnableButton()
    {
        SLOT1_BTN.enabled = true;
        SLOT2_BTN.enabled = true;
        SLOT3_BTN.enabled = true;
    }
    public void DisableButton(int Index)
    {
        switch (Index)
        {
            case 1:
                SLOT1_BTN.enabled = false;
                break;
            case 2:
                SLOT2_BTN.enabled = false;
                break;
            case 3:
                SLOT3_BTN.enabled = false;
                break;
        }
    }
    public void DisableButton()
    {
        SLOT1_BTN.enabled = false;
        SLOT2_BTN.enabled = false;
        SLOT3_BTN.enabled = false;
    }
    public void ChangeAllidBase(AllidBase allibase)
    {
        if (allibase.HP < 1)
        {
            if (allibase.Type == L_AllidELements[L_AllidELements.Count - 1].Type && allibase.ID == L_AllidELements[L_AllidELements.Count - 1].ID)
            {
                L_AllidELements[L_AllidELements.Count - 1].gameObject.SetActive(false);
                DisableButton((L_AllidELements.Count - 1) + 1);
                return;
            }
            for (int i = 0; i < L_AllidELements.Count; i++)
            {
                if (allibase.Type == L_AllidELements[i].Type && allibase.ID == L_AllidELements[i].ID)
                {
                    L_AllidELements[i].gameObject.SetActive(false);
                    DisableButton(i + 1);
                }
            }

            for (int i = 0; i < L_AllidELements.Count; i++)
            {
                if (L_AllidELements[i].HP > 0)
                {
                    ChangeAllidBase(i + 1);
                    break;
                }
            }
        }
    }

    public int Catch_Chance;
    public int Catch_Chance_Plus = 0;
    public int Sum_Catch;
    public bool IsCatchDone = false;
    Dictionary<EnemyBased, float> keyValuePairs = new Dictionary<EnemyBased, float>();

    public void LoadCatchChance(EnemyBased enemBased)
    {
        Debug.Log("Load Catch Chance");
        EnemyBased enemy = new EnemyBased();
        //  Dictionary<EnemyBased, float> keyValuePairs = new Dictionary<EnemyBased, float>();
        for (int i = 0; i < Controller.Instance.enemyData.enemies.Count; i++)
        {
            if (enemyBased.Type == Controller.Instance.enemyData.enemies[i].Type)
            {
                keyValuePairs.Remove(enemyBased);
                keyValuePairs.Remove(enemy);
                break;
            }
        }

        for (int i = 0; i < Controller.Instance.enemyData.enemies.Count; i++)
        {
            if (enemyBased.Type == Controller.Instance.enemyData.enemies[i].Type)
            {
                Catch_Chance = Controller.Instance.enemyData.enemies[i].CatchChance;
                Sum_Catch = Catch_Chance + Catch_Chance_Plus;
                keyValuePairs.Add(enemyBased, Sum_Catch);
                keyValuePairs.Add(enemy, 100 - (Sum_Catch));
                break;
            }
        }
    }
    public void CatchEnemy(EnemyBased enemyBased)
    {
        if (Catch_Chance_Controller.GetRandomByPercent(keyValuePairs) == enemyBased)
        {
            IsCatchDone = true;
            foreach (var kv in DataPlayer.GetDictionary())
            {
                DataPlayer.Add(enemyBased.Type);
                PopUpCatched.GetComponent<PopUpCatched>().SetName(enemyBased.Type);

                EnemyStat enemyStat = Controller.Instance.GetStatEnemy(enemyBased.Type);
                PopUpCatched.GetComponent<PopUpCatched>().SetAvatar(enemyStat.Avatar);

                PopUpCatched.SetActive(true);
            }
        }
        else
        {
            Debug.Log("khong bat dc");
            StartCoroutine(IE_FallPosCritter());
        }
    }
    IEnumerator IE_FallPosCritter()
    {
        yield return new WaitForSeconds(1f);
        UI_Catch.GetComponent<UI_Catch>().FallPosCritter();
    }
}
