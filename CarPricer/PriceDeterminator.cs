using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPricer
{
    public class PriceDeterminator
    {
        public decimal DetermineCarPrice(Car car)
        {
            var reducedValueOfCarByMonths = getReducedValueOfCarByNumberOfMonths(car);

            var newValueOftheCar = car.PurchaseValue - reducedValueOfCarByMonths;
            var reducedValueOfCarByMiles = getReducedValueOfCarByMilesDriven(car, newValueOftheCar);

            newValueOftheCar = newValueOftheCar - reducedValueOfCarByMiles;

            var newValueOfCarByCollissions = getReducedValueOfCarByNumbersOfcollisions(car, newValueOftheCar);


            newValueOftheCar = newValueOftheCar - newValueOfCarByCollissions;

            var reducedValueOfCarByPreviousOwner = getReducedValueOfCarByPreviousOwner(car, newValueOftheCar);

            newValueOftheCar = newValueOftheCar - reducedValueOfCarByPreviousOwner;
            if (car.NumberOfPreviousOwners == 0)
            {
                newValueOftheCar = newValueOftheCar + (newValueOftheCar * 0.10m);
            }

            return  Math.Round(newValueOftheCar, 2);
        }


        private decimal getReducedValueOfCarByMilesDriven(Car car, decimal currentCarValue)
        {
            var totalMilesOfCar = car.NumberOfMiles;

            var numberOfMilesPerThousands = totalMilesOfCar / 1000;

            decimal reducedValueOfTheCar = 0;

            for (var indexCarMile = 0; indexCarMile < numberOfMilesPerThousands; indexCarMile++)
            {
                const int maximumMilesDriven = 150;
                if (indexCarMile >= maximumMilesDriven) { return reducedValueOfTheCar; }
                reducedValueOfTheCar = reducedValueOfTheCar + (currentCarValue * 0.002m);
            }
            return reducedValueOfTheCar;


        }


        private decimal getReducedValueOfCarByPreviousOwner(Car car, decimal currentCarValue)
        {
            var numbersOfPreviousOwners = car.NumberOfPreviousOwners;
            decimal reducedValueOfTheCar = 0;
            const int maximumPreviousOwner = 2;
            if (numbersOfPreviousOwners > maximumPreviousOwner)
            {
                for (var indexPreviousOwner = 0; indexPreviousOwner < numbersOfPreviousOwners; indexPreviousOwner++)
                {
                    reducedValueOfTheCar = reducedValueOfTheCar + (currentCarValue * 0.25m);
                }
            }

            return reducedValueOfTheCar;
        }


        private decimal getReducedValueOfCarByNumbersOfcollisions(Car car, decimal currentCarValue)
        {

            decimal reducedValueOfTheCar = 0;
            int collisionMult = ((car.NumberOfCollisions < 5) ? car.NumberOfCollisions : 5);
            reducedValueOfTheCar += currentCarValue * (collisionMult * 0.02m);
            return reducedValueOfTheCar;
        }


        private decimal getReducedValueOfCarByNumberOfMonths(Car car)
        {
            decimal reducedValueOfTheCar = 0;

            var newPurhcaseValue = car.PurchaseValue - car.PurchaseValue * 0.005m;
            for (var indexCarMonth = 0; indexCarMonth < car.AgeInMonths; indexCarMonth++)
            {
                const int maximumNumberOfMonths = 120;
                if (indexCarMonth > maximumNumberOfMonths) { return reducedValueOfTheCar; }

                reducedValueOfTheCar = reducedValueOfTheCar + (car.PurchaseValue * 0.005m);
            }
            return reducedValueOfTheCar;
        }
    }
}
