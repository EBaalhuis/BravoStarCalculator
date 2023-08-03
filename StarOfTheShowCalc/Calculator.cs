using System.Runtime.InteropServices;

// In SDK-style projects such as this one, several assembly attributes that were historically
// defined in this file are now automatically added during build and populated with
// values defined in project properties. For details of which attributes are included
// and how to customise this process see: https://aka.ms/assembly-info-properties 


// Setting ComVisible to false makes the types in this assembly not visible to COM
// components.  If you need to access a type in this assembly from COM, set the ComVisible
// attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM.

[assembly: Guid("0845c2a3-8634-4c97-b47d-0f6e7e0631b0")]

public static class Calculator
{
    public static double Calculate(int earth, int ice, int lightning, int pulseEarthIce=0, int pulseEarthLightning=0, int pulseIceLightning=0, int deckSize=60, int draw=4)
    {
        if (deckSize < earth + ice + lightning + pulseEarthIce + pulseEarthLightning + pulseIceLightning) return 0;

        if (draw > deckSize) draw = deckSize;

        int rest = deckSize - earth - ice - lightning - pulseEarthIce - pulseEarthLightning - pulseIceLightning;
        return (CombinationsWithoutPulse(earth, ice, lightning, rest, deckSize, draw)
              + CombinationsWithSinglePulse(pulseEarthIce, lightning, otherPulses: pulseEarthLightning + pulseIceLightning, deckSize, draw)
              + CombinationsWithSinglePulse(pulseEarthLightning, ice, otherPulses: pulseEarthIce + pulseIceLightning, deckSize, draw)
              + CombinationsWithSinglePulse(pulseIceLightning, earth, otherPulses: pulseEarthLightning + pulseEarthIce, deckSize, draw)
              + CombinationsWithDoublePulse(pulseEarthIce, pulseEarthLightning, pulseIceLightning, deckSize, draw)
              + CombinationsWithTriplePulse(pulseEarthIce, pulseEarthLightning, pulseIceLightning, deckSize, draw))
              / (double)nCr(deckSize, draw);
    }

    private static double CombinationsWithoutPulse(int earth, int ice, int lightning, int rest, int deckSize=60, int draw=4)
    {
        return nCr(earth, 1) * nCr(ice, 1) * nCr(lightning, 1) * nCr(rest, draw - 3)
              + nCr(earth, 2) * nCr(ice, 1) * nCr(lightning, 1) * nCr(rest, draw - 4)
              + nCr(earth, 1) * nCr(ice, 2) * nCr(lightning, 1) * nCr(rest, draw - 4)
              + nCr(earth, 1) * nCr(ice, 1) * nCr(lightning, 2) * nCr(rest, draw - 4)
              + nCr(earth, 2) * nCr(ice, 2) * nCr(lightning, 1) * nCr(rest, draw - 5)
              + nCr(earth, 2) * nCr(ice, 1) * nCr(lightning, 2) * nCr(rest, draw - 5)
              + nCr(earth, 1) * nCr(ice, 2) * nCr(lightning, 2) * nCr(rest, draw - 5)
              + nCr(earth, 3) * nCr(ice, 1) * nCr(lightning, 1) * nCr(rest, draw - 5)
              + nCr(earth, 1) * nCr(ice, 3) * nCr(lightning, 1) * nCr(rest, draw - 5)
              + nCr(earth, 1) * nCr(ice, 1) * nCr(lightning, 3) * nCr(rest, draw - 5);
    }

    private static double CombinationsWithSinglePulse(int pulse, int otherElement, int otherPulses=0, int deckSize=60, int draw=4)
    {
        if (pulse == 0) return 0;
        int rest = deckSize - pulse - otherElement - otherPulses;
        return nCr(otherElement, 1) * nCr(rest, draw - 2)
             + nCr(otherElement, 2) * nCr(rest, draw - 3)
             + nCr(otherElement, 3) * nCr(rest, draw - 4)
             + nCr(otherElement, 4) * nCr(rest, draw - 5);
    }

    private static double CombinationsWithDoublePulse(int pulseEarthIce, int pulseEarthLightning, int pulseIceLightning, int deckSize=60, int draw=4)
    {
        int totalPulses = pulseEarthIce + pulseEarthLightning + pulseIceLightning;
        return nCr(pulseEarthIce, 1) * nCr(pulseEarthLightning, 1) * nCr(deckSize - totalPulses, draw - 2)
             + nCr(pulseEarthIce, 1) * nCr(pulseIceLightning, 1) * nCr(deckSize - totalPulses, draw - 2)
             + nCr(pulseEarthLightning, 1) * nCr(pulseIceLightning, 1) * nCr(deckSize - totalPulses, draw - 2);
    }

    private static double CombinationsWithTriplePulse(int pulseEarthIce, int pulseEarthLightning, int pulseIceLightning, int deckSize = 60, int draw = 4)
    {
        int totalPulses = pulseEarthIce + pulseEarthLightning + pulseIceLightning;
        return nCr(totalPulses, 3) * nCr(deckSize - totalPulses, draw - 3);
    }

    private static double nCr(int n, int r)
    {
        if (r > n) return 0;
        if (r < 0) return 0;
        return nPr(n, r) / Factorial(r);
    }

    private static double nPr(int n, int r)
    {
        return FactorialDivision(n, n - r);
    }

    private static double FactorialDivision(int topFactorial, int divisorFactorial)
    {
        double result = 1;
        for (int i = topFactorial; i > divisorFactorial; i--)
            result *= i;
        return result;
    }

    private static double Factorial(int i)
    {
        if (i <= 1)
            return 1;
        return i * Factorial(i - 1);
    }
}
