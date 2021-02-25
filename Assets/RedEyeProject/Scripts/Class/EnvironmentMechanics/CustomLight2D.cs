using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class CustomLight2D : MonoBehaviour
{
    public Color ShadowColor;
    [HideInInspector]
    public Light2D LightComponent;
    void Start()
    {
        LightComponent = gameObject.GetComponent<Light2D>();
    }
}
