using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadatak2
{
    public class DuplicateTodoItemException : Exception
    {
        private Guid _itemId;

        public DuplicateTodoItemException(Guid itemId)
        {
            _itemId = itemId;
        }

        public override string Message
        {
            get { return $" duplicate id: {_itemId }"; }
        }
    }
}
