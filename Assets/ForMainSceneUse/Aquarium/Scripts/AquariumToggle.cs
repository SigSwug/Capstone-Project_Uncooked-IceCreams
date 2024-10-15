using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumToggle : MonoBehaviour
{
    public GameObject aquarium;
    public GameObject aquariumCover;

    void Start()
    {
        
    }

    public void AquariumCoverToggle()
    {
        if (aquarium.activeInHierarchy == true)
        {
            aquarium.SetActive(false);
            aquariumCover.SetActive(true);
        }
        else if (aquarium.activeInHierarchy == false)
        {
            aquarium.SetActive(true);
            aquariumCover.SetActive(false);
        }
    }
}
