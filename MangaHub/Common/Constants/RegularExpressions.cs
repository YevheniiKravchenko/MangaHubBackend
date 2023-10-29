namespace Common.Constants
{
    public static class RegularExpressions
    {
        public const string Login = @"^[a-zA-Z0-9_-]{4,32}$";

        public const string Password = @"^[a-zA-Z0-9!@#$%^&*-_]{4,32}$";

        public const string PhoneNumber = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";
    }
}