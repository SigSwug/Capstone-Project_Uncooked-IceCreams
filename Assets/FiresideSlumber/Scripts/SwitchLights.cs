using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchLights : MonoBehaviour
{
    public Texture2D[] darkLightmapDir, darkLightmapColor;
    public Texture2D[] brightLightmapDir, brightLightmapColor;
    public Toggle lightmapToggle;

    private LightmapData[] darkLightmap, brightLightmap;

    void Start()
    {
        // Initialize the dark lightmap data
        List<LightmapData> dlightmap = new List<LightmapData>();
        for (int i = 0; i < darkLightmapDir.Length; i++)
        {
            LightmapData lmdata = new LightmapData
            {
                lightmapDir = darkLightmapDir[i],
                lightmapColor = darkLightmapColor[i]
            };
            dlightmap.Add(lmdata);
        }
        darkLightmap = dlightmap.ToArray();

        // Initialize the bright lightmap data
        List<LightmapData> blightmap = new List<LightmapData>();
        for (int i = 0; i < brightLightmapDir.Length; i++)
        {
            LightmapData lmdata = new LightmapData
            {
                lightmapDir = brightLightmapDir[i],
                lightmapColor = brightLightmapColor[i]
            };
            blightmap.Add(lmdata);
        }
        brightLightmap = blightmap.ToArray();

        // Set up the toggle listener
        if (lightmapToggle != null)
        {
            lightmapToggle.onValueChanged.AddListener(OnToggleChanged);
        }
    }

    private void OnToggleChanged(bool isOn)
    {
        // Switch lightmaps based on toggle state
        LightmapSettings.lightmaps = isOn ? brightLightmap : darkLightmap;
    }
}
