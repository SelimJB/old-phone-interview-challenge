using NUnit.Framework;
using OldPhone;

namespace OldPhoneTests;

[TestFixture]
public class PhonePadTests
{
	[TestCase("33#", true)]
	[TestCase("227*#", true)]
	[TestCase("4433555 555666#", true)]
	[TestCase("8 88777444666*664#", true)]
	[TestCase("", true)]
	[TestCase("****###    ##***", true)]
	[TestCase("   ", true)]
	[TestCase("Nop", false)]
	[TestCase("33#hey", false)]
	[TestCase("44335a55 555666#", false)]
	public void IsValidInput_ShouldReturnExpectedResult(string input, bool expectedResult)
	{
		Assert.That(OldPhonePadParser.IsValidInput(input), Is.EqualTo(expectedResult));
	}

	[TestCase("Nop")]
	[TestCase("33#hey")]
	[TestCase("44335a55 555666#")]
	public void OldPhonePad_InvalidInput_ThrowsException(string input)
	{
		Assert.Throws<ArgumentException>(() => OldPhonePadParser.OldPhonePad(input));
	}

	[TestCase("33#", "E")]
	[TestCase("227*#", "B")]
	[TestCase("4433555 555666#", "HELLO")]
	[TestCase("7777777", "R")]
	[TestCase("45*********77777 77 7777777", "PQR")]
	public void OldPhonePad_ValidInput_ReturnsExpectedOutput(string input, string expectedOutput)
	{
		Assert.That(OldPhonePadParser.OldPhonePad(input), Is.EqualTo(expectedOutput));
	}
}