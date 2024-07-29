using System;

class Checker
{
    // Define configurations for warning tolerances
    static readonly WarningConfig TemperatureConfig = new WarningConfig(45, 0);
    static readonly WarningConfig SocConfig = new WarningConfig(80, 20);
    static readonly WarningConfig ChargeRateConfig = new WarningConfig(0.8f, 0);

    static bool batteryIsOk(float temperature, float soc, float chargeRate)
    {
        bool isTemperatureOk = CheckParameter(temperature, TemperatureConfig, "Temperature");
        bool isSocOk = CheckParameter(soc, SocConfig, "State of Charge");
        bool isChargeRateOk = CheckParameter(chargeRate, ChargeRateConfig, "Charge Rate");

        return isTemperatureOk && isSocOk && isChargeRateOk;
    }

    static bool CheckParameter(float value, WarningConfig config, string paramName)
    {
        bool isOk = true;

        if (value < config.Min || value > config.Max)
        {
            Console.WriteLine($"{paramName} is out of range!");
            isOk = false;
        }
        else
        {
            CheckWarning(value, config, paramName);
        }

        return isOk;
    }

    static void CheckWarning(float value, WarningConfig config, string paramName)
    {
        if (value <= config.Min + config.WarningTolerance)
        {
            Console.WriteLine($"Warning: {paramName} is approaching the lower limit!");
        }
        else if (value => config.Max - config.WarningTolerance)
        {
            Console.WriteLine($"Warning: {paramName} is approaching the upper limit!");
        }
    }

    static void ExpectTrue(bool expression)
    {
        if (!expression)
        {
            Console.WriteLine("Expected true, but got false");
            Environment.Exit(1);
        }
    }

    static void ExpectFalse(bool expression)
    {
        if (expression)
        {
            Console.WriteLine("Expected false, but got true");
            Environment.Exit(1);
        }
    }

    static int Main()
    {
        ExpectTrue(batteryIsOk(25, 70, 0.7f));
        ExpectFalse(batteryIsOk(50, 85, 0.0f));
     
        //Testing for low warning
        ExpectTrue(batteryIsOk(2, 70, 0.7F));
        ExpectTrue(batteryIsOk(25, 22, 0.7F));
        ExpectTrue(batteryIsOk(25, 70, 0.02F));

        // Testing for high warning
        ExpectTrue(batteryIsOk(43, 70, 0.7F));
        ExpectTrue(batteryIsOk(25, 78, 0.7F));
        ExpectTrue(batteryIsOk(25, 70, 0.78F));

        // Testing for low-high combination warning
        ExpectTrue(batteryIsOk(2, 78, 0.78F));
        ExpectTrue(batteryIsOk(25, 78, 0.02F));
        ExpectTrue(batteryIsOk(2, 70, 0.78F));

        // Testing for all low warning
        ExpectTrue(batteryIsOk(1, 21, 0.03F));

        // Testing for all high warning
        ExpectTrue(batteryIsOk(44, 79, 0.79F));
     
        Console.WriteLine("All ok");
        return 0;
    }
}

class WarningConfig
{
    public float Max { get; }
    public float Min { get; }
    public float WarningTolerance { get; }

    public WarningConfig(float max, flaot min)
    {
        Max = max;
        Min = min;
        WarningTolerance = CalculateWarningTolerance(max);
    }

    private float CalculateWarningTolerance(float max)
    {
        return max * 0.05f; // 5% of the upper-limit
    }
}


// using System;

// class Checker
// {
//     static int Main()
//     {
//         RunTests();
//         Console.WriteLine("All tests passed.");
//         return 0;
//     }

//     static void RunTests()
//     {
//         ExpectTrue(batteryIsOk(25, 70, 0.7F));
//         ExpectFalse(batteryIsOk(50, 85, 0.0F));

//         // Testing for low warning
//         ExpectTrue(batteryIsOk(2, 70, 0.7F));
//         ExpectTrue(batteryIsOk(25, 22, 0.7F));
//         ExpectTrue(batteryIsOk(25, 70, 0.02F));

//         // Testing for high warning
//         ExpectTrue(batteryIsOk(43, 70, 0.7F));
//         ExpectTrue(batteryIsOk(25, 78, 0.7F));
//         ExpectTrue(batteryIsOk(25, 70, 0.78F));

//         // Testing for low-high combination warning
//         ExpectTrue(batteryIsOk(2, 78, 0.78F));
//         ExpectTrue(batteryIsOk(25, 78, 0.02F));
//         ExpectTrue(batteryIsOk(2, 70, 0.78F));

//         // Testing for all low warning
//         ExpectTrue(batteryIsOk(1, 21, 0.03F));

//         // Testing for all high warning
//         ExpectTrue(batteryIsOk(44, 79, 0.79F));
//     }

//     static bool batteryIsOk(float temperature, float soc, float chargeRate)
//     {
//         return IsConditionOk(temperature, IsTemperatureOk, "Temperature") &&
//                IsConditionOk(soc, IsSocOk, "State of Charge") &&
//                IsConditionOk(chargeRate, IsChargeRateOk, "Charge Rate");
//     }

//     static bool IsConditionOk(float value, Func<float, (bool, string)> checkFunc, string name)
//     {
//         var (isOk, message) = checkFunc(value);
//         if (!isOk)
//         {
//             Console.WriteLine($"{name} is out of range!");
//             return false;
//         }
//         if (message != null)
//         {
//             Console.WriteLine(message);
//         }
//         return true;
//     }

//     static (bool, string) IsTemperatureOk(float temperature)
//     {
//         if (temperature < 0 || temperature > 45)
//         {
//             return (false, null);
//         }

//         return temperature <= 2.25
//             ? (true, "WARNING! Temperature is LOW!")
//             : (temperature >= 42.75
//                 ? (true, "WARNING! Temperature is HIGH!")
//                 : (true, null));
//     }

//     static (bool, string) IsSocOk(float soc)
//     {
//         if (soc < 20 || soc > 80)
//         {
//             return (false, null);
//         }

//         return soc <= 24
//             ? (true, "WARNING! State of Charge is LOW!")
//             : (soc >= 76
//                 ? (true, "WARNING! State of Charge is HIGH!")
//                 : (true, null));
//     }

//     static (bool, string) IsChargeRateOk(float chargeRate)
//     {
//         if (chargeRate > 0.8)
//         {
//             return (false, null);
//         }
        
//         return chargeRate <= 0.04
//             ? (true, "WARNING! Charge Rate is LOW!")
//             : (chargeRate >= 0.76
//                 ? (true, "WARNING! Charge Rate is HIGH!")
//                 : (true, null));
//     }

//     static void ExpectTrue(bool expression)
//     {
//         if (!expression)
//         {
//             Console.WriteLine("Expected true, but got false");
//             Environment.Exit(1);
//         }
//     }

//     static void ExpectFalse(bool expression)
//     {
//         if (expression)
//         {
//             Console.WriteLine("Expected false, but got true");
//             Environment.Exit(1);
//         }
//     }
// }
