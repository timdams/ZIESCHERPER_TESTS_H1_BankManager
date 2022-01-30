using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOPClassBasicsTesterLibrary;

namespace BankManager.Tests
{
    [TestClass]
    public class UnitTestsDeel1
    {
        [TestMethod]
        public void BasisKlasseZaken()
        {
            TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new Rekening());
            tester.CheckAutoProperty("NaamKlant", typeof(string));
            tester.TestPropGetSet("NaamKlant", "tim", "tim");
            tester.CheckAutoProperty("RekeningNummer", typeof(string));
            tester.TestPropGetSet("RekeningNummer", "007", "007");
            tester.CheckFullProperty("Balans", typeof(int), propType: TimsEpicClassAnalyzer.PropertyTypes.NoSet);

        }

        [TestMethod]
        public void GeldStortenEnAfhalenTest()
        {
            TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new Rekening());
            tester.CheckMethod("StortGeld", typeof(void), new System.Type[] { typeof(int) });
            tester.CheckMethod("HaalGeldAf", typeof(int), new System.Type[] { typeof(int) });

            tester.TestMethod("StortGeld", new object[] { 100 });
            Assert.AreEqual(100, tester.GetProp("Balans"), "Ik probeerde 100 euro op een lege rekening te storten maar de balans klopt niet nadien");
            tester.TestMethod("HaalGeldAf", new object[] { 40 }, 40);
            string result = tester.TestMethod("HaalGeldAf", new object[] { 80 }, 60);
            Assert.IsTrue(result.Contains("leeg"), "Geen foutboodschap wanneer rekening leeg is gehaald.");
        }

        [TestMethod]
        public void ToonInfoMethodeTest()
        {
            TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new Rekening());
            if (tester.CheckMethod("ToonInfo", typeof(void)))
            {
                tester.SetProp("NaamKlant", "tim");
                tester.SetProp("RekeningNummer", "007");
                tester.TestMethod("StortGeld", new object[] { 100 });


                string result = tester.TestMethod("ToonInfo", null);
              
                Assert.IsTrue(result.Contains("tim"), "Klantennaam wordt niet door ToonInfo op het scherm getoond.");
                Assert.IsTrue(result.Contains("007"), "Rekeningnummer wordt niet door ToonInfo op het scherm getoond.");
                Assert.IsTrue(result.Contains("100"), "Balans wordt niet door ToonInfo op het scherm getoond.");
            }

        }

    }
}
