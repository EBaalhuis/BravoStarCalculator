using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StarOfTheShowCalcTest
{
    [TestClass]
    public class CalculatorTest
    {
        readonly double Delta = 0.0001;

        [TestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(0, 10, 10, 0)]
        [DataRow(10, 0, 10, 0)]
        [DataRow(10, 10, 0, 0)]
        [DataRow(5, 5, 5, 0.0131)]  // Outcome source: TCG Tools app
        [DataRow(10, 10, 10, 0.0892)]  // Outcome source: TCG Tools app
        [DataRow(15, 15, 15, 0.2492)]  // Outcome source: TCG Tools app
        [DataRow(20, 20, 20, 0.4676)]  // Outcome source: TCG Tools app
        [DataRow(5, 10, 20, 0.0841)]  // Outcome source: TCG Tools app
        [DataRow(1, 1, 5, 0.0006)]  // Outcome source: TCG Tools app
        [DataRow(1, 5, 1, 0.0006)]  // Outcome source: TCG Tools app
        [DataRow(5, 1, 1, 0.0006)]  // Outcome source: TCG Tools app
        public void WithoutPulse_Draw4(int earth, int ice, int lightning, double expected)
        {
            int pulseEarthIce = 0;
            int pulseEarthLightning = 0;
            int pulseIceLightning = 0;
            int draw = 4;

            var actual = Calculator.Calculate(earth, ice, lightning, pulseEarthIce, pulseEarthLightning, pulseIceLightning, draw: draw);
            Assert.AreEqual(expected, actual, Delta);
        }

        [TestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(0, 10, 10, 0)]
        [DataRow(10, 0, 10, 0)]
        [DataRow(10, 10, 0, 0)]
        [DataRow(5, 5, 5, 0.0293)]  // Outcome source: TCG Tools app
        [DataRow(10, 10, 10, 0.1715)]  // Outcome source: TCG Tools app
        [DataRow(15, 15, 15, 0.4066)]  // Outcome source: TCG Tools app
        [DataRow(20, 20, 20, 0.6471)]  // Outcome source: TCG Tools app
        [DataRow(5, 10, 20, 0.1541)]  // Outcome source: TCG Tools app
        [DataRow(1, 1, 5, 0.0014)]  // Outcome source: TCG Tools app
        [DataRow(1, 5, 1, 0.0014)]  // Outcome source: TCG Tools app
        [DataRow(5, 1, 1, 0.0014)]  // Outcome source: TCG Tools app
        public void WithoutPulse_Draw5(int earth, int ice, int lightning, double expected)
        {
            int pulseEarthIce = 0;
            int pulseEarthLightning = 0;
            int pulseIceLightning = 0;
            int draw = 5;

            var actual = Calculator.Calculate(earth, ice, lightning, pulseEarthIce, pulseEarthLightning, pulseIceLightning, draw: draw);
            Assert.AreEqual(expected, actual, Delta);
        }

        [TestMethod]
        public void OnePulse_OneOfThirdElement()
        {
            var expected = (double)1 / 60 * 1 / 59 * 4 * 3;

            var actual = Calculator.Calculate(earth: 1, ice: 0, lightning: 0, 
                pulseEarthIce: 0, pulseEarthLightning: 0, pulseIceLightning: 1, draw: 4);
            Assert.AreEqual(expected, actual, Delta);

            actual = Calculator.Calculate(earth: 0, ice: 1, lightning: 0,
                pulseEarthIce: 0, pulseEarthLightning: 1, pulseIceLightning: 0, draw: 4);
            Assert.AreEqual(expected, actual, Delta);

            actual = Calculator.Calculate(earth: 0, ice: 0, lightning: 1,
                pulseEarthIce: 1, pulseEarthLightning: 0, pulseIceLightning: 0, draw: 4);
            Assert.AreEqual(expected, actual, Delta);
        }

        [TestMethod]
        public void OnePulse_TwoOfThirdElement()
        {
            var expected = (double)1 / 60 * 2 / 59 * 57/58 * 56/57 * 4 * 3  // One pulse, one third element, two other
                         + (double)1 / 60 * 2 / 59 * 1 / 58 * 56 / 57 * 4 * 3; // One pulse, two third element, one other

            var actual = Calculator.Calculate(earth: 2, ice: 0, lightning: 0,
                pulseEarthIce: 0, pulseEarthLightning: 0, pulseIceLightning: 1, draw: 4);
            Assert.AreEqual(expected, actual, Delta);

            actual = Calculator.Calculate(earth: 0, ice: 2, lightning: 0,
                pulseEarthIce: 0, pulseEarthLightning: 1, pulseIceLightning: 0, draw: 4);
            Assert.AreEqual(expected, actual, Delta);

            actual = Calculator.Calculate(earth: 0, ice: 0, lightning: 2,
                pulseEarthIce: 1, pulseEarthLightning: 0, pulseIceLightning: 0, draw: 4);
            Assert.AreEqual(expected, actual, Delta);
        }
    }
}
