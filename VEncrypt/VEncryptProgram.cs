using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VEncrypt
{
    class VEncryptProgram
    {
        static List<char> vowels = new List<char> {'A', 'E', 'I', 'O', 'U'};

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Error: VEncrypt requires two arguments - mode (E or D) and string");
                return;
            }

            string response = "";

            switch(args[0])
            {
                case "E":
                    Console.WriteLine("VEncrypting: " + args[1]);
                    response = Encrypt(args[1]);
                    break;
                case "D":
                    Console.WriteLine("VDecrypting: " + args[1]);
                    response = Decrypt(args[1]);
                    break;
                default:
                    Console.WriteLine("Error: Unrecognised mode (E = Encrypt, D = Decrypt)");
                    break;
            }

            Console.WriteLine(response);
        }

        static string Encrypt(string stringToEncrypt)
        {
             // Encrypt the string
            var sb = new StringBuilder();

            for(int i = 0; i < stringToEncrypt.Length; i++)
            {
                var triplet = CreateVowelTripletFromChar32Val(char.ConvertToUtf32(stringToEncrypt, i));
                sb.Append(triplet.Item1.ToString());
                sb.Append(triplet.Item2.ToString());
                sb.Append(triplet.Item3.ToString());
            }

             return sb.ToString();
        }

        static string Decrypt(string stringToDecrypt)
        {
            // Get vowel sequence.
            List<char> vowelSequence = GetVowelsFromString(stringToDecrypt);

            string decryptedMessage = "";

            // Check that the vowels 
            int maxVowelSequenceCount = vowelSequence.Count - (vowelSequence.Count % 3);
            for(var i = 0; i < maxVowelSequenceCount; i += 3)
            {
                // Get Char for the vowel triplet
                decryptedMessage += GetCharForVowelTriplet(vowelSequence[i], vowelSequence[i+1], vowelSequence[i+2]);
            }

            return decryptedMessage;
        }

        static List<char> GetVowelsFromString(string stringToExtractVowels)
        {
            List<char> vowelsToReturn = new List<char>();

            string toLowerInvarintString = stringToExtractVowels.ToUpperInvariant();
            foreach(char c in toLowerInvarintString)
            {
                if (vowels.Contains(c))
                {
                    vowelsToReturn.Add(c);
                }
            }

            return vowelsToReturn;
        }

        static string GetCharForVowelTriplet(char first, char second, char third)
        {
            int char32Val = 65 + vowels.IndexOf(first);
            char32Val += vowels.IndexOf(second) * 5;
            char32Val += vowels.IndexOf(third) * 25;
            
            return char.ConvertFromUtf32(char32Val);
        }

        static (char first, char second, char third) CreateVowelTripletFromChar32Val(int char32Value)
        {
            int baseValue = char32Value - 65;
            char thirdVowel = vowels[baseValue / 25];
            baseValue = baseValue - ((baseValue / 25) * 25);
            char secondVowel = vowels[baseValue / 5];
            baseValue = baseValue - ((baseValue / 5) * 5);
            char firstVowel = vowels[baseValue];
            
            return (firstVowel, secondVowel, thirdVowel);
        }
    }
}
