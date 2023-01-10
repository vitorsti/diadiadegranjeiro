using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scri_QuoteBehaviour : MonoBehaviour
{
    public List<CuriosityQuote> quoteObj;
    public TextMeshProUGUI _uiText;

    private void Start()
   {
       int idx = Random.Range(0, quoteObj.Count);
       _uiText.text = quoteObj[idx].quoteText;
   }

}
