using System;

class Checker
{
    static int Main()
    {
        RunTests();
        Console.WriteLine("All tests passed.");
        return 0;
    }

    static void RunTests()
    {
        ExpectTrue(batteryIsOk(25, 70, 0.7F));
        ExpectFalse(batteryIsOk(50, 85, 0.0F));

        // Testing for low warning
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
    }

    static bool batteryIsOk(float temperature, float soc, float chargeRate)
    {
        return IsConditionOk(temperature, IsTemperatureOk, "Temperature") &&
               IsConditionOk(soc, IsSocOk, "State of Charge") &&
               IsConditionOk(chargeRate, IsChargeRateOk, "Charge Rate");
    }

    static bool IsConditionOk(float value, Func<float, (bool, string)> checkFunc, string name)
    {
        var (isOk, message) = checkFunc(value);
        if (!isOk)
        {
            Console.WriteLine($"{name} is out of range!");
            return false;
        }
        if (message != null)
        {
            Console.WriteLine(message);
        }
        return true;
    }

    static (bool, string) IsTemperatureOk(float temperature)
    {
        if (temperature < 0 || temperature > 45)
        {
            return (false, null);
        }
        if (temperature <= 2.25)
        {
            return (true, "WARNING! Temperature is LOW!");
        }
        if (temperature >= 42.75)
        {
            return (true, "WARNING! Temperature is HIGH!");
        }
        return (true, null);
    }

    static (bool, string) IsSocOk(float soc)
    {
        if (soc < 20 || soc > 80)
        {
            return (false, null);
        }
        if (soc <= 24)
        {
            return (true, "WARNING! State of Charge is LOW!");
        }
        if (soc >= 76)
        {
            return (true, "WARNING! State of Charge is HIGH!");
        }
        return (true, null);
    }

    static (bool, string) IsChargeRateOk(float chargeRate)
    {
        if (chargeRate > 0.8)
        {
            return (false, null);
        }
        if (chargeRate <= 0.04)
        {
            return (true, "WARNING! Charge Rate is LOW!");
        }
        if (chargeRate >= 0.76)
        {
            return (true, "WARNING! Charge Rate is HIGH!");
        }
        return (true, null);
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
}
