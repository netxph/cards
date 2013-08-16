using Cards.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cards.Web.Controllers.Api
{
    public class LabelsController : ApiController
    {

        public Label Post(Label label)
        {
            return Label.Create(label.Name, label.Color);
        }

    }
}
