using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Introduceți baza sursă (b1) între 2 și 16:");
        int b1 = int.Parse(Console.ReadLine());

        Console.WriteLine("Introduceți baza țintă (b2) între 2 și 16:");
        int b2 = int.Parse(Console.ReadLine());

        if (b1 < 2 || b1 > 16 || b2 < 2 || b2 > 16)
        {
            Console.WriteLine("Bazele trebuie să fie între 2 și 16.");
            return;
        }

        Console.WriteLine($"Introduceți numărul în baza {b1} (cu virgulă fixă, ex: 1A.3F):");
        string inputNumber = Console.ReadLine();

        try
        {
            string result = ConvertFixedPointNumber(inputNumber, b1, b2);
            Console.WriteLine($"Numărul în baza {b2} este: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
    }

    static string ConvertFixedPointNumber(string number, int b1, int b2)
    {
        // Separăm partea întreagă și fracționară
        string[] parts = number.Split('.');
        string integerPart = parts[0];
        string fractionalPart = parts.Length > 1 ? parts[1] : "";

        // Convertim partea întreagă din b1 în b10
        long integerInBase10 = ConvertToBase10(integerPart, b1);

        // Convertim partea fracționară din b1 în b10
        double fractionalInBase10 = ConvertFractionToBase10(fractionalPart, b1);

        // Convertim partea întreagă din b10 în b2
        string integerInBaseB2 = ConvertFromBase10((long)integerInBase10, b2);

        // Convertim partea fracționară din b10 în b2
        string fractionalInBaseB2 = ConvertFractionFromBase10(fractionalInBase10, b2);

        return fractionalPart == "" ? integerInBaseB2 : $"{integerInBaseB2}.{fractionalInBaseB2}";
    }

    static long ConvertToBase10(string number, int baseFrom)
    {
        return Convert.ToInt64(number, baseFrom);
    }

    static double ConvertFractionToBase10(string fraction, int baseFrom)
    {
        double result = 0;
        for (int i = 0; i < fraction.Length; i++)
        {
            int digit = Convert.ToInt32(fraction[i].ToString(), baseFrom);
            result += digit / Math.Pow(baseFrom, i + 1);
        }
        return result;
    }

    static string ConvertFromBase10(long number, int baseTo)
    {
        return Convert.ToString(number, baseTo).ToUpper();
    }

    static string ConvertFractionFromBase10(double fraction, int baseTo)
    {
        StringBuilder result = new StringBuilder();
        int maxPrecision = 10; // numărul maxim de cifre după virgulă
        int count = 0;

        while (fraction > 0 && count < maxPrecision)
        {
            fraction *= baseTo;
            int digit = (int)fraction;
            result.Append(Convert.ToString(digit, baseTo).ToUpper());
            fraction -= digit;
            count++;
        }

        return result.ToString();
    }
}