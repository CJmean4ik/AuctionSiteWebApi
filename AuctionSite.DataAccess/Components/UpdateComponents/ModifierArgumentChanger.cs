using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuctionSite.DataAccess.Components.UpdateComponents
{
    public class ModifierArgumentChanger<C> : IModifierArgumentChanger<C>
        where C : DbContext, new()

    {
        public IEnumerable<PropertyInfo> MarkedModifierProperty<T>(T oldEntity, T newEntity, C context)
        {
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            Type enityType = typeof(T);
            var properties = enityType.GetProperties()
                .Where(prop => prop.GetCustomAttribute<IgnoreDuringSelectionAttribute>() == null);

            foreach (var property in properties)
            {
                var prop = enityType.GetProperty(property.Name)!;
                var oldValue = prop.GetValue(oldEntity);
                var newValue = prop.GetValue(newEntity);
              
                if ((newValue is DateTime time) && time == DateTime.Parse("01.01.0001 00:00:00"))
                    continue;
               
                if (newValue != default && !oldValue!.Equals(newValue))
                {
                    prop.SetValue(oldEntity, newValue);
                    context.Entry(oldEntity!).Property(prop.Name).IsModified = true;
                    propertyInfos.Add(property);
                }
            }
            return propertyInfos;
        }
    }
}
