using OldPhone;

static class Program
{
	static void Main()
	{
		PrintInstructions();

		while (true)
		{
			var input = Console.ReadLine();
			Console.Clear();

			switch (input)
			{
				case "h":
					PrintInstructions();
					break;
				case "1":
					ParseOldPhonePadInput();
					break;
				case "2":
					EmulateOldPhonePadInput();
					break;
			}

			if (input == "q")
				break;

			Clear();
		}
	}

	private static void ParseOldPhonePadInput()
	{
		Console.WriteLine("Please enter an input to be parsed using the old phone parser :");
		try
		{
			var input = Console.ReadLine();
			Console.WriteLine($"{input} --> {OldPhonePadParser.OldPhonePad(input)}");
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}
	}

	private static void EmulateOldPhonePadInput()
	{
		var input = string.Empty;
		Console.WriteLine("Enter message :");

		while (true)
		{
			var keyInfo = Console.ReadKey(true);

			input += keyInfo.KeyChar;

			Console.Clear();
			Console.WriteLine("Enter message :");
			Console.WriteLine(OldPhonePadParser.OldPhonePad(input));

			if (keyInfo.KeyChar == '#')
			{
				Console.WriteLine($"{input} --> {OldPhonePadParser.OldPhonePad(input)}");
				break;
			}

			if (keyInfo.Key == ConsoleKey.Escape)
				break;
		}
	}

	private static void Clear()
	{
		Console.WriteLine();
		Console.WriteLine("---- Press any key to continue ----");
		Console.ReadKey(true);
		Console.Clear();
		PrintInstructions();
	}

	private static void PrintInstructions()
	{
		Console.WriteLine("Enter one of the following commands:");
		Console.WriteLine("\t'1' : Parse Old Phone Pad Input");
		Console.WriteLine("\t'2' : Emulate Old Phone Pad Input");
		Console.WriteLine("\t'h' : Display these instructions");
		Console.WriteLine("\t'q' : Quit");
		Console.WriteLine();
	}
}