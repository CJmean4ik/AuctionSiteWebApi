using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuctionSite.DataAccess.Components.UpdateComponents
{
    public class ModifierArgumentChangerDecorator<C> : IModifierArgumentChanger<C>
        where C : DbContext, new()
    {
        IModifierArgumentChanger<C> _modifierArgument;

        public ModifierArgumentChangerDecorator(IModifierArgumentChanger<C> modifierArgument)
        {
            _modifierArgument = modifierArgument;
        }

        public IEnumerable<PropertyInfo> MarkedModifierProperty<T1>(T1 oldEntity, T1 newEntity, C context)
        {
            var changedProperties = _modifierArgument.MarkedModifierProperty<T1>(oldEntity, newEntity, context);

            var durationSaleProp = changedProperties.Where(w => w.Name == "DurationSale")
                                                     .FirstOrDefault();

            if (durationSaleProp == null)
                return changedProperties;

            var newValue = durationSaleProp.GetValue(newEntity);
            var oldValue = durationSaleProp.GetValue(oldEntity);

            ChangeDurationSaleAndEndDate<T1>(durationSaleProp, oldEntity, newValue, context);
            return changedProperties;
        }
        private void ChangeDurationSaleAndEndDate<T1>(PropertyInfo property, T1 oldEntity, object? newValue, C context)
        {
            Type enityType = typeof(T1);

            PropertyInfo startDateProp = enityType.GetProperty("StartDate")!;
            PropertyInfo endDateProp = enityType.GetProperty("EndDate")!;

            var datetime = startDateProp.GetValue(oldEntity)!.ToString()!;
            DateTime endDate = DateTime.Parse(datetime);
            endDate = endDate.AddDays(int.Parse(newValue.ToString()!));
            endDateProp.SetValue(oldEntity, endDate);

            context.Entry(oldEntity!).Property(endDateProp.Name).IsModified = true;
        }
    }
}
