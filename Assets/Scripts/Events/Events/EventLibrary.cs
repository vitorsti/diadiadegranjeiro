namespace EventProperties
{
    public enum GameEventType
    {
        Consequence,
        Daily,
        Random,
        SubConsequence,
        MegaConsequence
    }

    public enum Wheather
    {
        Sunny,
        Rainy,
        Cloudy,
        Any
    }

    public enum Region
    {
        Any = 0,
        Farm = 1,
        House = 2,
        LockerRoom = 3,
        Generator = 4,
        Heater = 5,
        Composter = 6,
        WaterTankA = 7,
        WaterTankB = 8,
        SiloA = 9,
        SiloB = 10
    }

    public enum EventDuration
    {
        RunTime,
        SixHours = 6,
        EightHours = 8,
        TenHours = 10,
        HalfDay = 12,
        Day = 24
    }
}