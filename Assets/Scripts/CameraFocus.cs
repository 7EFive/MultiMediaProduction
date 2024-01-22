using UnityEngine;
using Cinemachine;

public class CameraFocus : MonoBehaviour
{
    //camaer for zoom in zoom out
    [SerializeField]public CinemachineVirtualCamera vCamera;
    // stats of the player gets checked
    public PlayerMain action;
    // zoom in and out minimal and maximal values
    [SerializeField] public float minZoom;
    [SerializeField] public float maxZoom;
    // zoom in and out speed values 
    public float zoomIn;
    public float zoomOut;



    // Update is called once per frame
    // Zoom in that are triggered by the players actions and/or states
    void Update()
    {
        // doubled speed in zoom value on player dead state 
        float zoomGO = zoomIn * 2;
        // cheking if player is charging or dead
        if (action.charging || action.isFinished) 
        {
            // zoom in until min Zoom has reached
            if (vCamera.m_Lens.OrthographicSize > minZoom)
            {
                if (action.isFinished)
                {
                    vCamera.m_Lens.OrthographicSize -= Time.deltaTime * zoomGO;
                }
                else
                {
                    vCamera.m_Lens.OrthographicSize -= Time.deltaTime * zoomIn;
                    action.createChargeParticles();
                }
            }
            else if(vCamera.m_Lens.OrthographicSize <= minZoom)
            {
                vCamera.m_Lens.OrthographicSize = minZoom;
            }
        }
        // zoom out
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
