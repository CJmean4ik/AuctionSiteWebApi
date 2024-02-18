using Microsoft.EntityFrameworkCore;

namespace AuctionSite.DataAccess.Components.UpdateComponents
{
    public interface IModifierArgumentChanger<T, C>
        where T : class, new()
        where C : DbContext, new()
    {
        void ChangeAndAttachValue(C context, T oldEntity);
        void SearchModifierProperty(T oldEntity, T newEntity);
    }
}