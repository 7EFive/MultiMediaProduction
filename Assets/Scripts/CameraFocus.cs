using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField]public CinemachineVirtualCamera vCamera;

    public PlayerMain action;
    [SerializeField] public float minZoom;
    [SerializeField] public float maxZoom;
    public float zoomIn;
    public float zoomOut;



    // Update is called once per frame
    void Update()
    {
        float zoomGO = zoomIn * 2;
        if (action.charging || action.isFinished) 
        {
            
            if (vCamera.m_Lens.OrthographicSize > minZoom)
            {
                if (action.isFinished)
                {
                    vCamera.m_Lens.OrthographicSize -= Time.deltaTime * zoomGO;
                }
                else
                {
                    vCamera.m_Lens.OrthographicSize -= Time.deltaTime * zoomIn;
                }
            }
            else if(vCamera.m_Lens.OrthographicSize <= minZoom)
            {
                vCamera.m_Lens.OrthographicSize = minZoom;
            }
        }
        else
        {
            if(vCamera.m_Lens.OrthographicSize < maxZoom)
            {
                vCamera.m_Lens.OrthographicSize += Time.deltaTime * zoomOut;
            }
            else if(vCamera.m_Lens.OrthographicSize >= maxZoom)
            {
                vCamera.m_Lens.OrthographicSize = maxZoom;
            }
        }
    }
}
