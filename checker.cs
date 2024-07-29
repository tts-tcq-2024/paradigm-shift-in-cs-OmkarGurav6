class Checker
{
    static bool batteryIsOk(float temperature, float soc, float chargeRate)
    {
        bool isTemperatureOk = IsTemperatureOk(temperature);
        bool isSocOk = IsSocOk(soc);
        bool isChargeRateOk = IsChargeRateOk(chargeRate);

        return isTemperatureOk && isSocOk && isChargeRateOk;
    }

    static bool IsTemperatureOk(float temperature)
    {
        if (temperature < 0 || temperature > 45)
        {
            Console.WriteLine("Temperature is out of range!");
            return false;
        }
        else if(0 <= temperature && temperature <= 2.25)
        {
            Console.WriteLine("WARNING! Temperature is LOW!");
            return true;
        }
        else if(42.75 <= temperature && temperature <= 45)
        {
            Console.WriteLine("WARNING! Temperature is HIGH!");
            return true;
        }
        return true;
    }

    static bool IsSocOk(float soc)
    {
        if (soc < 20 || soc > 80)
        {
            Console.WriteLine("State of Charge is out of range!");
            return false;
        }
        else if(20 <= soc && soc <= 24)
        {
            Console.WriteLine("WARNING! State of Charge is LOW!");
            return true;
        }
        else if(76 <= soc && soc <= 80)
        {
            Console.WriteLine("WARNING! State of Charge is HIGH!");
            return true;
        }
        return true;
    }

    static bool IsChargeRateOk(float chargeRate)
    {
        if (chargeRate > 0.8)
        {
            Console.WriteLine("Charge Rate is out of range!");
            return false;
        }
        else if(0 <= chargeRate && chargeRate <= 0.04)
        {
            Console.WriteLine("WARNING! Charge Rate is LOW!");
            return true;
        }
        else if(0.76 <= chargeRate && chargeRate <= 0.80)
        {
            Console.WriteLine("WARNING! Charge Rate is HIGH!");
            return true;
        }
        return true;
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
        ExpectTrue(batteryIsOk(25, 70, 0.7));
        ExpectFalse(batteryIsOk(50, 85, 0.0));

        // Testing for low warning
        ExpectTrue(batteryIsOk(2, 70, 0.7));
        ExpectTrue(batteryIsOk(25, 22, 0.7));
        ExpectTrue(batteryIsOk(25, 70, 0.02));

        // Testing for high warning
        ExpectTrue(batteryIsOk(43, 70, 0.7));
        ExpectTrue(batteryIsOk(25, 78, 0.7));
        ExpectTrue(batteryIsOk(25, 70, 0.78));

        // Testing for low-high combination warning
        ExpectTrue(batteryIsOk(2, 78, 0.78));
        ExpectTrue(batteryIsOk(25, 78, 0.02));
        ExpectTrue(batteryIsOk(2, 70, 0.78));

        // Testing for all low warning
        ExpectTrue(batteryIsOk(1, 21, 0.03));

        // Testing for all high warning
        ExpectTrue(batteryIsOk(44, 79, 0.79));
        
        Console.WriteLine("All ok");
        return 0;
    }
}
