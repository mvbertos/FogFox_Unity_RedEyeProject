using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomShadowsEntity : MonoBehaviour
{
    public Transform ShadowsParent;
    public SpriteRenderer Renderer;
    //public Color ShadowColor; can be used as default shadow in the future
    private Dictionary<GameObject, SpriteRenderer> m_InstantiatedRenderer = new Dictionary<GameObject, SpriteRenderer>();


    public void CreateShadow(GameObject whoCalledMethod, Color color)
    {
        SpriteRenderer shadowInstance = Instantiate<SpriteRenderer>(Renderer, ShadowsParent);
        shadowInstance.color = color;
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
                    print(lightSource.name + "/" + renderer.name);
                }
            }
            yield return new WaitForEndOfFrame();
        } while (true);
    }
}
