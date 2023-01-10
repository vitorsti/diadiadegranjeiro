using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TutorialManager))]
public class TutorialCustomEditor : Editor
{
    int index = 0;
    string newText;

    bool enableEditor = true;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //index = 0;

        TutorialManager tutorialManager = (TutorialManager)target;

        enableEditor = EditorGUILayout.Toggle("Enable Editor", enableEditor);

        if (enableEditor)
        {

            if (tutorialManager.tutorialsTexts.Count > 0)
            {
                GUILayout.Label("Texto atual do: " + tutorialManager.tutorialsTexts[index].gameObject.name);
                EditorGUILayout.TextArea(tutorialManager.tutorialsTexts[index].text);
                GUILayout.Label("Insira o novo texto abaixo para substituir: " + tutorialManager.tutorialsTexts[index].gameObject.name);
                newText = EditorGUILayout.TextArea(newText);
            }

            //newText = EditorGUILayout.TextArea(newText);

            GUILayout.BeginHorizontal();
            if (tutorialManager.tutorialsTexts.Count > 0)
            {
                if (GUILayout.Button("<"))
                {
                    if (index == 0)
                    {
                        index = 0;
                        return;
                    }
                    else
                        index--;
                }

                if (GUILayout.Button(">"))
                {
                    if (index >= (tutorialManager.tutorialsTexts.Count - 1))
                    {
                        index = tutorialManager.tutorialsTexts.Count - 1;
                        return;
                    }
                    else
                        index++;
                }

                if (GUILayout.Button("Aplicar"))
                {
                    tutorialManager.tutorialsTexts[index].text = newText;
                }
            }



            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            if (GUILayout.Button("Find All Tutorial Texts"))
            {
                tutorialManager.FindAllTutorialText();
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Find All Tutorials"))
            {
                tutorialManager.FindAllTutorials();
            }
        }


    }
}
