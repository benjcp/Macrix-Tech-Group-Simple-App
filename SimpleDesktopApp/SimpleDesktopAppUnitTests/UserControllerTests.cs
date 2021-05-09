using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleDesktopApp;

namespace SimpleDesktopAppUnitTests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void ValidationFirstName()
        {
            string colName = "FirstName";

            string strNull = null;
            string strEmpty = "";
            string name = "Ben";

            string isNull = UserController.ValidateField(colName, strNull);
            string isEmpty = UserController.ValidateField(colName, strEmpty);
            string validName = UserController.ValidateField(colName, name);

            Assert.AreEqual(UserController.EMPTY_ERROR_MESSAGE, isNull);
            Assert.AreEqual(UserController.EMPTY_ERROR_MESSAGE, isEmpty);
            Assert.IsNull(validName);
    }

        [TestMethod]
        public void ValidationApartmentNumber()
        {
            string colName = "ApartmentNumber";

            string strNull = null;
            string strEmpty = "";
            string text = "one";
            string invalidNumber = "0";
            string validNumber = "1";

            string isNull = UserController.ValidateField(colName, strNull);
            string isEmpty = UserController.ValidateField(colName, strEmpty);
            string isText = UserController.ValidateField(colName, text);
            string isInvalidNumber = UserController.ValidateField(colName, invalidNumber);
            string isValidNumber = UserController.ValidateField(colName, validNumber);

            Assert.IsNull(isNull);
            Assert.IsNull(isEmpty);
            Assert.AreEqual(UserController.APARTMENT_NUMBER_ERROR_MESSAGE, isText);
            Assert.AreEqual(UserController.APARTMENT_NUMBER_ERROR_MESSAGE, isInvalidNumber);
            Assert.IsNull(isValidNumber);
        }

        [TestMethod]
        public void ValidationPhoneNumber()
        {
            string colName = "PhoneNumber";

            string strNull = null;
            string strEmpty = "";
            string text = "phone";
            string invalidNumber = "1234567890123456";
            string plus = "+637748532";
            string dash = "123-456-789";
            string number = "05674983485";

            string isNull = UserController.ValidateField(colName, strNull);
            string isEmpty = UserController.ValidateField(colName, strEmpty);
            string isText = UserController.ValidateField(colName, text);
            string isInvalidNumber = UserController.ValidateField(colName, invalidNumber);
            string hasPlus = UserController.ValidateField(colName, plus);
            string hasDash = UserController.ValidateField(colName, dash);
            string isNumber = UserController.ValidateField(colName, number);

            Assert.AreEqual(UserController.PHONE_NUMBER_ERROR_MESSAGE, isNull);
            Assert.AreEqual(UserController.PHONE_NUMBER_ERROR_MESSAGE, isEmpty);
            Assert.AreEqual(UserController.PHONE_NUMBER_ERROR_MESSAGE, isText);
            Assert.AreEqual(UserController.PHONE_NUMBER_ERROR_MESSAGE, isInvalidNumber);
            Assert.IsNull(hasPlus);
            Assert.IsNull(hasDash);
            Assert.IsNull(isNumber);
        }

        [TestMethod]
        public void ValidationDateOfBirth()
        {
            string colName = "DateOfBirth";

            string strNull = null;
            string strEmpty = "";
            string text = "DOB";
            string invalidNumber = "10102000";
            string future = DateTime.Today.AddDays(1).ToString();
            string withSlash = "10/10/2000";
            string withDash = "10-10-2000";
            string withDot = "10.10.2000";
            string shortDate = "10/10/20";

            string isNull = UserController.ValidateField(colName, strNull);
            string isEmpty = UserController.ValidateField(colName, strEmpty);
            string isText = UserController.ValidateField(colName, text);
            string isInvalidNumber = UserController.ValidateField(colName, invalidNumber);
            string isFuture = UserController.ValidateField(colName, future);
            string isSlash = UserController.ValidateField(colName, withSlash);
            string isDash = UserController.ValidateField(colName, withDash);
            string isDot = UserController.ValidateField(colName, withDot);
            string isShortDate = UserController.ValidateField(colName, shortDate);

            Assert.AreEqual(UserController.FUTURE_DATETIME_ERROR_MESSAGE, isFuture);
            Assert.AreEqual(UserController.INVALID_DATETIME_ERROR_MESSAGE, isNull);
            Assert.AreEqual(UserController.INVALID_DATETIME_ERROR_MESSAGE, isEmpty);
            Assert.AreEqual(UserController.INVALID_DATETIME_ERROR_MESSAGE, isText);
            Assert.AreEqual(UserController.INVALID_DATETIME_ERROR_MESSAGE, isInvalidNumber);
            Assert.IsNull(isSlash);
            Assert.IsNull(isDash);
            Assert.IsNull(isDot);
            Assert.IsNull(isShortDate);
        }
    }
}
