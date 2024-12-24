using System;
using System.Collections.Generic;
using System.Linq;

public class PrimeNumberCalculator
{
    /// <summary>
    /// Returns the list of prime numbers between two integers (exclusive).
    /// </summary>
    /// <param name="start">The starting integer.</param>
    /// <param name="end">The ending integer.</param>
    /// <returns>A list of prime numbers between the two integers (exclusive).</returns>
    /// <exception cref="ArgumentException">Thrown when start is greater than or equal to end.</exception>
    public List<int> GetPrimesBetween(int start, int end)
    {
        // Validate inputs
        if (start >= end)
            throw new ArgumentException("The start value must be less than the end value.");

        // Ensure boundaries are excluded
        var range = Enumerable.Range(start + 1, end - start - 1);

        // Filter and return prime numbers
        return range.Where(IsPrime).ToList();
    }

    /// <summary>
    /// Determines if a number is prime.
    /// </summary>
    /// <param name="number">The number to check.</param>
    /// <returns>True if the number is prime; otherwise, false.</returns>
    private bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true; // 2 is the only even prime number
        if (number % 2 == 0) return false; // Exclude other even numbers

        var boundary = (int)Math.Sqrt(number);
        for (int i = 3; i <= boundary; i += 2)
        {
            if (number % i == 0) return false;
        }

        return true;
    }
}

// Example usage
public class Program
{
    public static void Main(string[] args)
    {
        var calculator = new PrimeNumberCalculator();

        try
        {
            PrintPrimes(1, 5, calculator);    // Expected: Count 2 (2, 3)
            PrintPrimes(7, 11, calculator);   // Expected: Count 0 (None)
            PrintPrimes(1, 11, calculator);   // Expected: Count 4 (2, 3, 5, 7)
            PrintPrimes(1, 211, calculator);   // Expected: 
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    private static void PrintPrimes(int start, int end, PrimeNumberCalculator calculator)
    {
        var primes = calculator.GetPrimesBetween(start, end);
        string primesOutput = primes.Any() ? string.Join(", ", primes) : "None";
        Console.WriteLine($"Input: {start}, {end} Output: {primes.Count} ({primesOutput})");
    }
}
