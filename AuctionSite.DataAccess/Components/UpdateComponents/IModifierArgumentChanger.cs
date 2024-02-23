using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuctionSite.DataAccess.Components.UpdateComponents
{
    public interface IModifierArgumentChanger<C>
        where C : DbContext, new()
    {
       IEnumerable<PropertyInfo> MarkedModifierProperty<T>(T oldEntity, T newEntity, C context);
    }
}