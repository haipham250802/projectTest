using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class AllidsBaseShow : MonoBehaviour
{
    public ECharacterType Type;
    public int ID;
    public int HP;
    public int Damage;
    public int Rarity;
    public int CoinHealing;
    public int SLotIndex;

    public Button PurchaseBtn;
    //  public SkeletonDataAsset ICON;
    public Image Avatar;
    public Text TxtHP;
    public Text TxtDamage;
    public Text TxtCoinHealing;

    public Slider HP_Bar;
    public Button PurChaseBtn;

    public ElementData ThisElementData;
    public int MaxHP;

    public int CoinStart;

    int Lowest_HP = 0;
    int Low_HP = 0;
    int Medium_HP = 0;
    int High_HP = 0;

    private void Start()
    {
        CoinStart = DataPlayer.GetCoin();

        if (Type != ECharacterType.NONE)
        {
            EnemyStat enemyStat = Controller.Instance.GetStatEnemy(Type);
            MaxHP = enemyStat.HP;
        }
        Lowest_HP = (MaxHP * 0 / 100);
        Low_HP = (MaxHP * 75 / 100);
        Medium_HP = (MaxHP * 50 / 100);
        High_HP = (MaxHP * 25 / 100);

        HP_Bar.maxValue = MaxHP;
        PurChaseBtn.gameObject.SetActive(false);


        /*        Debug.Log("Lowest_HP: " + Lowest_HP);
                Debug.Log("Low_HP: " + Low_HP);
                Debug.Log("MediumHP: " + Medium_HP);
                Debug.Log("HighHP: " + High_HP);*/
    }
    private void Update()
    {
        if (HP < MaxHP)
        {
            PurChaseBtn.gameObject.SetActive(true);
        }

        for (int i = 0; i < Controller.Instance.healingData.E_Blood.Count; i++)
        {
            if (Rarity - 1 == i)
            {
                if (HP >= Lowest_HP && HP <= Low_HP)
                {
                    CoinHealing = Controller.Instance.healingData.E_Blood[i].Cost_HP_Lowest;
                    TxtCoinHealing.text = CoinHealing.ToString();
                    break;
                }
                else if (HP >= Low_HP && HP <= Medium_HP)
                {
                    CoinHealing = Controller.Instance.healingData.E_Blood[i].Cost_HP_Low;
                    TxtCoinHealing.text = CoinHealing.ToString();
                }
                else if (HP >= Medium_HP && HP <= High_HP)
                {
                    CoinHealing = Controller.Instance.healingData.E_Blood[i].Cost_HP_Medium;
                    TxtCoinHealing.text = CoinHealing.ToString();
                    break;
                }
                else if (HP >= High_HP && HP < MaxHP)
                {
                    CoinHealing = CoinHealing = Controller.Instance.healingData.E_Blood[i].Cost_HP_Hight;
                    TxtCoinHealing.text = CoinHealing.ToString();
                    break;
                }
                else
                    TxtCoinHealing.text = 0.ToString();
                break;
            }
        }
    }

    public void Init(ElementData elemendata = null)
    {
        if (elemendata != null)
        {
            Type = elemendata.Type;
            ThisElementData = elemendata;
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            HP = elemendata.HP;
            HP_Bar.value = HP;
            Damage = stat.Damage;
            Rarity = stat.Rarity;
            //  ICON = stat.ICON;
            Avatar.sprite = stat.Avatar;
        }
        else
        {
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            HP = stat.HP;
            Damage = stat.Damage;
            Rarity = stat.Rarity;
            // ICON = stat.ICON;
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
    public void SubCoin()
    {
        CoinStart -= CoinHealing;
        DataPlayer.SetCoin(CoinStart);
        UI_Home.Instance.m_UICoin.CoinText.text = DataPlayer.GetCoin().ToString();
        TxtCoinHealing.text = 0.ToString();
        HP = MaxHP;
        TxtHP.text = HP.ToString();
        HP_Bar.value = HP;
        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            if (Type == DataPlayer.GetListAllid()[i].Type)
                DataPlayer.SetHP(HP, DataPlayer.GetListAllid()[i]);
        }
        if (HP == MaxHP)
        {
            PurchaseBtn.gameObject.SetActive(false);
        }
    }
}
