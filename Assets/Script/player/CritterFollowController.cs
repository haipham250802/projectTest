using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterFollowController : MonoBehaviour
{
    public CritterFollowElement Critter_Follow_Element_01;
    public CritterFollowElement Critter_Follow_Element_02;
    public CritterFollowElement Critter_Follow_Element_03;
    //public CritterFollowElement Critter_Follow_Element_04;

    private void Start()
    {
        LoadCritterFollow();
    }
    public void LoadCritterFollow()
    {
        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            switch (i)
            {
                case 0:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Critter_Follow_Element_01.CritterFollowType = DataPlayer.GetListAllid()[i].Type;
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(DataPlayer.GetListAllid()[i].Type);
                       // Critter_Follow_Element_01.ICON = enemyStat.ICON;
                        Critter_Follow_Element_01.Avatar.sprite = enemyStat.Avatar;
                        Critter_Follow_Element_01.gameObject.SetActive(true);
                    }
                    else
                    {
                        Critter_Follow_Element_01.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Critter_Follow_Element_02.CritterFollowType = DataPlayer.GetListAllid()[i].Type;
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(DataPlayer.GetListAllid()[i].Type);
                       // Critter_Follow_Element_02.ICON = enemyStat.ICON;
                        Critter_Follow_Element_02.Avatar.sprite = enemyStat.Avatar;
                        Critter_Follow_Element_02.gameObject.SetActive(true);
                    }
                    else
                    {
                        Critter_Follow_Element_02.gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Critter_Follow_Element_03.CritterFollowType = DataPlayer.GetListAllid()[i].Type;
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(DataPlayer.GetListAllid()[i].Type);
                      //  Critter_Follow_Element_03.ICON = enemyStat.ICON;
                        Critter_Follow_Element_03.Avatar.sprite = enemyStat.Avatar;
                        Critter_Follow_Element_03.gameObject.SetActive(true);
                    }
                    else
                    {
                        Critter_Follow_Element_03.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
}
