using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utilities
{
    class BesiegedException : Exception
    {
        public enum ErrorCode { ResourceNotFound };
        
        public BesiegedException(ErrorCode errorCode) 
        { 
            
        }
    }
}
