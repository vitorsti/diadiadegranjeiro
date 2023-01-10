using TMPro;
using UnityEngine;
using DG.Tweening;

public class BtnAnswerScript : MonoBehaviour
{
    public QuestionSelectScript qSS;
    public TextMeshProUGUI _textoPonto;
    public Transform _holder;

    public void TaskOnClick()
    {
        string texto = GetComponent<TextMeshProUGUI>().text;

        if (texto == qSS._sequenciaDasPerguntas[qSS.actualQuestion].itIsCorrect)
        {
            Debug.Log("Acertou!");
            _textoPonto.text = "Acertou!";
            qSS.points++;
            _holder.DOLocalMoveY(1080, 1, false);
        }
        else
        {
            Debug.Log("Errou!");
            _textoPonto.text = "Errou!";
            _holder.DOLocalMoveY(1080, 1, false);

        }
    }
}
