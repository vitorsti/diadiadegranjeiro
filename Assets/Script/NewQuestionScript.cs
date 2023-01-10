using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "New Question", order = 1)]
public class NewQuestionScript : ScriptableObject
{
    [TextArea]public string question;

    [TextArea]public string[] answers;
    [TextArea]public string itIsCorrect;
    [TextArea]public string justification;
}