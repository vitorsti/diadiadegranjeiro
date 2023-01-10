using UnityEngine;
using UnityEditor;
using System.Collections;
using EventProperties;

[CustomEditor(typeof(RegionValuesContainer))]
public class RegionValuesContainerEditor : Editor
{
    Region region;
    //RegionValuesContainer regionData;
    string individual, all;
    string[] sets = new string[4] { "Set Helth", "Set MaxHealth", "Set Gap", "Set Health Modify" };
    int index = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RegionValuesContainer regionContainer = (RegionValuesContainer)target;

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        region = (Region)EditorGUILayout.EnumPopup(region);
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
        GUILayout.Label(sets[index]);
        if (GUILayout.Button(">"))
        {

            if (index >= (sets.Length - 1))
            {
                index = sets.Length - 1;
                return;
            }
            else
                index++;
        }

        individual = EditorGUILayout.TextArea(individual);

        if (GUILayout.Button("apply"))
        {
            switch (index)
            {
                case 0:
                    regionContainer.SetHealth(region.ToString(), float.Parse(individual));
                    break;
                case 1:
                    regionContainer.SetMaxHealth(region.ToString(), float.Parse(individual));
                    break;
                case 2:
                    regionContainer.SetRegionGap(region.ToString(), float.Parse(individual));
                    break;
                case 3:
                    regionContainer.SetHealthModfy(region.ToString(), float.Parse(individual));
                    break;
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();

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
        GUILayout.Label(sets[index]);
        if (GUILayout.Button(">"))
        {

            if (index >= (sets.Length - 1))
            {
                index = sets.Length - 1;
                return;
            }
            else
                index++;
        }

        all = EditorGUILayout.TextArea(all);

        if (GUILayout.Button("apply"))
        {

            switch (index)
            {
                case 0:
                    for (int i = 0; i < regionContainer.datas.Length; i++)
                    {

                        regionContainer.datas[i].health = float.Parse(all);

                    }
                    break;
                case 1:
                    for (int i = 0; i < regionContainer.datas.Length; i++)
                    {

                        regionContainer.datas[i].maxHealth = float.Parse(all);

                    }
                    break;
                case 2:
                    for (int i = 0; i < regionContainer.datas.Length; i++)
                    {

                        regionContainer.datas[i].regionGap = float.Parse(all);

                    }
                    break;
                case 3:
                    for (int i = 0; i < regionContainer.datas.Length; i++)
                    {

                        regionContainer.datas[i].healthModify = float.Parse(all);

                    }
                    break;

            }
        }


        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        if (GUILayout.Button("Max Health"))
        {
            for (int i = 0; i < regionContainer.datas.Length; i++)
            {

                regionContainer.datas[i].health = regionContainer.datas[i].maxHealth;
                regionContainer.datas[i].isHealthy = 1;

            }
            Debug.Log("All regions are now with max health");
        }

        GUILayout.Space(5);


    }
}
