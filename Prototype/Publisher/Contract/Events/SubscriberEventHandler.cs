using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Publisher.Contract.Events
{
    internal delegate void SubscriberEventHandler(object sender, SubscriberEventArgs e);
}
