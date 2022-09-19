using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpCatched : MonoBehaviour
{
    public Image Avatar;
    public Text NameCritter;

    public void SetAvatar(Sprite Avatar)
    {
        this.Avatar.sprite = Avatar;
    }
    public void SetName(ECharacterType NameCritter)
    {
        this.NameCritter.text = NameCritter.ToString();
    }
}
