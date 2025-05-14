using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest_uut
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void Test_Valid_Login()
        {
            var account = new Account();
            account.Login = "user_123";
            Assert.AreEqual("user_123", account.Login);
        }

        [TestMethod]
        public void Test_Login_Empty_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.Login = "");
        }

        [TestMethod]
        public void Test_Login_Too_Short_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.Login = "ab");
        }

        [TestMethod]
        public void Test_Login_Too_Long_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.Login = new string('a', 21));
        }

        [TestMethod]    
        public void Test_Login_Invalid_Characters_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.Login = ";;№user123");
        }

        [TestMethod]
        public void Test_Valid_Password()
        {
            var account = new Account();
            account.Password = "securePassword123";
            Assert.AreEqual("securePassword123", account.Password);
        }

        [TestMethod]
        public void Test_Password_Empty_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.Password = "");
        }

        [TestMethod]
        public void Test_Password_Too_Short_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.Password = "1234567");
        }

        [TestMethod]
        public void Test_Password_Too_Long_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.Password = new string('a', 330));
        }

        [TestMethod]
        public void Test_Password_Equals_Login_Throws_Exception()
        {
            var account = new Account();
            account.Login = "user_123";
            Assert.ThrowsException<ArgumentException>(() => account.Password = "user_123");
        }

        [TestMethod]
        public void Test_Valid_TransactionTypeAnk()
        {
            var account = new Account();
            account.TransactionTypeAnk = "1";
            Assert.AreEqual("1", account.TransactionTypeAnk);
        }

        [TestMethod]
        public void Test_TransactionTypeAnk_Invalid_Value_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.TransactionTypeAnk = "3");
        }

        [TestMethod]
        public void Test_Valid_TypeAnk()
        {
            var account = new Account();
            account.TypeAnk = "Дом";
            Assert.AreEqual("Дом", account.TypeAnk);
        }

        [TestMethod]
        public void Test_Valid_Rooms()
        {
            var account = new Account();
            account.Rooms = "3";
            Assert.AreEqual("3", account.Rooms);
        }

        [TestMethod]
        public void Test_Rooms_Invalid_Value_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.Rooms = "-1");
        }

        [TestMethod]
        public void Test_Valid_RentPriceOt()
        {
            var account = new Account();
            account.RentPriceOt = "500";
            Assert.AreEqual("500", account.RentPriceOt);
        }

        [TestMethod]
        public void Test_RentPriceOt_Invalid_Value_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.RentPriceOt = "-500");
        }

        [TestMethod]
        public void Test_Valid_RentPriceDo()
        {
            var account = new Account();
            account.RentPriceDo = "1000";
            Assert.AreEqual("1000", account.RentPriceDo);
        }

        [TestMethod]
        public void Test_RentPriceDo_Invalid_Value_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.RentPriceDo = "-1000");
        }

        [TestMethod]
        public void Test_Valid_BuyPriceOt()
        {
            var account = new Account();
            account.BuyPriceOt = "500000";
            Assert.AreEqual("500000", account.BuyPriceOt);
        }

        [TestMethod]
        public void Test_BuyPriceOt_Invalid_Value_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.BuyPriceOt = "-500000");
        }

        [TestMethod]
        public void Test_Valid_BuyPriceDo()
        {
            var account = new Account();
            account.BuyPriceDo = "1000000";
            Assert.AreEqual("1000000", account.BuyPriceDo);
        }

        [TestMethod]
        public void Test_BuyPriceDo_Invalid_Value_Throws_Exception()
        {
            var account = new Account();
            Assert.ThrowsException<ArgumentException>(() => account.BuyPriceDo = "-1000000");
        }
    }
}