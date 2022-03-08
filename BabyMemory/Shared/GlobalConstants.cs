﻿namespace SharedTrip.Shared
{
    internal class GlobalConstants
    {
        //RegisterInputModel
        public const int UserNameMAxLen = 100;
        public const int UserNameMinLen = 4;
        public const int PasswordMinLen = 6;
        public const int PasswordMaxLen = 100;
        public const string NameErrorMsg = "The {0} must be at least {2} and at max {1} characters long.";
        public const string PasswordNotMach = "The password and confirmation password do not match.";
        
        //User
        public const int UserFullNameMAxLenDb = 254;
        public const int UrlMaxLen = 2048;

        //Child
        public const int UserNameMaxLenDb = 100;

        //Memory
        public const int MemoryNameMaxLen = 254;
        public const int MemoryDescriptionMaxLen = 2048;

        //HealthProcedure
        public const int HealthProcedureNameMAxLenDb = 254;

        //Event
        public const int EventNameMaxLen = 254;
        public const int EventDescriptionMaxLen = 2048;


        public const int IdGuidMaxLen = 36;
        public const int CarModelMAxLen = 20;
        public const int HashedPasswordMAxLen = 64;
        public const int ProductNameMAxLen = 20;
        public const int ProductNameMinLen = 4;
        public const int DescriptionMaxLen = 80;
        public const int SeatMinLen = 2;
        public const int SeatMaxLen = 6;
        public const double PriceMinLen = 0.05;
        public const double PriceMaxLen = 1000;
        public const string SeatsErrorMsg = "{0} must be between {1} and {2}";
        public const string EmailError = "Email must be valid.";
        public const string UserNameExist = "User alredy exist.";
        public const string ThimeError = "DepartureTime is not in correct format.";
        public const string UserTypeError = "Value must be: 'Client' or 'Mechanic'.";
    }
}