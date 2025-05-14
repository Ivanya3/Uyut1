using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest_uut
{
    [TestClass]
    public class UnitTest_property
    {
        [TestMethod]
        public void Test_Valid_Id()
        {
            var property = new Property();
            property.Id = "P123";
            Assert.AreEqual("P123", property.Id);
        }

        [TestMethod]
        public void Test_Type_Empty_Throws_Exception()
        {
            var property = new Property();
            Assert.ThrowsException<ArgumentException>(() => property.Type = "");
        }

        [TestMethod]
        public void Test_Type_Valid_Values()
        {
            var property1 = new Property();
            property1.Type = "Дом";
            var property2 = new Property();
            property2.Type = "Квартира";
            Assert.AreEqual("Дом", property1.Type);
            Assert.AreEqual("Квартира", property2.Type);
        }

        [TestMethod]
        public void Test_Type_Invalid_Value_Throws_Exception()
        {
            var property = new Property();
            Assert.ThrowsException<ArgumentException>(() => property.Type = "Офис");
        }

        [TestMethod]
        public void Test_City_Empty_Throws_Exception()
        {
            var property = new Property();
            Assert.ThrowsException<ArgumentException>(() => property.City = "");
        }

        [TestMethod]
        public void Test_City_Invalid_Characters_Throws_Exception()
        {
            var property = new Property();
            Assert.ThrowsException<ArgumentException>(() => property.City = "Москва123");
        }

        [TestMethod]
        public void Test_City_Valid_Value()
        {
            
            var property = new Property { City = "Москва" };

            Assert.AreEqual("Москва", property.City);
        }

        [TestMethod]
        public void Test_RentPrice_Negative_Throws_Exception()
        {
            var property = new Property();
            Assert.ThrowsException<ArgumentException>(() => property.RentPrice = -500);
        }

        [TestMethod]
        public void Test_RentPrice_Valid_Value()
        {
            var property = new Property();
            property.RentPrice = 1000;
            Assert.AreEqual(1000, property.RentPrice);
        }

        [TestMethod]
        public void Test_BuyPrice_Negative_Throws_Exception()
        {
            var property = new Property();
            Assert.ThrowsException<ArgumentException>(() => property.BuyPrice = -500);
        }

        [TestMethod]
        public void Test_BuyPrice_Valid_Value()
        {
            var property = new Property { BuyPrice = 1000000 };
            Assert.AreEqual(1000000, property.BuyPrice);
        }

        [TestMethod]
        public void Test_Description_Too_Long_Throws_Exception()
        {
            var longDescription = new string('a', 1001);
            var property = new Property();
            Assert.ThrowsException<ArgumentException>(() => property.Description = longDescription);
        }

        [TestMethod]
        public void Test_Description_Valid_Value()
        {
            var property = new Property { Description = "Уютная квартира" };

            Assert.AreEqual("Уютная квартира", property.Description);
        }

    }
}
