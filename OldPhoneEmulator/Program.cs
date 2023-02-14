using OldPhone;

static class Program
{
	private const float delayBeforeAutomaticCharacterValidation = 0.5f;

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

	// Imperative programming is most likely not be the best approach to truly emulate the functioning of a telephone keypad,
	// but since that is not the focus of this exercise and I just wanted to create a fun demo, this quick draft is totally ok
	private static void EmulateOldPhonePadInput()
	{
		var input = string.Empty;
		Console.WriteLine("Enter message :");
		var previousKeyPressTime = DateTime.Now;

		while (true)
		{
			var keyInfo = Console.ReadKey(true);
			var currentKeyPressTime = DateTime.Now;
			var deltaTime = currentKeyPressTime - previousKeyPressTime;

			if (input != string.Empty &&
			    deltaTime.TotalSeconds > delayBeforeAutomaticCharacterValidation &&
			    input.Last() == keyInfo.KeyChar
			   )
			{
				input += " ";
			}

			input += keyInfo.KeyChar;
			previousKeyPressTime = currentKeyPressTime;

			Console.Clear();
			Console.WriteLine("Enter message :");

			try
			{
				Console.WriteLine(OldPhonePadParser.OldPhonePad(input));

				if (keyInfo.KeyChar == '#')
				{
					Console.WriteLine($"{input} --> {OldPhonePadParser.OldPhonePad(input)}");
					break;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
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