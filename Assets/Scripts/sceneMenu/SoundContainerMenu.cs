using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundContainerMenu : MonoBehaviour
{
    public static SoundContainerMenu instance;
    public AudioClip clickSound;

    void Start()
    {
        instance = this;      
    }
}
