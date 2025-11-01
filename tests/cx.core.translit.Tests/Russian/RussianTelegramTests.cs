namespace cx.core.translit.Tests.Russian;

/// <summary>
/// Tests for the Russian Telegram transliteration.
/// </summary>
public class RussianTelegramTests
{
    private readonly RussianTelegram _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("ж", "j")]
    [InlineData("х", "h")]
    [InlineData("ц", "c")]
    [InlineData("щ", "sc")]
    [InlineData("ю", "iu")]
    [InlineData("я", "ia")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Москва", "Moskva")]
    [InlineData("Россия", "Rossiia")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void HardSign_Deleted()
    {
        Assert.Equal("obekt", Translit.Convert("объект", _table));
    }
}
