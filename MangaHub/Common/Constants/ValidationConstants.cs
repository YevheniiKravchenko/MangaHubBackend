namespace Common.Constants
{
    public static class ValidationConstant
    {
        #region User

        public const int NameMinLength = 4;
        public const int NameMaxLength = 50;

        public const int LoginMinLength = 4;
        public const int LoginMaxLength = 32;

        public const int DescriptionMinLength = 15;
        public const int DescriptionMaxLength = 500;

        public const int PasswordMinLength = 4;
        public const int PasswordMaxLength = 32;

        public const int EmailMinLength = 5;

        #endregion

        #region Manga

        public const int TitleMinLength = 10;
        public const int TitleMaxLength = 128;

        #endregion
    }
}