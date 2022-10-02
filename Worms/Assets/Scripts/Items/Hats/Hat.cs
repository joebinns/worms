using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : Item
{
    [HideInInspector] public HatSettings hatSettings;
    
    private void Awake()
    {
        if (itemSettings is HatSettings)
        {
            hatSettings = itemSettings as HatSettings;
        }
        
        //id = (itemSettings as HatSettings).id;
    }
}
