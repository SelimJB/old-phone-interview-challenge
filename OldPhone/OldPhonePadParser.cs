using System.Text.RegularExpressions;

namespace OldPhone;

public static class OldPhonePadParser
{
	private static readonly string validInputPattern = @"^[0-9 *#]*$";
	private static readonly string specialCharacters = "0*#";

	public static String OldPhonePad(string input)
	{
		throw new NotImplementedException();
	}

	public static bool IsValidInput(string input)
	{
		return Regex.IsMatch(input, validInputPattern);
	}

	public static string Simplify(string input)
	{
		return SimplifyWhiteSpace(RemoveHashSuffix(input));
	}

	private static string RemoveHashSuffix(string input)
	{
		var hashIndex = input.IndexOf("#");
		if (hashIndex >= 0)
			input = input.Substring(0, hashIndex + 1);

		return input;
	}

	private static string CollapseSpaces(string input)
	{
		return Regex.Replace(input, @"\s+", " ").Trim();
	}

	private static string RemoveSpacesAfterSpecialCharacters(string input)
	{
		return Regex.Replace(input, $@"([{specialCharacters}])\s+", "$1");
	}

	private static string SimplifyWhiteSpace(string input)
	{
		input = CollapseSpaces(input);

		if (string.IsNullOrEmpty(input))
			return input;

		var result = input[0].ToString();

		for (var i = 1; i < input.Length; i++)
			if (input[i] != ' ' || input[i - 1] == input[i + 1])
				result += input[i];

		result = RemoveSpacesAfterSpecialCharacters(result);

		return result;
	}
}