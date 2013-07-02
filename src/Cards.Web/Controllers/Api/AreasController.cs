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

        public List<Area> GetAll()
        {
            return Area.GetAll();
        }

        public Area Create(Area area)
        {
            return Area.Create(area.Name);
        }

        public Area Delete(int id)
        {
            return Area.Delete(id);
        }

    }
}
