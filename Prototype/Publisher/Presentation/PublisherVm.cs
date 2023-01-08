using Prototype.Publisher.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Publisher.Presentation
{
    internal class PublisherVm
    {
        private readonly PublisherM _model;

        internal PublisherVm()
        {
            _model = new PublisherM();
        }

        internal void Start()
        {
            // Bl staren und Event Registrieren?
            throw new NotImplementedException();
        }
    }
}
