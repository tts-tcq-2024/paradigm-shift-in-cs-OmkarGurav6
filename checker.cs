using System;
using System.Diagnostics;
namespace paradigm_shift_csharp
{
class Checker
{
    static bool batteryIsOk(float temperature, float soc, float chargeRate) {
        if(! (isTempOutofRange(temperature) || isSocOutofRange(soc) || isChargeRateOutofRange(chargeRate)) )
            return true;
        return false;
    }

    static bool isTempOutofRange(float temperature){
        if(temperature < 0 || temperature > 45)
        {
            Console.WriteLine("Temperature is out of range!");
            return true;
        }
        return false;
    }

    static bool isSocOutofRange(float soc){
        if(soc < 20 || soc > 80)
        {
            Console.WriteLine("State of Charge is out of range!");
            return true;
        }
        return false; 
    }

    static bool isChargeRateOutofRange(float chargeRate){
        if(chargeRate > 0.8)
        {
            Console.WriteLine("Charge Rate is out of range!");
            return true;
        }
        return false; 
    }

    static void ExpectResult(bool expression) {
        if(expression) {
            Console.WriteLine("Got true");
            Environment.Exit(1);
        }
        else
        {
            Console.WriteLine("Got false");
            Environment.Exit(1);
        }
    }
    
    static int Main() {
        ExpectResult(batteryIsOk(25, 70, 0.7f));
        ExpectResult(batteryIsOk(50, 85, 0.0f));
        Console.WriteLine("All ok");
        return 0;
    }
    
}
}
