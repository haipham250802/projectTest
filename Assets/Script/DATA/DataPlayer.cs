using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using CodeStage.AntiCheat.ObscuredTypes;

public static class DataPlayer
{
    private const string ALL_DATA = "ALL_DATA";
    private const string ALL_DATA_ALLID = "ALL_DATA_ALLID";
    private const string ALL_DATA_ALLID_MERGE = "ALL_DATA_ALLID_MERGE";
    private const string ALL_DATA_COIN = "ALL_DATA_COIN";
    private const string ALL_DATA_POINT = "ALL_DATA_POINT";
    private static DataInfoPlayer dataInfoPlayer;
    private static DataAllid dataAllid;
    private static DataAllidMerge dataAllidMerge;
    private static DataCoin dataCoin;
    private static DataPoint dataPoint;


    static DataPlayer()
    {
        dataInfoPlayer = JsonConvert.DeserializeObject<DataInfoPlayer>(PlayerPrefs.GetString(ALL_DATA));
        if (dataInfoPlayer == null)
        {
            dataInfoPlayer = new DataInfoPlayer();

            dataInfoPlayer.Add(ECharacterType.Bonare);
            dataInfoPlayer.Add(ECharacterType.Eyemeleon);
        }
        dataAllid = JsonConvert.DeserializeObject<DataAllid>(PlayerPrefs.GetString(ALL_DATA_ALLID));

        if (dataAllid == null)
        {
            dataAllid = new DataAllid(3);
        }
        dataAllidMerge = JsonConvert.DeserializeObject<DataAllidMerge>(PlayerPrefs.GetString(ALL_DATA_ALLID_MERGE));
        if (dataAllidMerge == null)
        {
            dataAllidMerge = new DataAllidMerge(2);
        }
        dataCoin = JsonConvert.DeserializeObject<DataCoin>(PlayerPrefs.GetString(ALL_DATA_COIN));
        if (dataCoin == null)
        {
            dataCoin = new DataCoin(5000);
        }
        dataPoint = JsonConvert.DeserializeObject<DataPoint>(PlayerPrefs.GetString(ALL_DATA_POINT));
        if (dataPoint == null)
        {
            dataPoint = new DataPoint();
        }

        SaveData();
        SaveDataAllied();
        SaveDataAlliedMerge();
        SaveDataCoin();
        SaveDataPoint();
    }

    private static void SaveData()
    {
        string json = JsonConvert.SerializeObject(dataInfoPlayer);
        PlayerPrefs.SetString(ALL_DATA, json);
    }
    private static void SaveDataAllied()
    {
        string json = JsonConvert.SerializeObject(dataAllid);
        PlayerPrefs.SetString(ALL_DATA_ALLID, json);
    }

    private static void SaveDataAlliedMerge()
    {
        string json = JsonConvert.SerializeObject(dataAllidMerge);
        PlayerPrefs.SetString(ALL_DATA_ALLID_MERGE, json);
    }

    private static void SaveDataCoin()
    {
        string json = JsonConvert.SerializeObject(dataCoin);
        PlayerPrefs.SetString(ALL_DATA_COIN, json);
    }
    public static void SaveDataPoint()
    {
        string json = JsonConvert.SerializeObject(dataPoint);
        PlayerPrefs.SetString(ALL_DATA_POINT, json);
    }
    public static void Log()
    {
        string json = JsonConvert.SerializeObject(dataInfoPlayer);
        Debug.Log(json);
    }
    public static void Add(ECharacterType Key)
    {
        Debug.Log(Key);
        dataInfoPlayer.Add(Key);
        SaveData();
    }
    public static void Add(ElementData elementData)
    {
        dataInfoPlayer.Add(elementData);
        SaveData();
    }
    public static void Remove(ECharacterType Key, int ID)
    {
        dataInfoPlayer.Remove(Key, ID);
        SaveData();
    }
    public static bool Checkey(ECharacterType Key)
    {
        return dataInfoPlayer.CheckKey(Key);
    }
    public static Dictionary<ECharacterType, List<ElementData>> GetDictionary()
    {
        return dataInfoPlayer.keyValuePairs;
    }
    public static List<ElementData> GetListAllid()
    {
        return dataAllid.keyValueAllid;
    }
    public static List<ElementData> GetListAllidMerge()
    {
        return dataAllidMerge.keyValueAllid;
    }
    public static void AddAlliedIteam(ElementData elementData)
    {
        dataAllid.AddAllidItem(elementData);
        SaveDataAllied();
    }
    public static void RemoveItem(ElementData elementData)
    {
        dataAllid.RemoveItem(elementData);
        SaveDataAllied();
    }
    public static void AddAlliedIteamMerge(ElementData elementData)
    {
        dataAllidMerge.AddAllidItemMerge(elementData);
        SaveDataAlliedMerge();
    }
    public static void RemoveItemMerge(ElementData elementData)
    {
        dataAllidMerge.RemoveItemMerge(elementData);
        SaveDataAlliedMerge();
    }
    public static void ResetItemMerge()
    {
        dataAllidMerge.ResetItemMerge();
        SaveDataAlliedMerge();
    }
    public static void SetHP(int HP, ElementData elementData)
    {
        dataAllid.SetHP(HP, elementData);
        SaveDataAllied();
    }
    public static void SetHP(ECharacterType key, int ID, int HP)
    {
        dataInfoPlayer.SetHP(key, ID, HP);
        SaveData();
    }
    public static int GetCoin()
    {
        return dataCoin.GetCoin();
    }
    public static void SetCoin(int Coin)
    {
        dataCoin.SetCoin(Coin);
        SaveDataCoin();
    }
    public static Vector2 GetPos()
    {
        return dataPoint.GetPos();
    }
    public static void SetPos(Vector2 Pos)
    {
        dataPoint.SetPos(Pos);
        SaveDataPoint();
    }
}
public class DataAllidMerge
{
    public List<ElementData> keyValueAllid = new List<ElementData>();
    public DataAllidMerge(int count)
    {
        keyValueAllid = new List<ElementData>();
        for (int i = 0; i < count; i++)
        {
            ElementData elementData = new ElementData();
            elementData.Type = ECharacterType.NONE;
            keyValueAllid.Add(elementData);
        }
    }

    public void AddAllidItemMerge(ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == ECharacterType.NONE)
            {
                keyValueAllid[i].Type = m_elementData.Type;
                keyValueAllid[i].ID = m_elementData.ID;
                keyValueAllid[i].HP = m_elementData.HP;
                break;
            }
        }
    }

    public void RemoveItemMerge(ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == m_elementData.Type && keyValueAllid[i].ID == m_elementData.ID)
            {
                keyValueAllid[i].Type = ECharacterType.NONE;
                keyValueAllid[i].ID = 0;
                keyValueAllid[i].HP = 0;
                break;
            }
        }
    }
    public void ResetItemMerge()
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            keyValueAllid[i].Type = ECharacterType.NONE;
        }
    }
}
public class DataAllid
{
    public List<ElementData> keyValueAllid = new List<ElementData>();
    DataInfoPlayer m_dataInfo = new DataInfoPlayer();
    public DataAllid(int count)
    {
        keyValueAllid = new List<ElementData>();
        for (int i = 0; i < count; i++)
        {
            ElementData elementData = new ElementData();
            elementData.Type = ECharacterType.NONE;
            keyValueAllid.Add(elementData);
        }
    }

    public void AddAllidItem(ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == ECharacterType.NONE)
            {
                keyValueAllid[i].Type = m_elementData.Type;
                keyValueAllid[i].ID = m_elementData.ID;
                keyValueAllid[i].HP = m_elementData.HP;
                keyValueAllid[i].Rarity = m_elementData.Rarity;
                break;
            }
        }
    }

    public void RemoveItem(ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == m_elementData.Type && keyValueAllid[i].ID == m_elementData.ID)
            {
                keyValueAllid[i].Type = ECharacterType.NONE;
                keyValueAllid[i].ID = 0;
                keyValueAllid[i].HP = 0;
                break;
            }
        }
    }

    public void SetHP(int HP, ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == m_elementData.Type && keyValueAllid[i].ID == m_elementData.ID)
            {
                m_elementData.HP = HP;
                if (m_elementData.HP < 0)
                {
                    m_elementData.HP = 0;
                }
                keyValueAllid[i].HP = m_elementData.HP;
            }
        }
    }
}
public class DataInfoPlayer
{
    public Dictionary<ECharacterType, List<ElementData>> keyValuePairs = new Dictionary<ECharacterType, List<ElementData>>();
    public bool CheckKey(ECharacterType Key)
    {
        return keyValuePairs.ContainsKey(Key);
    }
    public void Add(ECharacterType Key)
    {
        if (keyValuePairs.ContainsKey(Key))
        {
            List<ElementData> L_elementData = keyValuePairs[Key];
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = Key;
            m_elemenData.ID = L_elementData[L_elementData.Count - 1].ID + 1;
            m_elemenData.HP = Controller.Instance.enemyData.EnemyStatIndex(Key).HP;
            m_elemenData.Rarity = Controller.Instance.enemyData.EnemyStatIndex(Key).Rarity;
            L_elementData.Add(m_elemenData);

            keyValuePairs[Key] = L_elementData;
        }
        else
        {
            List<ElementData> L_elementData = new List<ElementData>();
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = Key;
            m_elemenData.ID = 0;
            m_elemenData.HP = Controller.Instance.enemyData.EnemyStatIndex(Key).HP;
            m_elemenData.Rarity = Controller.Instance.enemyData.EnemyStatIndex(Key).Rarity;
            L_elementData.Add(m_elemenData);
            keyValuePairs.Add(Key, L_elementData);
        }
    }
    public void Add(ElementData elementData)
    {
        if (keyValuePairs.ContainsKey(elementData.Type))
        {
            List<ElementData> L_elementData = keyValuePairs[elementData.Type];
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = elementData.Type;
            m_elemenData.ID = elementData.ID;
            m_elemenData.HP = elementData.HP;
            m_elemenData.Rarity = elementData.Rarity;

            L_elementData.Add(m_elemenData);

            keyValuePairs[elementData.Type] = L_elementData;
        }
        else
        {
            List<ElementData> L_elementData = new List<ElementData>();
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = elementData.Type;
            m_elemenData.ID = elementData.ID;
            m_elemenData.HP = elementData.HP;
            m_elemenData.Rarity = elementData.Rarity;
            L_elementData.Add(m_elemenData);
            keyValuePairs.Add(m_elemenData.Type, L_elementData);
        }
    }
    public void Remove(ECharacterType Key, int ID)
    {
        Debug.Log(JsonConvert.SerializeObject(keyValuePairs));
        Debug.Log(Key + " --- " + ID);

        List<ElementData> L_elementData = keyValuePairs[Key];
        if (L_elementData.Count == 1)
        {
            keyValuePairs.Remove(Key);
        }
        else
        {
            for (int i = 0; i < L_elementData.Count; i++)
            {
                if (L_elementData[i].ID == ID)
                {
                    L_elementData.RemoveAt(i);
                    break;
                }
            }
        }
    }
    public void SetHP(ECharacterType Key, int ID, int HP)
    {
        List<ElementData> L_elementData = keyValuePairs[Key];

        for (int i = 0; i < L_elementData.Count; i++)
        {
            if (L_elementData[i].ID == ID)
            {
                L_elementData[i].HP = HP;
                if (L_elementData[i].HP < 0)
                {
                    L_elementData[i].HP = 0;
                }
            }
        }
    }
}
public class DataCoin
{
    public int coin = 0;

    public DataCoin(int coin)
    {
        this.coin = coin;
    }

    public int GetCoin()
    {
        return coin;
    }
    public void SetCoin(int coin)
    {
        this.coin = coin;
    }
}
public class DataPoint
{
    public Vector2 Pos;

    public Vector2 GetPos()
    {
        return Pos;
    }
    public void SetPos(Vector2 Pos)
    {
        this.Pos = Pos;
    }
}
public class ElementData
{
    public ECharacterType Type;
    public int ID;
    public int HP;
    public int Rarity;
}
