using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestionSelectScript : MonoBehaviour
{
    public TextMeshProUGUI _textoPergunta;
    public TextMeshProUGUI _textoJustificativa;
    public TextMeshProUGUI[] _textoResp;
    public List<NewQuestionScript> _dataBasePerguntas;
    public Transform _holder;
    public GameObject _transition;
    public TextMeshProUGUI _textoPonto;
    public GameObject _voltarMenuBTN;
    public GameObject _proximaPerguntaBTN;

    public int points = 0;

    [HideInInspector]
    public List<NewQuestionScript> _sequenciaDasPerguntas;
    [HideInInspector]
    public List<string> _respostasDaPergunta;
    [HideInInspector]
    public List<string> _sequenciaRespostaDaPergunta;

    public Button[] btns;

    public int actualQuestion = 0;

    private void Start()
    {
        AleatorizarPerguntas();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _textoPergunta.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _textoPergunta.enabled = true;
        }


    }

    void AtualizarPergunta()
    {
        _textoPergunta.text = _sequenciaDasPerguntas[actualQuestion].question;
        string TESTE = _textoPergunta.text.ToString();
        TESTE = TESTE.Replace("\\n", "\n");
        _textoPergunta.text = TESTE;
        _textoJustificativa.text = _sequenciaDasPerguntas[actualQuestion].justification;

        for (int i = 0; i < 3; i++)
        {
            _respostasDaPergunta.Add(_sequenciaDasPerguntas[actualQuestion].answers[i]);
        }

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, _respostasDaPergunta.Count - 1);
            _sequenciaRespostaDaPergunta.Add(_respostasDaPergunta[index]);
            _respostasDaPergunta.RemoveAt(index);

            _textoResp[i].text = _sequenciaRespostaDaPergunta[i];

        }

        for (int i = 0; i < 3; i++)
        {
            if (_textoResp[i].text == _sequenciaDasPerguntas[actualQuestion].itIsCorrect)
            {
                Debug.LogError("Resposta correta: " + (1 + i) + " " + _sequenciaDasPerguntas[actualQuestion].itIsCorrect);
            }
        }

        _respostasDaPergunta.Clear();
        _sequenciaRespostaDaPergunta.Clear();
    }

    void AleatorizarPerguntas()
    {
        int cnt = _dataBasePerguntas.Count;

        for (int i = 0; i < cnt; i++)
        {
            int index = Random.Range(0, _dataBasePerguntas.Count - 1);
            _sequenciaDasPerguntas.Add(_dataBasePerguntas[index]);
            _dataBasePerguntas.RemoveAt(index);
        }

        AtualizarPergunta();
    }

    public void ProximaPergunta()
    {
        StartCoroutine(Fade());


    }

    public void VoltarParaMenu()
    {
        //StartCoroutine(FadeMenu());
        Destroy(this.gameObject);
    }

    IEnumerator Fade()
    {
        //GameObject go = Instantiate(_transition, this.transform.position, this.transform.rotation, this.transform);
        _transition.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        actualQuestion++;
        _holder.DOLocalMoveY(0, 0.1f, false);

        if (actualQuestion < _sequenciaDasPerguntas.Count)
        {
            AtualizarPergunta();
        }
        else
        {
            if (points == 0)
            {
                _textoJustificativa.text = "Que pena!\n Você não acertou nenhuma das pergunta, tente mais um pouco, até ter entendido todos os procedimentos!";
            }
            else if (points == 1)
            {
                _textoJustificativa.text = "Você acertou uma pergunta, está indo bem!\nTente novamente até ter entendido todos os procedimentos!";
                if (!DailyQuizManager.sameDay && PlayerPrefs.GetInt("1p", 0) == 0)
                {
                    RewardSingle();
                    PlayerPrefs.SetInt("1p", 1);
                }
            }
            else if (points == 2)
            {
                _textoJustificativa.text = "Você acertou duas perguntas, está quase lá!\nTente novamente até ter entendido todos os procedimentos!";
                if (!DailyQuizManager.sameDay && PlayerPrefs.GetInt("2p", 0) == 0)
                {
                    if (PlayerPrefs.GetInt("1p") == 1)
                        RewardSingle();
                    else
                        RewardTotal();

                    PlayerPrefs.SetInt("2p", 1);
                }
            }
            else if (points == 3)
            {
                _textoJustificativa.text = "Você acertou mais da metade das perguntas, mandou bem!\nTente novamente até ter entendido todos os procedimentos!";
                if (!DailyQuizManager.sameDay && PlayerPrefs.GetInt("3p", 0) == 0)
                {
                    if (PlayerPrefs.GetInt("2p") == 1)
                        RewardSingle();
                    else
                        RewardTotal();

                    PlayerPrefs.SetInt("3p", 1);
                }
            }
            else if (points == 4)
            {
                _textoJustificativa.text = "Você é muito bom!\nQuase conseguiu acertar todas, está de parabéns!\nTente mais um pouquinho e você vai conseguir acertar todas!";
                if (!DailyQuizManager.sameDay && PlayerPrefs.GetInt("4p", 0) == 0)
                {
                    if (PlayerPrefs.GetInt("3p") == 1)
                        RewardSingle();
                    else
                        RewardTotal();

                    PlayerPrefs.SetInt("4p", 1);
                }
            }
            else if (points == 5)
            {
                _textoJustificativa.text = "Você é incrível!\nRealmente entende sobre o assunto, e mostrou isso acertando todas as perguntas!";
                if (!DailyQuizManager.sameDay && PlayerPrefs.GetInt("5p", 0) == 0)
                {
                    if (PlayerPrefs.GetInt("4p") == 1)
                        RewardSingle();
                    else
                        RewardTotal();

                    PlayerPrefs.SetInt("5p", 1);
                }
            }

            _textoPonto.text = "[ " + points + " / 5 ]";
            _voltarMenuBTN.SetActive(true);
            _proximaPerguntaBTN.SetActive(false);
            _holder.DOKill(true);
            _holder.DOLocalMoveY(1080, 0.1f, false);
        }
        yield return new WaitForSeconds(0.4f);
        //Destroy(go);
        _transition.SetActive(false);
    }

    IEnumerator FadeMenu()
    {
        _transition.SetActive(true);
        yield return new WaitForSeconds(2f);
        //SceneManager.LoadScene("_menu_main");
        Destroy(this.gameObject);
        _transition.SetActive(false);
    }

    void RewardTotal()
    {

        MoneyManager.AddMoney("cash", 100 * points);
    }

    void RewardSingle()
    {
        MoneyManager.AddMoney("cash", 100);
    }

    public void Close()
    {
        Destroy(this.gameObject);
    }
}
