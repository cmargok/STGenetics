namespace STGenetics.Application.Security
{
    public interface ITokenService{

        /// <summary>
        /// Validate if the name and user given are equal to the local user allowed
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true</returns>
        public bool ValidateUser(User user);

        /// <summary>
        ///Gets the token to access the api endpoints
        /// </summary>
        /// <returns>Token</returns>
        public Task<string> GetToken();
    }
}
