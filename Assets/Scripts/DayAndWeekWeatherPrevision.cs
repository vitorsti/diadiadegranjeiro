using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndWeekWeatherPrevision : MonoBehaviour
{
    public int temp, minTemp, maxTemp, days, today;
    public List<int> weekTemp;
    public List<string> weekDays;
    // Start is called before the first frame update
    void Start()
    {
        days = 7;
        today = 0;
        WeekTemperatureGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WeekTemperatureGenerator();
        }

        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i <= days; i++)
            {
                Debug.Log(weekDays[i] + ": " + weekTemp[i] + "°");
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Hoje é: " + weekDays[today] + " " + weekTemp[today] + "°");
            NextDay();
        }

        if(Input.GetKeyDown(KeyCode.N)){
            NextDay();
        }
    }

    public void WeekTemperatureGenerator()
    {
        weekTemp = new List<int>();

        do
        {
            temp = Random.Range(minTemp, maxTemp);
            weekTemp.Add(temp);
        } while (weekTemp.Count < days);

    }

    public void NextDay()
    {
        if (today == 6){
            today = 0;
            WeekTemperatureGenerator();
        }
        else{
            today++;
        }
    }
}
