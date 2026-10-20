using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Framework
{
    internal class NullDriver : Driver
    {
        private readonly ObjectContext objectContext;
        public NullDriver(ObjectContext objectContext) : base(null, null, null, objectContext)
        {
            this.objectContext = objectContext;
        }
    }
}