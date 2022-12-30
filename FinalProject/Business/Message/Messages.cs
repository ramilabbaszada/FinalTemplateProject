using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Message
{
    public static class Messages
    {
        public static string ProductAdded = "Product Added";
        public static string ProductNameeInvalid = "Invalid Product Name";
        public static string MaintenanceTime = "System Maintenance";
        public static string ProductsListed = "Products Listed";
        public static string ProductCountOfCategoryError = "Product Count is out of available range";
        public static string ProductNameExistsInDatabase ="The same product name is already existed in database";
        public static string EnaughCategories = "Category Error";
        public static string AuthorizationDenied = "Authorization Denied ";
        public static string AccessTokenCreated = "Access Token Created";
        public static string UserAlreadyExists = "User Already Exists";
        public static string SuccessfulLogin = "Successful Login";
        public static string PasswordError = "Password Error";
        public static string UserNotFound = "User Not Foound";
        public static string UserRegistered = "User Registered";
        public static string CategoryLimitHasNotBeenReached = "Products category limit has not been reached yet";
        public static string ProductNameDoesNotExistInDatabase = "Product name deos not exist in database";
    } 
}
