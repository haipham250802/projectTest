using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class CritterFollowElement : MonoBehaviour
{
    public int ID;
    public ECharacterType CritterFollowType;

    public Transform Player;
  //  public SkeletonDataAsset ICON;
    public SpriteRenderer Avatar;

    public int Speed;
    public Vector3 OffSet;
    PolyNavAgent agent;

    private void Start()
    {
        agent = GetComponent<PolyNavAgent>();
        LoadCritterFollow();
        if (CritterFollowType == ECharacterType.NONE)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        agent.SetDestination(Player.position + OffSet);
    }
    public void LoadCritterFollow()
    {
    }
}
