namespace OldPhone;

internal class OldPhonePadInputEvaluator
{
	public OldPhonePadInputEvaluator(string input)
	{
		this.input = input;
		result = string.Empty;
	}

	private static readonly Dictionary<char, List<char>> padAssociations = new()
	{
		{ '1', new List<char> { '&', '\'', '(' } },
		{ '2', new List<char> { 'A', 'B', 'C' } },
		{ '3', new List<char> { 'D', 'E', 'F' } },
		{ '4', new List<char> { 'G', 'H', 'I' } },
		{ '5', new List<char> { 'J', 'K', 'L' } },
		{ '6', new List<char> { 'M', 'N', 'O' } },
		{ '7', new List<char> { 'P', 'Q', 'R', 'S' } },
		{ '8', new List<char> { 'T', 'U', 'V' } },
		{ '9', new List<char> { 'W', 'X', 'Y', 'Z' } }
	};

	private readonly string input;
	private string result;
	private bool isInputCompleted;
	private char? currentCharacter;
	private char? currentDigit;
	private int currentDigitIncr = -1;

	public bool IsInputCompleted => isInputCompleted;

	public string Evaluate()
	{
		foreach (var character in input)
		{
			Evaluate(character);

			if (isInputCompleted)
				break;
		}

		NextCharacterCommand();
		return result;
	}

	// Note : I probably would have chosen a command pattern if there were more complicated & different actions 
	private void Evaluate(char input)
	{
		switch (input)
		{
			case ' ':
				NextCharacterCommand();
				break;
			case '0':
				SpaceCommand();
				break;
			case '*':
				DeleteCommand();
				break;
			case '#':
				EnterCommand();
				break;
			default:
			{
				if (char.IsDigit(input))
					PressDigitCommand(input);
				else
					throw new ArgumentException("Input character is invalid. Only 0, *, #, spaces and digits are allowed.");

				break;
			}
		}
	}

	private void PressDigitCommand(char digit)
	{
		if (currentDigit != null && currentDigit != digit)
			NextCharacterCommand();

		currentDigit = digit;
		currentDigitIncr++;
		var characters = padAssociations[digit];
		currentCharacter = characters[currentDigitIncr % padAssociations[digit].Count];
	}

	private void EnterCommand()
	{
		NextCharacterCommand();
		isInputCompleted = true;
	}

	private void SpaceCommand()
	{
		NextCharacterCommand();
		result += " ";
	}

	private void DeleteCommand()
	{
		if (currentCharacter != null)
			ClearCurrentCharacter();
		else if (!string.IsNullOrEmpty(result))
			result = result.Remove(result.Length - 1);
	}

	private void NextCharacterCommand()
	{
		result += currentCharacter;
		ClearCurrentCharacter();
	}

	private void ClearCurrentCharacter()
	{
		currentCharacter = null;
		currentDigit = null;
		currentDigitIncr = -1;
	}
}