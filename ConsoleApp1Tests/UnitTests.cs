using ConsoleApp1;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ConsoleApp1Tests {

    [TestFixture]
    public class Tests
    {
        private string testfilesDirectory;

        [SetUp]
        public void Setup()
        {
            testfilesDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}/../../../testfiler/";
        }

        [Test]
        public void Test_CountFilenameOccurencesInFile_CountOk()
        {
            TestFile("Test.txt", 5);
            TestFile("Noll.txt", 0);
            TestFile("bild.jpg", 0);
            TestFile("Tom.txt", 0);
            TestFile("Åhléns.txt", 1);
        }

        [Test]
        public void Test_CountFilenameOccurencesInFile_TooBigFile()
        {
            var ex = Assert.Throws<ValidationException>(() => TestFile("stor.pdf"));
            Assert.That(ex.Message, Contains.Substring("File is too big"));
        }

        [Test]
        public void Test_CountFilenameOccurencesInFile_WrongFilePath()
        {
            var ex = Assert.Throws<ValidationException>(() => TestFile("nofilelikethis.txt"));
            Assert.That(ex.Message, Contains.Substring("File not found"));
        }

        private void TestFile(string fileName, int? expectedCount = null)
        {
            string filePath = Path.Combine(testfilesDirectory, fileName);
            var actualCount = FileUtil.CountFilenameOccurencesInFile(filePath);
            Assert.That(actualCount, Is.EqualTo(expectedCount), $"{fileName} has an unexpected number of occurensies");
        }
    }

}