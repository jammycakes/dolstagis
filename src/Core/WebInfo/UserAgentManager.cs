using System.Linq;
using Dolstagis.Core.Data;

namespace Dolstagis.Core.WebInfo
{
    public class UserAgentManager : Manager<UserAgent>
    {
        public UserAgentManager(IRepository<UserAgent> repository)
            : base(repository)
        { }


        /// <summary>
        ///  Gets a <see cref="UserAgent"/> instance from the database, by user agent string.
        /// </summary>
        /// <param name="uaString">
        ///  The user agent string.
        /// </param>
        /// <returns>
        ///  A <see cref="UserAgent"/> instance.
        /// </returns>

        public UserAgent GetUserAgent(string uaString)
        {
            /*
             * The intention is that user agents should only be looked up when creating a new
             * session, i.e. when logging on. Since this is already an expensive operation
             * (due to the cost of checking passwords), there's little or no reason to optimise
             * this for performance.
             * 
             * There is a small risk of a race condition here if two users log on at the same
             * time, both with a new, unrecognised browser. In this case, two references to
             * the user agent concerned will be stored in the database, but only the first will
             * be used. This data can be cleaned up manually every so often if need be.
             * For this reason, you should NOT put a unique constraint on the user agent string.
             */

            var result = this.Repository.Query().Where(x => x.String == uaString).OrderBy(x => x.UserAgentID);
            if (result.Any()) {
                return result.First();
            }
            else {
                var ua = new UserAgent() {
                    String = uaString
                };
                this.Repository.Save(ua);
                return ua;
            }
        }
    }
}
