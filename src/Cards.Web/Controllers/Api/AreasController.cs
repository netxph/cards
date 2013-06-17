using Cards.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cards.Web.Controllers.Api
{
    public class AreasController : ApiController
    {
        // GET api/areas
        public List<Area> Get()
        {
            return Area.GetAll();
        }

    }
}
