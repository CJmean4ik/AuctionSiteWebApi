using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuctionSite.DataAccess.Components.UpdateComponents
{
    public class ModifierArgumentChanger<T, C> : IModifierArgumentChanger<T, C> 
        where T : class, new()
        where C : DbContext, new()

    {
        private List<Func<T, PropertyInfo>> _valuesChanger;

        public ModifierArgumentChanger()
        {
            _valuesChanger = new List<Func<T, PropertyInfo>>();
        }

        public void SearchModifierProperty(T oldEntity, T newEntity)
        {
            Type enityType = typeof(T);
            var properties = enityType.GetProperties()
                .Where(prop => prop.GetCustomAttribute<IgnoreDuringSelectionAttribute>() == null);

            foreach (var property in properties)
            {
                var prop = enityType.GetProperty(property.Name)!;
                var oldValue = prop.GetValue(oldEntity);
                var newValue = prop.GetValue(newEntity);

                if (newValue != default && !oldValue!.Equals(newValue))
                {
                    _valuesChanger.Add((oldChangeEntity) =>
                    {
                        prop.SetValue(oldChangeEntity, newValue);
                        return prop;
                    });
                }
            }
        }

        public void ChangeAndAttachValue(C context, T oldEntity)
        {
            foreach (var valueChanger in _valuesChanger)
            {
                var prop = valueChanger.Invoke(oldEntity);
                context.Entry(oldEntity).Property(prop.Name).IsModified = true;
            }
        }
    }
}
