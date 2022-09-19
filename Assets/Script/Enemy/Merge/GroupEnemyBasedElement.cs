using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class GroupEnemyBasedElement : MonoBehaviour
{
#if UNITY_EDITOR
    //  public List<Items> List_Items = new List<Items>();
    public List<Items> L_Items = new List<Items>();
    public Transform ParentItems;
    [Button("Render Items")]
    private void RenderItems()
    {
        // this.GetComponent<RectTransform>().po
        if (L_Items.Count > 1)
        {
            for (int i = 0; i < L_Items.Count; i++)
            {
                if (L_Items[i].enemyBaseElement != null)
                {
                    var Obj = Instantiate(L_Items[i].enemyBaseElement.gameObject);
                    Obj.transform.SetParent(ParentItems.transform);
                    Obj.gameObject.GetComponent<EnemyBaseElement>().Type = L_Items[i].TypeName;
                    Obj.gameObject.GetComponent<EnemyBaseElement>().TypeEnemy = L_Items[i].TypeEnemy;
                    Debug.Log(Obj.gameObject.GetComponent<RectTransform>().position);
                    Obj.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
                }
            }
        }
        else if (L_Items.Count == 1)
        {
            if (L_Items[0].enemyBaseElement != null)
            {
                var Obj = Instantiate(L_Items[0].enemyBaseElement.gameObject);
                Obj.transform.SetParent(ParentItems.transform);
                Obj.gameObject.GetComponent<EnemyBaseElement>().Type = L_Items[0].TypeName;
                Obj.gameObject.GetComponent<EnemyBaseElement>().TypeEnemy = L_Items[0].TypeEnemy;
                Debug.Log(Obj.gameObject.GetComponent<RectTransform>().position);
                Obj.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }
        }
    }
#endif
}
[System.Serializable]
public class Items
{
    [FoldoutGroup("$TypeName")]
    public TypeEnemy TypeEnemy;

    [FoldoutGroup("$TypeName")]
    public ECharacterType TypeName;

    [FoldoutGroup("$TypeName")]
    public EnemyBaseElement enemyBaseElement;

}
public enum TypeEnemy
{
    NONE = -1,
    Soldier,
    Boss,
}