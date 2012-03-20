using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}

		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestGetCarLocation()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            String carLocation = "Tokyo";
            using (mocks.Record())
            {
                // The mock will return Tokyo when the call is made with 1
                mockDatabase.getCarLocation(1);
                LastCall.Return(carLocation);
            }
            var target = new Car(10);
            target.Database = mockDatabase;

            String result;
            result = target.getCarLocation(1);
            Assert.AreEqual(result, carLocation);


        }

        [Test()]
        public void testCarMilage()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            int miles = 2000;
            mockDatabase.Miles = miles ;
            var target = new Car(1);
            target.Database = mockDatabase;
            int actual = target.Mileage;
            Assert.AreEqual(actual, 2000);
        }

        [Test()]
        public void TestThatCarHasCorrectBasePriceForTenDaysWithObjectMother()
        {
            var target = ObjectMother.BMW();
            Assert.AreEqual(10 * 10 * .8, target.getBasePrice());
        }

	}
}
