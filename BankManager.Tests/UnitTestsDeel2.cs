using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOPClassBasicsTesterLibrary;

namespace BankManager.Tests
{
    [TestClass]
    public class UnitTestsDeel2
    {
        [TestMethod]
        public void StaatPropertyTest()
        {
            TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new Rekening());
            tester.CheckAutoProperty("Staat", typeof(RekeningStaat), TimsEpicClassAnalyzer.PropertyTypes.PrivateSetPublicGet);
            Assert.AreEqual(RekeningStaat.Geldig, tester.GetProp("Staat"), "Rekening moet Geldig zijn bij de start");
        }

        [TestMethod]
        public void StaatGeldStortenEnAfhalenTest()
        {
            TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new Rekening());
            tester.CheckMethod("StortGeld", typeof(void), new System.Type[] { typeof(int) });
            tester.CheckMethod("HaalGeldAf", typeof(int), new System.Type[] { typeof(int) });

            tester.TestMethod("StortGeld", new object[] { 100 });
            string result= tester.TestMethod("HaalGeldAf", new object[] { 200 }, 100);
            Assert.AreEqual(RekeningStaat.Geblokkeerd, tester.GetProp("Staat"), "Rekening werd niet geblokkeerd nadat een te groot bedrag werd afgeaald");
            string result2 = tester.TestMethod("HaalGeldAf", new object[] { 200 }, 0);
            Assert.IsTrue(result2.ToLower().Contains("geblokkeerd"), "Geld afhalen van geblokkeerde rekening moet foutboodschap geven");

            string result3 = tester.TestMethod("StortGeld", new object[] { 200 });
            Assert.IsTrue(result3.ToLower().Contains("geblokkeerd"), "Geld storten op een geblokkeerde rekening moet foutboodschap geven");

        }

        [TestMethod]
        public void StaatToonInfoMethodeTest()
        {
            TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new Rekening());
            if (tester.CheckMethod("ToonInfo", typeof(void)))
            {
                tester.SetProp("NaamKlant", "tim");
                tester.SetProp("RekeningNummer", "007");
                tester.TestMethod("StortGeld", new object[] { 100 });
                string result1 = tester.TestMethod("ToonInfo", null);
                Assert.IsTrue(result1.ToLower().Contains("geldig"), "Staat wordt niet door ToonInfo op het scherm getoond.");
                tester.TestMethod("HaalGeldAf", new object[] { 200 },100);
                string result = tester.TestMethod("ToonInfo", null);     
                Assert.IsTrue(result.ToLower().Contains("geblokkeerd"), "Staat wordt niet door ToonInfo op het scherm getoond.");
               
            }

        }

        [TestMethod]
        public void VeranderStaatMethodeTest()
        {

            TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new Rekening());
            if (tester.CheckMethod("VeranderStaat", typeof(void), null))
            {
                for (int i = 0; i < 2; i++)
                {
                    RekeningStaat begin = (RekeningStaat)tester.GetProp("Staat");
                    tester.TestMethod("VeranderStaat", null);
                    if (begin == RekeningStaat.Geldig)
                        Assert.AreEqual(RekeningStaat.Geblokkeerd, (RekeningStaat)tester.GetProp("Staat"), "Staat wordt niet aangepast na aanroep VeranderStaat");
                    else
                        Assert.AreEqual(RekeningStaat.Geldig, (RekeningStaat)tester.GetProp("Staat"), "Staat wordt niet aangepast na aanroep VeranderStaat");
                }
            }  
        }

    }
}
