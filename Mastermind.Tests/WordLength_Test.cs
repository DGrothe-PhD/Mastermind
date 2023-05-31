namespace Mastermind.Tests
{
    [TestFixture]
    public class CorrectlySettingWordlists
    {
        private JsonWordLists jsonWordLists;

        [SetUp]
        public void Setup()
        {
            jsonWordLists = new();
        }

        [TestCase("en")]
        [TestCase("de")]
        [TestCase("fr")]
        public void CanReadJsonFiles(string languageCode)
        {
            jsonWordLists.SetWordList(languageCode);

            Assert.IsNotNull(jsonWordLists.CurrentWordLists?.WordsWith3);
            Assert.IsNotNull(jsonWordLists.CurrentWordLists?.WordsWith4);
            Assert.IsNotNull(jsonWordLists.CurrentWordLists?.WordsWith5);
        }

        [TestCase("en")]
        [TestCase("de")]
        [TestCase("fr")]
        public void JsonFilesContainWordsWithCorrectLength(string languageCode)
        {
            jsonWordLists.SetWordList(languageCode);

            Assert.That(
                jsonWordLists.CurrentWordLists?.WordsWith3?.Any(word => word.Length != 3),
                Is.False,
                "Found a word that is not of length 3");

            Assert.That(
                jsonWordLists.CurrentWordLists?.WordsWith4?.Any(word => word.Length != 4),
                Is.False,
                "Found a word that is not of length 4");

            Assert.That(
                jsonWordLists.CurrentWordLists?.WordsWith5?.Any(word => word.Length != 5),
                Is.False,
                "Found a word that is not of length 5");
        }
    }
}