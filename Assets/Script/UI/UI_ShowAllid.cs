using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShowAllid : MonoBehaviour
{
    public AllidsBaseShow Slot1;
    public AllidsBaseShow Slot2;
    public AllidsBaseShow Slot3;

    public GameObject SlotParent_01;
    public GameObject SlotParent_02;
    public GameObject SlotParent_03;
 //   public GameObject SlotParent_04;


    public List<AllidsBaseShow> L_SlotAllidBaseShowDone = new List<AllidsBaseShow>();

    private void Start()
    {
        var arr = GetComponentsInChildren<AllidsBaseShow>();
        LoadAllidBaseShow();
    }
    public void LoadAllidBaseShow()
    {
        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            switch (i)
            {
                case 0:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Slot1.Type = DataPlayer.GetListAllid()[i].Type;
                        Slot1.Init(DataPlayer.GetListAllid()[i]);
                        Slot1.gameObject.SetActive(true);
                        SlotParent_01.SetActive(true);
                    }
                    else
                    {
                        Slot1.gameObject.SetActive(false);
                        SlotParent_01.SetActive(false);
                    }
                    break;
                case 1:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Slot2.Type = DataPlayer.GetListAllid()[i].Type;
                        Slot2.Init(DataPlayer.GetListAllid()[i]);
                        Slot2.gameObject.SetActive(true);
                        SlotParent_02.SetActive(true);
                    }
                    else
                    {
                        Slot2.gameObject.SetActive(false);
                        SlotParent_02.SetActive(false);
                    }
                    break;
                case 2:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Slot3.Type = DataPlayer.GetListAllid()[i].Type;
                        Slot3.Init(DataPlayer.GetListAllid()[i]);
                        Slot3.gameObject.SetActive(true);
                        SlotParent_03.SetActive(true);
                    }
                    else
                    {
                        Slot3.gameObject.SetActive(false);
                        SlotParent_03.SetActive(false);
                    }
                    break;
            }
        }
    }
}
