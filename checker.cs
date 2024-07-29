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
        TestBatteryCondition(batteryIsOk(25, 70, 0.7F), true);
        TestBatteryCondition(batteryIsOk(50, 85, 0.0F), false);

        // Testing for low warning
        TestBatteryCondition(batteryIsOk(2, 70, 0.7F), true);
        TestBatteryCondition(batteryIsOk(25, 22, 0.7F), true);
        TestBatteryCondition(batteryIsOk(25, 70, 0.02F), true);

        // Testing for high warning
        TestBatteryCondition(batteryIsOk(43, 70, 0.7F), true);
        TestBatteryCondition(batteryIsOk(25, 78, 0.7F), true);
        TestBatteryCondition(batteryIsOk(25, 70, 0.78F), true);

        // Testing for low-high combination warning
        TestBatteryCondition(batteryIsOk(2, 78, 0.78F), true);
        TestBatteryCondition(batteryIsOk(25, 78, 0.02F), true);
        TestBatteryCondition(batteryIsOk(2, 70, 0.78F), true);

        // Testing for all low warning
        TestBatteryCondition(batteryIsOk(1, 21, 0.03F), true);

        // Testing for all high warning
        TestBatteryCondition(batteryIsOk(44, 79, 0.79F), true);
    }

    static void TestBatteryCondition(bool actualResult, bool expectedResult)
    {
        if (actualResult != expectedResult)
        {
            string expected = expectedResult ? "true" : "false";
            string actual = !expectedResult ? "false" : "true";
            Console.WriteLine($"Expected {expected}, but got {actual}");
            Environment.Exit(1);
        }
    }

    static bool batteryIsOk(float temperature, float soc, float chargeRate)
    {
        return CheckCondition(temperature, IsTemperatureOk) &&
               CheckCondition(soc, IsSocOk) &&
               CheckCondition(chargeRate, IsChargeRateOk);
    }

    static bool CheckCondition(float value, Func<float, (bool, string)> checkFunc)
    {
        var (isOk, message) = checkFunc(value);
        if (!isOk)
        {
            Console.WriteLine($"{GetConditionName(checkFunc)} is out of range!");
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
        return temperature < 0 || temperature > 45
            ? (false, null)
            : temperature <= 2.25
                ? (true, "WARNING! Temperature is LOW!")
                : temperature >= 42.75
                    ? (true, "WARNING! Temperature is HIGH!")
                    : (true, null);
    }

    static (bool, string) IsSocOk(float soc)
    {
        return soc < 20 || soc > 80
            ? (false, null)
            : soc <= 24
                ? (true, "WARNING! State of Charge is LOW!")
                : soc >= 76
                    ? (true, "WARNING! State of Charge is HIGH!")
                    : (true, null);
    }

    static (bool, string) IsChargeRateOk(float chargeRate)
    {
        return chargeRate > 0.8
            ? (false, null)
            : chargeRate <= 0.04
                ? (true, "WARNING! Charge Rate is LOW!")
                : chargeRate >= 0.76
                    ? (true, "WARNING! Charge Rate is HIGH!")
                    : (true, null);
    }

    static string GetConditionName(Func<float, (bool, string)> checkFunc)
    {
        return checkFunc.Method.Name.Replace("Is", "").Replace("Ok", "").Replace("Condition", "");
    }
}
