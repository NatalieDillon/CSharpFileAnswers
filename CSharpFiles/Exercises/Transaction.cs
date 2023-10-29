using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CSharpFiles.Examples.Diary;

namespace CSharpFiles.Exercises
{
	public class Transaction
	{
		public enum TransactionCategory
		{
			Groceries,
			Utilities,
			Interest,
			EatingOut,
			Salary,
			Rent,
			Gift,
			Other
		}

		public enum TransactionType
		{
			Cash,
			DirectDebit,
			StandingOrder,
			BankTransfer,
			DebitCard,
			Cheque
		}

		public record TransactionRecord(DateTime Date, decimal Amount, TransactionCategory Category, TransactionType Type, string CounterParty, string Description);
		
		public static void WriteToFile(string fileName, List<TransactionRecord> transactions)
		{
			using (var streamWriter = File.AppendText(fileName))
			{
				foreach (var item in transactions)
				{
					streamWriter.WriteLine($"{item.Date:dd/MM/yyyy},{item.Amount},{item.Category},{item.Type},{item.CounterParty},{item.Description}");
				}
			}
		}

		public static List<TransactionRecord> ReadTransactions(string fileName)
		{
			List<TransactionRecord> transactions = new();
			string[] lines = File.ReadAllLines(fileName);
			foreach (string  line in lines)
			{
				string[] fields = line.Split(",");
				if (fields.Length == 6)
				{
					if (DateTime.TryParseExact(fields[0], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,out DateTime date)
						&& decimal.TryParse(fields[1], out decimal amount)
						&& Enum.TryParse(fields[2], out TransactionCategory category)
						&& Enum.TryParse(fields[3], out TransactionType type))
					{
						TransactionRecord record = new(date, amount, category, type, fields[4], fields[5]);
						transactions.Add(record);
					}
				}				
			}
			return transactions;
		}

	}
}
