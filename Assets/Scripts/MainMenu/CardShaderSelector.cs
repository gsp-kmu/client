using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardShaderSelector : MonoBehaviour
{
    public Shader blackAndWhiteShader;

    public void SetAtiveBlackWhiteCardShader(Image card)
    {
        if (blackAndWhiteShader != null)
        {
            Material material = new Material(blackAndWhiteShader);
            card.material = material;
        }
    }
}
