using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture.Commands
{
    public class SmartCommand : CommandBase
    {
        private Action func;
        public SmartCommand(Action Func)
        {
            func = Func;
        }

        public override void Execute(object parameter)
        {
            func();
        }
    }
}
