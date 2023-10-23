namespace DAL.Infrastructure.Helpers
{
    public static class HashHelper
    {
        private static readonly Random random = new((int)(DateTime.Now.Ticks % 181081));
        private static string symbols => "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm[]'/.,{}:\"<>?`1234567890-=~!@#$%^&*()_+\\|";
        private static string defaultSalt => "98+_)+(_+a?}\">?\\\"kf98bvsocn01234-)(U^)QWEJOFkn9uwe0tj)ASDJF)(H0INHO$%uh09hj";
        
        public static (string salt, string passwordHash) GenerateNewPasswordHash(string newPassword)
        {
            const int saltLength = 16;
            var salt = new string(Enumerable.Repeat(symbols, saltLength)
                .Select(s => s[random.Next(s.Length)]).ToArray()
            );

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword + salt + defaultSalt);

            return (salt, passwordHash);
        }

        public static bool VerifyPassword(string password, string salt, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password + salt + defaultSalt, passwordHash);
        }
    }
}
