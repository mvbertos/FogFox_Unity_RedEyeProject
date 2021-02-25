using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CustomShadowsController : MonoBehaviour
{
    void Start()
    {
        CustomShadowsEntity[] m_ShadowEntity = FindObjectsOfType<CustomShadowsEntity>();
        CustomLight2D[] m_LightSources = FindObjectsOfType<CustomLight2D>();

        foreach (CustomShadowsEntity entity in m_ShadowEntity)
        {
            foreach (CustomLight2D item in m_LightSources)
            {
                entity.CreateShadow(item.gameObject, item.ShadowColor);
            }

            StartCoroutine(entity.UpdateDynamicShadow());
        }
    }
}
