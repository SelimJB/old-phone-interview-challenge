using System.Text.RegularExpressions;

namespace OldPhone;

public static class OldPhonePadParser
{
	private static readonly string validInputPattern = @"^[0-9 *#]*$";
	private static readonly string specialCharacters = "0*#";

	/// <summary>
	/// Evaluates a given input string to determine its equivalent representation on an old-style phone pad. 
	/// The function only allows numbers (0-9), spaces, *, and # as valid inputs. 
	/// </summary>
	/// <param name="input">The input string to be evaluated</param>
	/// <returns>The equivalent representation of the input on an old-style phone pad</returns>
	/// <exception cref="System.ArgumentException">Thrown if the input string contains invalid characters</exception>
	public static String OldPhonePad(string input)
	{
		if (!IsValidInput(input))
			throw new ArgumentException("Invalid input. Only numbers (0-9), spaces, *, and # are allowed.");

		return new OldPhonePadInputEvaluator(Simplify(input)).Evaluate();
	}

	/// <summary>
	/// Checks if a given string is a valid input.
	/// Valid inputs are strings that consist solely of numbers (0-9), spaces, *, and #.
	/// </summary>
	public static bool IsValidInput(string input)
	{
		return Regex.IsMatch(input, validInputPattern);
	}

	/// <summary>
	/// Cleans up the input string by removing hash (#) suffixes and simplifying white spaces. 
	/// </summary>
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