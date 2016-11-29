using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarPricer;

namespace CarPriceUnitTest.Tests
{
    [TestClass()]
    public class CarPricerTest
    {

       
        [TestMethod()]
        public void DetermineCarPrice_SampleValues_Calculate()
        {
            AssertCarValue(25313.40m, 35000m, 3 * 12, 50000, 1, 1);
            AssertCarValue(19688.20m, 35000m, 3 * 12, 150000, 1, 1);
            AssertCarValue(19688.20m, 35000m, 3 * 12, 250000, 1, 1);
            AssertCarValue(20090.00m, 35000m, 3 * 12, 250000, 1, 0);
            AssertCarValue(21657.02m, 35000m, 3 * 12, 250000, 0, 1);
        }

        private static void AssertCarValue(decimal expectValue, decimal purchaseValue, int ageInMonths, int numberOfMiles, int numberOfPreviousOwners, int numberOfCollisions)
        {
            var car = new Car
            {
                AgeInMonths = ageInMonths,
                NumberOfCollisions = numberOfCollisions,
                NumberOfMiles = numberOfMiles,
                NumberOfPreviousOwners = numberOfPreviousOwners,
                PurchaseValue = purchaseValue
            };
            var priceDeterminator = new PriceDeterminator();
            decimal carPrice = priceDeterminator.DetermineCarPrice(car);
            Assert.AreEqual(expectValue, carPrice);
        }
    }
}