using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Team : MonoBehaviour
{
    public GameObject Eventory;
    public GameObject prefabsItemEventory;

    GameObject Item1Render, Item2Render, Item3Render;
    public GameObject ItemBased;

    public ElementBase Slot1;
    public ElementBase Slot2;
    public ElementBase Slot3;

    public GameObject SlotParent01, SlotParent02, SlotParent03, SLOT;

    public Button DEL_SLOT1, DEL_SLOT2, DEL_SLOT3;

    public List<GameObject> L_G_Render = new List<GameObject>();
    public List<Element> L_elementEventory = new List<Element>();
    public List<Element> L_SLot = new List<Element>();
    private void Start()
    {
       /* Spawn(Eventory.transform);
        LoadAllied();*/
        gameObject.SetActive(false);
    }
    private void Update()
    {
        for (int i = 0; i < L_elementEventory.Count; i++)
        {
            if (L_elementEventory[i] == null)
            {
                L_elementEventory.RemoveAt(i);
            }
        }
        for(int i = 0; i< L_SLot.Count;i++)
        {
            if (L_SLot[i] == null)
            {
                L_SLot.RemoveAt(i);
            }
        }
    }
    public void Test()
    {

    }    
    public void ResetAllidInEventory()
    {
        for (int i = 0; i < L_elementEventory.Count; i++)
        {
            Debug.Log("da den");
            if (L_elementEventory[i] != null)
                Destroy(L_elementEventory[i].gameObject);
        }
    }
    public void ResetAllid()
    {
        Slot1 = null;
        Slot2 = null;
        Slot3 = null;

        var arr = SLOT.GetComponentsInChildren<Element>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].transform.GetChild(0) != null)
            {
                Destroy(arr[i].GetComponentInChildren<Element>().gameObject);
                //  DataPlayer.Remove(arr[i].GetComponentInChildren<Element>().Type, arr[i].GetComponentInChildren<Element>().ID);
            }
        }
        Debug.Log("da reset");
    }
    public void IsActive(int index)
    {
        Debug.Log("da bam 5");

        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            for (int j = 0; j < L_elementEventory.Count; j++)
            {
                if (i == index)
                {
                    if (L_elementEventory[j] != null)
                    {
                        if (DataPlayer.GetListAllid()[i].ID == L_elementEventory[j].ID &&
                            DataPlayer.GetListAllid()[i].Type == L_elementEventory[j].Type)
                        {
                            L_elementEventory[j].HP = DataPlayer.GetListAllid()[i].HP;
                            DataPlayer.SetHP(L_elementEventory[j].Type, L_elementEventory[j].ID, L_elementEventory[j].HP);
                            L_elementEventory[j].TxtHP.text = L_elementEventory[j].HP.ToString();
                            L_elementEventory[j].gameObject.SetActive(true);

                            if (L_SLot.Count > 1)
                            {
                                for (int k = 0; k < L_SLot.Count; k++)
                                {
                                    if (L_SLot[k] != null)
                                    {
                                        if (DataPlayer.GetListAllid()[i].ID == L_SLot[k].ID &&
                                            DataPlayer.GetListAllid()[i].Type == L_SLot[k].Type)
                                        {
                                            Destroy(L_SLot[k].gameObject);
                                            DataPlayer.RemoveItem(DataPlayer.GetListAllid()[i]);

                                            return;
                                        }
                                    }
                                }
                            }
                            else if (L_SLot.Count == 1)
                            {
                                if (L_SLot[0] != null)
                                {
                                    if (DataPlayer.GetListAllid()[i].ID == L_SLot[0].ID &&
                                        DataPlayer.GetListAllid()[i].Type == L_SLot[0].Type)
                                    {
                                        DataPlayer.GetListAllid()[i].HP = L_SLot[0].HP;
                                        Destroy(L_SLot[0].gameObject);
                                        DataPlayer.RemoveItem(DataPlayer.GetListAllid()[i]);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void LoadAllied()
    {
        Debug.Log("da load");
        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
            {
                //   LoadAllidElement(i, element);

                if (Slot1 == null)
                {
                    GameObject tempItem = Instantiate(ItemBased);
                    tempItem.transform.SetParent(SLOT.transform.GetChild(i));
                    tempItem.transform.localScale = Vector3.one;
                    tempItem.transform.localPosition = Vector3.zero;
                    tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
                    ElementData element = new ElementData();
                    element.Type = DataPlayer.GetListAllid()[i].Type;
                    element.ID = DataPlayer.GetListAllid()[i].ID;
                    element.HP = DataPlayer.GetListAllid()[i].HP;
                    element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                    tempItem.GetComponent<Element>().PurchaseBtn.enabled = false;
                    tempItem.GetComponent<Element>().Init(element);
                    Debug.Log("a " + i);

                    switch (i)
                    {
                        case 0:
                            Slot1 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 1:
                            Slot2 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 2:
                            Slot3 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                    }
                }
                else if (Slot2 == null)
                {
                    GameObject tempItem = Instantiate(ItemBased);
                    tempItem.transform.SetParent(SLOT.transform.GetChild(i));
                    tempItem.transform.localScale = Vector3.one;
                    tempItem.transform.localPosition = Vector3.zero;
                    tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
                    ElementData element = new ElementData();
                    element.Type = DataPlayer.GetListAllid()[i].Type;
                    element.ID = DataPlayer.GetListAllid()[i].ID;
                    element.HP = DataPlayer.GetListAllid()[i].HP;
                    element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                    tempItem.GetComponent<Element>().PurchaseBtn.enabled = false;
                    tempItem.GetComponent<Element>().Init(element);

                    // Slot2 = tempItem.GetComponent<ElementBase>();
                    switch (i)
                    {
                        case 0:
                            Slot1 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 1:
                            Slot2 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 2:
                            Slot3 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                    }
                }
                else if (Slot3 == null)
                {
                    GameObject tempItem = Instantiate(ItemBased);
                    tempItem.transform.SetParent(SLOT.transform.GetChild(i));
                    tempItem.transform.localScale = Vector3.one;
                    tempItem.transform.localPosition = Vector3.zero;
                    tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
                    ElementData element = new ElementData();
                    element.Type = DataPlayer.GetListAllid()[i].Type;
                    element.ID = DataPlayer.GetListAllid()[i].ID;
                    element.HP = DataPlayer.GetListAllid()[i].HP;
                    element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                    tempItem.GetComponent<Element>().PurchaseBtn.enabled = false;
                    tempItem.GetComponent<Element>().Init(element);

                    switch (i)
                    {
                        case 0:
                            Slot1 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 1:
                            Slot2 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 2:
                            Slot3 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                    }
                }
                if (L_elementEventory.Count > 0)
                {
                    for (int k = 0; k < L_elementEventory.Count; k++)
                    {
                        if (DataPlayer.GetListAllid()[i].Type == L_elementEventory[k].Type &&
                            DataPlayer.GetListAllid()[i].ID == L_elementEventory[k].ID)
                        {
                            if (L_elementEventory[k] != null)
                            {
                                L_elementEventory[k].gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
    public void Spawn(Transform _Eventory)
    {

        foreach (var kv in DataPlayer.GetDictionary())
        {
            for (int i = 0; i < kv.Value.Count; i++)
            {
                GameObject tempItem = Instantiate(prefabsItemEventory);
                tempItem.transform.SetParent(_Eventory.transform);
                tempItem.transform.localScale = Vector3.one;
                tempItem.transform.localPosition = Vector3.zero;
                ElementData element = new ElementData();
                element.Type = kv.Key;
                element.ID = kv.Value[i].ID;
                element.HP = kv.Value[i].HP;
                element.Rarity = kv.Value[i].Rarity;

                tempItem.GetComponent<Element>().Init(element);
                L_elementEventory.Add(tempItem.GetComponent<Element>());
            }
        }

    }
    public void HiddenAllidEventory()
    {
        Debug.Log("hidden");
        for (int i = 0; i < L_elementEventory.Count; i++)
        {
            for (int j = 0; j < DataPlayer.GetListAllid().Count; j++)
            {

                if (L_elementEventory[i] != null)
                {
                    if (L_elementEventory[i].Type == DataPlayer.GetListAllid()[j].Type &&
                        L_elementEventory[i].ID == DataPlayer.GetListAllid()[j].ID)
                    {
                        L_elementEventory[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    IEnumerator IE_HiddenAllidEventory()
    {
        yield return null;
        HiddenAllidEventory();
    }
    public void ADD_SLOT_ELEMENT_TEAM(Element CrtterItem, Action<bool> successed)
    {
        if (Slot1 == null)
        {
            Slot1 = CrtterItem;
            LoadDataElement(1, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT1.gameObject.SetActive(true);
        }
        else if (Slot2 == null)
        {
            Slot2 = CrtterItem;
            LoadDataElement(2, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT2.gameObject.SetActive(true);
        }
        else if (Slot3 == null)
        {
            Slot3 = CrtterItem;
            LoadDataElement(3, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT3.gameObject.SetActive(true);
        }
        else
        {
            successed?.Invoke(false);
        }
    }
    /*
        public void LoadAllidElement(int index, ElementData elemendata)
        {
            if (index == 1)
            {
                Item1Render = Instantiate(ItemBased);
                Item1Render.GetComponent<ElementBase>().Type = Slot1.Type;
                Item1Render.GetComponent<MergeElement>().Init(elemendata);

                Item1Render.transform.SetParent(SlotParent01.transform);
                Item1Render.transform.localPosition = Vector3.zero;
                Item1Render.transform.localScale = Vector3.one;
                *//*DataPlayer.AddAlliedIteam(elemendata);
                DataPlayer.Remove(elemendata.Type, elemendata.ID);*//*

            }
            else if (index == 2)
            {
                Item2Render = Instantiate(ItemBased);

                Item2Render.GetComponent<ElementBase>().Type = Slot2.Type;
                Item2Render.GetComponent<MergeElement>().Init(elemendata);
                Item2Render.transform.SetParent(SlotParent02.transform);
                Item2Render.transform.localPosition = Vector3.zero;
                Item2Render.transform.localScale = Vector3.one;

                *//* DataPlayer.AddAlliedIteam(elemendata);
                 DataPlayer.Remove(elemendata.Type, elemendata.ID);*//*
            }
            else if (index == 3)
            {
                Item3Render = Instantiate(ItemBased);

                Item3Render.GetComponent<ElementBase>().Type = Slot3.Type;
                Item3Render.GetComponent<MergeElement>().Init(elemendata);
                Item3Render.transform.SetParent(SlotParent03.transform);
                Item3Render.transform.localPosition = Vector3.zero;
                Item3Render.transform.localScale = Vector3.one;
            }
        }*/
    public void LoadDataElement(int index, ElementData elemendata)
    {
        if (index == 1)
        {

            Item1Render = Instantiate(ItemBased);

            Item1Render.GetComponent<Element>().Type = Slot1.Type;
            Item1Render.GetComponent<Element>().Init(elemendata);

            Item1Render.transform.SetParent(SlotParent01.transform);
            Item1Render.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
            Item1Render.transform.localPosition = Vector3.zero;
            Item1Render.transform.localScale = Vector3.one;
            DataPlayer.AddAlliedIteam(elemendata);
            //  DataPlayer.Remove(elemendata.Type, elemendata.ID);

        }
        else if (index == 2)
        {
            Item2Render = Instantiate(ItemBased);

            Item2Render.GetComponent<Element>().Type = Slot2.Type;
            Item2Render.GetComponent<Element>().Init(elemendata);
            Item2Render.transform.SetParent(SlotParent02.transform);
            Item2Render.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
            Item2Render.transform.localPosition = Vector3.zero;
            Item2Render.transform.localScale = Vector3.one;

            DataPlayer.AddAlliedIteam(elemendata);
            //   DataPlayer.Remove(elemendata.Type, elemendata.ID);
        }
        else if (index == 3)
        {
            Item3Render = Instantiate(ItemBased);

            Item3Render.GetComponent<Element>().Type = Slot3.Type;
            Item3Render.GetComponent<Element>().Init(elemendata);
            Item3Render.transform.SetParent(SlotParent03.transform);
            Item3Render.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
            Item3Render.transform.localPosition = Vector3.zero;
            Item3Render.transform.localScale = Vector3.one;

            DataPlayer.AddAlliedIteam(elemendata);
            //  DataPlayer.Remove(elemendata.Type, elemendata.ID);
        }
    }
    public void RemoveItem(int index)
    {
        Debug.Log("da bam");
        ElementData elementdata = new ElementData();
        if (index == 1)
        {
            if (Item1Render == null) return;

            Destroy(Item1Render);

            //  DataPlayer.Add(elementdata);
            Debug.Log(elementdata);
            DataPlayer.RemoveItem(Item1Render.GetComponent<Element>().ThisElementData);

            DEL_SLOT1.gameObject.SetActive(false);
            Slot1.gameObject.transform.SetAsLastSibling();
            //    UI_Home.Instance.m_UIMerge.LoadEventory(Eventory.gameObject);

            Slot1.gameObject.SetActive(true);
            Slot1 = null;
        }
        else if (index == 2)
        {
            if (Item2Render == null) return;

            Destroy(Item2Render);

            //   DataPlayer.Add(elementdata);
            DataPlayer.RemoveItem(Item2Render.GetComponent<Element>().ThisElementData);

            DEL_SLOT2.gameObject.SetActive(false);
            Slot2.gameObject.transform.SetAsLastSibling();
            //   UI_Home.Instance.m_UIMerge.LoadEventory(Eventory.gameObject);

            Slot2.gameObject.SetActive(true);
            Slot2 = null;
            //    DataPlayer.RemoveItem(elementdata);

        }
        else if (index == 3)
        {
            if (Item3Render == null) return;

            Destroy(Item3Render);

            //  DataPlayer.Add(elementdata);
            DataPlayer.RemoveItem(Item3Render.GetComponent<Element>().ThisElementData);

            DEL_SLOT3.gameObject.SetActive(false);
            Slot3.gameObject.transform.SetAsLastSibling();
            Slot3.gameObject.SetActive(true);

            //   UI_Home.Instance.m_UIMerge.LoadEventory(Eventory.gameObject);
            Slot3 = null;

        }
    }

}
