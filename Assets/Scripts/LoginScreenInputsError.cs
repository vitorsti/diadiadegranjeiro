using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreenInputsError : MonoBehaviour
{
    public Image inputFieldError, dropDownError;
    public float fadeRate;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            inputFieldError.color = Color.red;
            inputFieldError.gameObject.SetActive(false);
            dropDownError.color = Color.red;
            dropDownError.gameObject.SetActive(false);
        }
    }

    public void StartFlicker(bool drop, string input)
    {
        if (!drop && input == "")
        {
            StartCoroutine(FlickerImage(true, inputFieldError));
            StartCoroutine(FlickerImage(true, dropDownError));
        }
        if (drop && input == "")
        {
            StartCoroutine(FlickerImage(true, inputFieldError));
        }
        if (!drop && input != "")
        {
            StartCoroutine(FlickerImage(true, dropDownError));
        }
    }

    IEnumerator FlickerImage(bool fadeAway, Image img)
    {
        img.gameObject.SetActive(true);
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
            StartCoroutine(StopFlicker(img));
        }

    }

    IEnumerator StopFlicker(Image img)
    {
        yield return new WaitForSeconds(5f);
        img.color = Color.red;
        img.gameObject.SetActive(false);
        StopAllCoroutines();
    }
}
