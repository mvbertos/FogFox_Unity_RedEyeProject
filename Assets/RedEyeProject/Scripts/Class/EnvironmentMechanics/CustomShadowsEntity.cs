using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomShadowsEntity : MonoBehaviour
{
    public Transform ShadowsParent;
    public SpriteRenderer Renderer;
    public int ShadowLayerOrder = 2;
    public AnimationCurve Curve;
    //public Color ShadowColor; can be used as default shadow in the future
    private Dictionary<GameObject, SpriteRenderer> m_InstantiatedRenderer = new Dictionary<GameObject, SpriteRenderer>();


    public void CreateShadow(GameObject whoCalledMethod, Color color)
    {
        SpriteRenderer shadowInstance = Instantiate<SpriteRenderer>(Renderer, ShadowsParent);
        shadowInstance.color = color;
        shadowInstance.flipY = true;
        shadowInstance.sortingOrder = ShadowLayerOrder;
        m_InstantiatedRenderer.Add(whoCalledMethod, shadowInstance);
    }

    public IEnumerator UpdateDynamicShadow()
    {
        do
        {
            foreach (GameObject lightSource in m_InstantiatedRenderer.Keys)
            {
                if (m_InstantiatedRenderer.TryGetValue(lightSource, out SpriteRenderer renderer))
                {
                    Vector3 position = lightSource.transform.position;
                    position.Normalize();
                    float rot_z = (Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg) - 90;
                    renderer.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
                    float sinPosition = Mathf.Sin(rot_z * 0.0096f) * 0.9f;
                    renderer.transform.localPosition = rot_z < 0 ? new Vector3((sinPosition * -0.5f) * -1, (sinPosition * -0.5f) * -1, 0) : new Vector3((sinPosition * -0.5f), (sinPosition * -0.5f), 0);
                    
                }
            }
            yield return new WaitForEndOfFrame();
        } while (true);
    }
}
