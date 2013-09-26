﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Routing;

namespace Dolstagis.Web.Static
{
    public class StaticFileHandler
    {
        private IMimeTypes _mimeTypes;
        private IEnumerable<IResourceLocator> _resources;

        public StaticFileHandler(IMimeTypes mimeTypes, IEnumerable<IResourceLocator> resources)
        {
            this._mimeTypes = mimeTypes;
            this._resources = resources;
        }

        [Parameters("path")]
        public object Get(string path)
        {
            return null;
        }
    }
}
