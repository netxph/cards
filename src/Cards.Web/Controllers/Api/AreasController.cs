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

        public List<AreaView> GetAll([FromUri] FilterCardRequest request)
        {
            return Area.Find(request);
        }

        public Area Get(int id)
        {
            return Area.Get(id);
        }

        public Area Post(Area area)
        {
            return Area.Create(area.Name);
        }

        public Area Put(int id, Area area)
        {
            return Area.Update(id, area.Name);
        }

        public Area Delete(int id)
        {
            return Area.Delete(id);
        }

    }
}
