using Cards.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cards.Web.Controllers.Api
{
    public class CardsController : ApiController
    {

        public Card Post(Card card)
        {
            return Card.Create(card.Name, card.AreaID);
        }

        public Card Put(int id, Card card)
        {
            return Card.Update(id, card.Name, card.AreaID);
        }

    }
}
