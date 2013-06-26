using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public interface ICardRepository
    {

        Area CreateArea(Area area);
        List<Area> FindAllArea();

        Card FindCard(int id);
        Card CreateCard(Card card);
        Card UpdateCard(Card card);

    }
}
