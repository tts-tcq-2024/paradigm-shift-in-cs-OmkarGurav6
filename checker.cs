using System;

class Checker
{
    // Main entry point for the application
    static int Main()
    {
        // Test cases to validate battery status
        RunTests();
        Console.WriteLine("All tests passed.");
        return 0;
    }

    // Runs various test cases
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

    // Checks if the battery conditions are okay
    static bool batteryIsOk(float temperature, float soc, float chargeRate)
    {
        return IsTemperatureOk(temperature) && IsSocOk(soc) && IsChargeRateOk(chargeRate);
    }

    // Evaluates temperature condition
    static bool IsTemperatureOk(float temperature)
    {
        if (temperature < 0 || temperature > 45)
        {
            Console.WriteLine("Temperature is out of range!");
            return false;
        }

        if (temperature <= 2.25 || temperature >= 42.75)
        {
            Console.WriteLine("WARNING! Temperature is " + (temperature <= 2.25 ? "LOW" : "HIGH") + "!");
            return true;
        }

        return true;
    }

    // Evaluates state of charge condition
    static bool IsSocOk(float soc)
    {
        if (soc < 20 || soc > 80)
        {
            Console.WriteLine("State of Charge is out of range!");
            return false;
        }

        if (soc <= 24 || soc >= 76)
        {
            Console.WriteLine("WARNING! State of Charge is " + (soc <= 24 ? "LOW" : "HIGH") + "!");
            return true;
        }

        return true;
    }

    // Evaluates charge rate condition
    static bool IsChargeRateOk(float chargeRate)
    {
        if (chargeRate > 0.8)
        {
            Console.WriteLine("Charge Rate is out of range!");
            return false;
        }

        if (chargeRate <= 0.04 || chargeRate >= 0.76)
        {
            Console.WriteLine("WARNING! Charge Rate is " + (chargeRate <= 0.04 ? "LOW" : "HIGH") + "!");
            return true;
        }

        return true;
    }

    // Checks if an expression is true, exits with error if not
    static void ExpectTrue(bool expression)
    {
        if (!expression)
        {
            Console.WriteLine("Expected true, but got false");
            Environment.Exit(1);
        }
    }

    // Checks if an expression is false, exits with error if not
    static void ExpectFalse(bool expression)
    {
        if (expression)
        {
            Console.WriteLine("Expected false, but got true");
            Environment.Exit(1);
        }
    }
}
