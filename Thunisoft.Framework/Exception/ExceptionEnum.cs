using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunisoft.Framework
{
    public enum ExceptionType
    {

    }
    public enum ExceptionLevel
    {
        //记录log即可
        Slight,
        //需要有提示框
        Moderate,
        //程序要强行退出
        Serious
    }
}
