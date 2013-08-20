
namespace Dolstagis.Contrib.Auth.Passwords
{
    /// <summary>
    ///  Represents the results of checking a password.
    /// </summary>

    public enum PasswordResult
    {
        /// <summary>
        ///  The password was incorrect.
        /// </summary>

        Incorrect,

        /// <summary>
        ///  The password provider was unable to validate this password, because
        ///  it was encrypted by a different password provider.
        /// </summary>

        Unrecognised,

        /// <summary>
        ///  The password is correct, but was not hashed using the password
        ///  provider's optimal settings. It should be upgraded.
        /// </summary>

        CorrectButInsecure,

        /// <summary>
        ///  The password was correct and stored with the password provider's
        ///  optimal security settings.
        /// </summary>

        Correct
    }
}
