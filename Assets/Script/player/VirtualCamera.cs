using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class VirtualCamera : MonoBehaviour
{
    public float ZoomOutSize;
    public float ZoomStart;
    public float Smoth;

    public  CinemachineVirtualCamera virtualCamera;
    public player player;
    private void Start()
    {
        ZoomStart = virtualCamera.m_Lens.OrthographicSize;
    }
    public void ZoomOut()
    {
       
    }
    private void Update()
    {
        if(player.IsZoomOut)
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, ZoomOutSize, Smoth);
        if(player.IsZoomIn)
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, ZoomStart, Smoth);
        if (virtualCamera.m_Lens.OrthographicSize >= ZoomOutSize - 0.05)
        {
            player.IsZoomOut = false;
        }
        if (virtualCamera.m_Lens.OrthographicSize <= ZoomStart + 0.05f)
        {
            player.IsZoomIn = false;
        }
    }
}
