using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class EncryptionProgram
{
    private readonly Dictionary<char, char> _encryptionMap;

    public EncryptionProgram(string mapFilePath)
    {
        if (string.IsNullOrWhiteSpace(mapFilePath))
            throw new ArgumentException("Map file path cannot be null or empty.");

        if (!File.Exists(mapFilePath))
            throw new FileNotFoundException("Encryption map file not found.", mapFilePath);

        _encryptionMap = LoadEncryptionMap(mapFilePath);
    }

    /// <summary>
    /// Encrypts a given input string based on the encryption map.
    /// </summary>
    /// <param name="input">The input string to encrypt.</param>
    /// <returns>The encrypted string.</returns>
    public string Encrypt(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be null or empty.");

        return new string(input.Select(c => _encryptionMap.ContainsKey(c) ? _encryptionMap[c] : c).ToArray());
    }

    /// <summary>
    /// Loads the encryption map from a file.
    /// </summary>
    /// <param name="filePath">The file path containing the encryption map.</param>
    /// <returns>A dictionary representing the encryption map.</returns>
    private Dictionary<char, char> LoadEncryptionMap(string filePath)
    {
        var map = new Dictionary<char, char>();

        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split('|');

            if (parts.Length != 2 || parts[0].Length != 1 || parts[1].Length != 1)
                throw new FormatException($"Invalid format in encryption map file: {line}");

            char original = parts[0][0];
            char replacement = parts[1][0];

            if (map.ContainsKey(original))
                throw new InvalidOperationException($"Duplicate mapping for character: {original}");

            map[original] = replacement;
        }

        return map;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Encryption Program!");
        Console.Write("Enter the path to the encryption map file: ");
        string mapFilePath = Console.ReadLine();

        try
        {
            var encryptionProgram = new EncryptionProgram(mapFilePath);

            while (true)
            {
                Console.Write("Enter a word to encrypt (or type 'exit' to quit): ");
                string input = Console.ReadLine();

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exited!");
                    break;
                }

                try
                {
                    string encryptedWord = encryptionProgram.Encrypt(input);
                    Console.WriteLine($"Encrypted word: {encryptedWord}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during encryption: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Initialization error: {ex.Message}");
        }
    }
}
