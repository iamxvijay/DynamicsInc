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

// Main Program
public class Program
{
    public static void Main(string[] args)
    {
        var calculator = new PrimeNumberCalculator();
        bool keepRunning = true;

        while (keepRunning)
        {
            try
            {
                Console.WriteLine("Enter the starting integer:");
                int start = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the ending integer:");
                int end = int.Parse(Console.ReadLine());

                PrintPrimes(start, end, calculator);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter valid integers.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("Do you want to run again? Type 'yes' to run again or 'exit' to quit:");
            string choice = Console.ReadLine();
            if (choice.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                keepRunning = false;
                Console.WriteLine("Exited");
            }
        }
    }

    private static void PrintPrimes(int start, int end, PrimeNumberCalculator calculator)
    {
        var primes = calculator.GetPrimesBetween(start, end);
        string primesOutput = primes.Any() ? string.Join(", ", primes) : "None";
        Console.WriteLine($"Input: {start}, {end} Output: {primes.Count} ({primesOutput})");
    }
}
