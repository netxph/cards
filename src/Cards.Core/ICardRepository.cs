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
        Area FindArea(int id);
        Area UpdateArea(Area area);

        Card FindCard(int id);
        Card CreateCard(Card card);
        Card UpdateCard(Card card);
        
        Activity CreateActivity(Activity activity);

        Label CreateLabel(Label label);
        List<Label> FindAllLabels();

        Label DeleteLabel(Label label);

        User CreateUser(User user);
        User FindUser(int id);
        User UpdateUser(User user);
    }
}
