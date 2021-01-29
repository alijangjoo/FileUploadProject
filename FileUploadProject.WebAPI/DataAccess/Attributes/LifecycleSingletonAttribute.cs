using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadProject.WebAPI.DataAccess.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class LifecycleSingletonAttribute : System.Attribute
    {
        public double version;

        public LifecycleSingletonAttribute()
        {
            version = 1.0;
        }
    }
}
