using CodeStage.AntiCheat.ObscuredTypes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Merge : MonoBehaviour
{
    public int Coin;
    public int CoinMerge;
    public int CurCoin;

    public Button DEL_SLOT1;
    public Button DEL_SLOT2;
    public Button MERGE;
    public Text ButtonMergeTxt;

    public Element slot1, slot2;
    public GameObject SLOT;
    public MergeElement SlotMerge;

    public Transform SlotParent1, SlotParent2, SlotParentMerge, Eventory, EventoryTeam;

    public GameObject prefabsItemMerge;
    public GameObject prefabsItemEventory;

    GameObject Item1Render, Item2Render, ItemMergeRender;


    Dictionary<ECharacterType, int> keyValuePairs = new Dictionary<ECharacterType, int>();
    public UI_Team m_UITeam;
    [SerializeField]
    public List<ElementData> L_elementData = new List<ElementData>();

    public bool isEnoughtCoinMerge;
    private void Start()
    {
        isEnoughtCoinMerge = false;
        Coin = DataPlayer.GetCoin();
        ButtonMergeTxt.text = "MERGE";
        MERGE.onClick.AddListener(OnMergeBtn);
        DEL_SLOT1.gameObject.SetActive(false);
        DEL_SLOT2.gameObject.SetActive(false);
        // Spawn();

        //  m_UITeam.Spawn(Eventory);
    }
    public void ResetAllid()
    {
        slot1 = null;
        slot2 = null;

        var arr = SLOT.GetComponentsInChildren<MergeElement>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].transform.GetChild(0) != null)
            {
                Destroy(arr[i].GetComponentInChildren<MergeElement>().gameObject);
            }
        }
        Debug.Log("da reset");
    }
    public void ADD_SLOT(Element CrtterItem, Action<bool> successed) //, 
    {
        if (slot1 == null)
        {
            slot1 = CrtterItem;
            LoadDataElement(1, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT1.gameObject.SetActive(true);
            MergeCritter();
        }
        else if (slot2 == null)
        {
            slot2 = CrtterItem;
            LoadDataElement(2, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT2.gameObject.SetActive(true);
            MergeCritter();
        }
    }
    public void MergeCritter()
    {
        if (slot1 != null && slot2 != null)
        {
            ItemMergeRender = Instantiate(prefabsItemMerge);
            SlotMerge = ItemMergeRender.GetComponent<MergeElement>();

            if (SlotMerge != null)
            {
                SlotMerge.Type = Controller.Instance.mergeElementData.Child(slot1.Type, slot2.Type);
                for (int i = 0; i < Controller.Instance.enemyData.enemies.Count; i++)
                {
                    if (Controller.Instance.enemyData.enemies[i].Type == SlotMerge.Type)
                    {
                        CoinMerge = Controller.Instance.enemyData.enemies[i].CombineCost;
                        ButtonMergeTxt.text = CoinMerge.ToString();

                        if (CoinMerge < Coin)
                        {
                            Debug.Log("du tien");
                            isEnoughtCoinMerge = true;
                            CurCoin = Coin - CoinMerge;
                            break;
                        }
                    }
                }
                ItemMergeRender.transform.SetParent(SlotParentMerge);
                ItemMergeRender.transform.localPosition = Vector3.zero;
                ItemMergeRender.transform.localScale = Vector3.one;
                SlotMerge.Init();
            }
        }
    }
    public void LoadDataElement(int index, ElementData elementData)
    {
        if (index == 1)
        {
            Item1Render = Instantiate(prefabsItemMerge);
            Item1Render.GetComponent<MergeElement>().Type = slot1.Type;
            Item1Render.GetComponent<MergeElement>().Init(elementData);

            Item1Render.transform.SetParent(SlotParent1.transform);
            Item1Render.transform.localPosition = Vector3.zero;
            Item1Render.transform.localScale = Vector3.one;

            /**//* DataPlayer.AddAlliedIteam(elementData);*/
            DataPlayer.AddAlliedIteamMerge(elementData);
            // DataPlayer.Remove(elementData.Type, elementData.ID);

            L_elementData.Add(elementData);
        }
        else
        {
            Item2Render = Instantiate(prefabsItemMerge);

            Item2Render.GetComponent<MergeElement>().Type = slot2.Type;
            Item2Render.GetComponent<MergeElement>().Init(elementData);
            Item2Render.transform.SetParent(SlotParent2);
            Item2Render.transform.localPosition = Vector3.zero;
            Item2Render.transform.localScale = Vector3.one;

            /*DataPlayer.AddAlliedIteam(elementData);*/
            DataPlayer.AddAlliedIteamMerge(elementData);

            // DataPlayer.Remove(elementData.Type, elementData.ID);
            L_elementData.Add(elementData);
        }
    }
    public void RemoveItem(int index)
    {
        ElementData elementdata = new ElementData();
        if (index == 1)
        {
            DataPlayer.RemoveItemMerge(Item1Render.GetComponent<MergeElement>().ThisElementData);
            Destroy(ItemMergeRender);
            if (Item1Render == null) return;

            Destroy(Item1Render);

            //DataPlayer.Add(elementdata);
            DEL_SLOT1.gameObject.SetActive(false);
            slot1.gameObject.transform.SetAsLastSibling();
            slot1.gameObject.SetActive(true);
            //  LoadEventory(Eventory.gameObject);

            slot1 = null;
        }
        else if (index == 2)
        {
            DataPlayer.RemoveItemMerge(Item2Render.GetComponent<MergeElement>().ThisElementData);

            Destroy(ItemMergeRender);
            if (Item2Render == null) return;

            Destroy(Item2Render);

            //    DataPlayer.Add(elementdata);
            DEL_SLOT2.gameObject.SetActive(false);
            slot2.gameObject.transform.SetAsLastSibling();
            slot2.gameObject.SetActive(true);
            //   LoadEventory(Eventory.gameObject);
            slot2 = null;
        }
    }
    public bool isMerge = false;
    private void OnMergeBtn()
    {
        if (slot1 != null && slot2 != null)
        {
            if (isEnoughtCoinMerge)
            {
                UI_Home.Instance.m_UICoin.CoinText.text = CurCoin.ToString();
                DataPlayer.SetCoin(CurCoin);
                DEL_SLOT1.gameObject.SetActive(false);
                DEL_SLOT2.gameObject.SetActive(false);
                GameObject MergeDone = Instantiate(prefabsItemEventory);
                // MergeDone.transform.SetParent(Eventory);
                MergeDone.GetComponent<Element>().Init();
                MergeDone.transform.localPosition = Vector3.zero;
                MergeDone.transform.localScale = Vector3.one;
                MergeDone.GetComponent<Element>().Type = SlotMerge.Type;
                DataPlayer.Add(SlotMerge.Type);

                UI_Home.Instance.m_UIPopUp._ShowPopUpSuccess(SlotMerge);
                Destroy(SlotMerge.gameObject);
                SlotMerge = null;
                Destroy(Item1Render);
                Destroy(Item2Render);
                Destroy(slot1.gameObject);
                Destroy(slot2.gameObject);
                Debug.Log("List: " + JsonConvert.SerializeObject(L_elementData));

                for (int i = 0; i < L_elementData.Count; i++)
                {
                    for (int j = 0; j < DataPlayer.GetListAllidMerge().Count; j++)
                    {
                        if (L_elementData[i].Type == DataPlayer.GetListAllidMerge()[j].Type &&
                            L_elementData[i].ID == DataPlayer.GetListAllidMerge()[j].ID)
                        {
                            DataPlayer.Remove(L_elementData[i].Type, L_elementData[i].ID);
                            DataPlayer.RemoveItem(L_elementData[i]);
                            DataPlayer.RemoveItemMerge(DataPlayer.GetListAllidMerge()[j]);
                        }
                    }
                }
                L_elementData.Clear();
                UI_Home.Instance.m_UITeam.ResetAllid();
                UI_Home.Instance.m_UITeam.LoadAllied();
                LoadEventory(Eventory.gameObject);

                isEnoughtCoinMerge = false;
            }
            else
            {
                Debug.LogWarning("ban khong du tien !"); // show pop UP cho nay !
            }
        }
        isMerge = true;
    }
    public void LoadEventory(GameObject eventory)
    {
        var arr = eventory.GetComponentsInChildren<Element>();
        for (int i = 0; i < arr.Length; i++)
        {
            Destroy(arr[i].gameObject);
        }
        m_UITeam.Spawn(Eventory.transform);
    }
    public void ResetItemMerge()
    {
        DataPlayer.ResetItemMerge();
        if (ItemMergeRender != null)
        {
            Destroy(ItemMergeRender.GetComponent<MergeElement>().gameObject);
        }
        else
            return;
        Debug.Log(ItemMergeRender);
    }
}
