using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Collections.Immutable;
using System.Globalization;
using static CSharpFiles.Exercises.Transaction;
using CSharpFiles.Examples;
using static CSharpFiles.Examples.Student;

namespace CSharpFiles.Exercises
{
	// Add your code to this file
	public class FileExercises
	{
		public static void Run()
		{
			Console.WriteLine();
			Numbers();
			Console.WriteLine();
			Transactions();
			Console.WriteLine();
			Stations();
			Console.WriteLine();
			Console.WriteLine(Sentences("Sentence.txt"));
			Console.WriteLine();
		}

		public static void Stations()
		{
			string fileName = @"Files\stations.txt";

			if (File.Exists(fileName))
			{
				string[] lines = File.ReadAllLines(fileName);
				Dictionary<string, int> stationsOnLine = []; // Holds stations per line

				List<string> twoWordSameLetter = [];

				// Holds stations with no matching letters
				List<(string word, List<string> stations)> wordsNoLetters =			
				[
					("Mackerel", []),
					("Piranha", []),
					("Sturgeon", []),
					("Bacteria",[])
				];

				foreach (string line in lines)
				{
					string[] values = line.Split(",");
					for (int i = 1; i < values.Length; i++) // Find station per line count
					{
						if (!stationsOnLine.TryAdd(values[i], 1))
						{
							stationsOnLine[values[i]]++;
						}
					}

					string stationName = values[0];
					foreach (var value in wordsNoLetters) // Find station names that don't share letters
					{
						if (ContainsNoLetters(value.word, stationName))
						{
							value.stations.Add(stationName);
						}
					}
					string[] splitStationName = stationName.Split();
					if (splitStationName.Length == 2 && char.ToLower(splitStationName[0][0]) == char.ToLower(splitStationName[1][0]))
					{
						twoWordSameLetter.Add(stationName);
					}
				}

				// Output results
				Console.WriteLine("Station problem");
				var sortedStations = stationsOnLine.OrderBy(kvp => kvp.Key).ToList();
				foreach (var kvp in sortedStations)
				{
					Console.WriteLine($"{kvp.Key}: {kvp.Value}");
				}
				Console.WriteLine();
				foreach (var value in wordsNoLetters)
				{
					Console.WriteLine($"{value.word}: {string.Join(", ", value.stations)}");
				}
				Console.WriteLine();
				Console.WriteLine($"Two word same letter station names: {string.Join(", ", twoWordSameLetter)}");
			}
			else
			{
				Console.WriteLine("Unable to read " + fileName);
			}
		}

		private static bool ContainsNoLetters(string wordA, string wordB)
		{
			foreach (char letter in wordA)
			{
				if (wordB.Contains(letter, StringComparison.CurrentCultureIgnoreCase))
				{
					return false;
				}
			}
			return true;
		}


		public static string Sentences(string fileName)
		{
			string fileNameWithPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{fileName}";
			string message = File.Exists(fileNameWithPath) ? "Appended to existing file" : "Created new file";
			string input;
			using (StreamWriter writer = new(fileNameWithPath, true))
			{
				do
				{
					Console.Write("Please enter your sentence: ");
					input = Console.ReadLine() ?? string.Empty;
					if (input != string.Empty)
					{
						writer.WriteLine(input);
					}

				} while (input != string.Empty);
			}
			return message;
		}


		public static void Transactions()
		{
			// Write Transactions
			string fileName = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\transactions.csv";
			List<TransactionRecord> records =
			[
				new TransactionRecord(new DateTime(2023, 10, 25), -52, TransactionCategory.Gift, TransactionType.DebitCard, "Party Palace", "Halloween Gifts"),
				new TransactionRecord(new DateTime(2023, 10, 20), -112, TransactionCategory.Groceries, TransactionType.DebitCard, "Waitrose", "Food shopping"),
				new TransactionRecord(new DateTime(2023, 10, 15), 4234, TransactionCategory.Salary, TransactionType.StandingOrder, "St Pauls", "Monthly salary")
			];

			WriteToFile(fileName, records);

			// Read transactions
			var transactions = ReadTransactions(fileName);
			Console.WriteLine($"There are {transactions.Count} transactions");
			for (int i = 0; i < transactions.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {transactions[i]}");
			}
		}


		public static void Numbers()
		{
			string fileName = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\numbers.bin";

			Random random = new();
			List<double> numbers = [];
			for (int i=0; i < 100; i++) // Generate 100 random numbers
			{
				double num = random.NextDouble()*1000;
				numbers.Add(num);
			}

			using (BinaryWriter binWriter = new(File.Open(fileName, FileMode.Create)))
			{
				foreach (double number in numbers)
				{
					binWriter.Write(number); 
				}
			}

			using (BinaryReader reader = new(File.OpenRead(fileName)))
			{
				byte[] bytes = reader.ReadBytes((int)reader.BaseStream.Length);
				Console.WriteLine(string.Join(',', bytes));
			}

			List<double> numbersRead = [];
			using (BinaryReader reader = new(File.OpenRead(fileName)))
			{
				while (reader.BaseStream.Position < reader.BaseStream.Length)
				{
					double number = reader.ReadDouble(); // Reads eight bytes
					numbersRead.Add(number);
				}
			}
			Console.WriteLine($"{numbersRead.Count} read from file");
			Console.WriteLine(string.Join(",", numbersRead));

		}
	}
}
