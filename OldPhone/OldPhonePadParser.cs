using System.Text.RegularExpressions;

namespace OldPhone;

public static class OldPhonePadParser
{
	private static readonly string validInputPattern = @"^[0-9 *#]*$";
	
	public static String OldPhonePad(string input)
	{
		throw new NotImplementedException();
	}
	
	public static bool IsValidInput(string input)
	{
		return Regex.IsMatch(input, validInputPattern);
	}
}