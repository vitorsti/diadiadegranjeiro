using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartFlicker : MonoBehaviour
{
    // Start is called before the first frame update
    
    void OnEnable()
    {
        this.gameObject.GetComponent<Image>().enabled = true;
        StartCoroutine(FlickerImage(true, this.GetComponent<Image>()));
    }

    public void StopFlickerUpdate()
    {
        StartCoroutine(StopFlicker(this.GetComponent<Image>()));
    }

    IEnumerator FlickerImage(bool fadeAway, Image img)
    {
        //img.gameObject.SetActive(true);
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(img.color.r, img.color.g, img.color.b, i);
                yield return null;
            }
            StartCoroutine(FlickerImage(false, img));
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(img.color.r, img.color.g, img.color.b, i);
                yield return null;
            }
            StartCoroutine(FlickerImage(true, img));
            //StartCoroutine(StopFlicker(img));
        }

    }

    IEnumerator StopFlicker(Image img)
    {
        yield return new WaitForSeconds(0.1f);
        img.color = Color.red;
        img.enabled = false;
        StopAllCoroutines();
        this.enabled = false;

    }

}
