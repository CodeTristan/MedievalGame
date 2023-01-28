using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


public class ColorChange : MonoBehaviour
{
    public IEnumerator ChangeColor(float time,Gradient gradient, Image image, SpriteRenderer sprite,bool reverse)
    {
        Color c;
        float elapsedTime = 0.0f;
        while (elapsedTime < time)
        {
            yield return new YieldInstruction();
            elapsedTime += Time.unscaledDeltaTime;
            if (reverse)
            {
                c = gradient.Evaluate(Mathf.Clamp01(1-(elapsedTime / time)));
            }
            else
            {
                c = gradient.Evaluate(Mathf.Clamp01(elapsedTime / time));
            }
            if (sprite==null)
            {
                image.color = c;
            }
            else
            {
                sprite.color = c;
            }
        }
        
    }
}
