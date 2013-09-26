using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Static
{
    public interface IResource
    {
        DateTime DateModified { get; }

        long Length { get; }

        string Name { get; }

        Stream Open();
    }
}
