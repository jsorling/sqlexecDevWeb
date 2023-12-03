using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.constraints;

namespace Sorling.sqlexecDevWeb.extensions.sqlobjects;

public static class SqlDefaultConstraintListItemExtensions
{
   public static ISqlItem GetTableSqlItem(this SqlDefaultConstraintListItem defaultConstraint) {
      ArgumentException.ThrowIfNullOrEmpty(defaultConstraint.ParentName, nameof(defaultConstraint.ParentName));
      ArgumentException.ThrowIfNullOrEmpty(defaultConstraint.ParentSchema, nameof(defaultConstraint.ParentSchema));

      return new CraftedSqlItem(defaultConstraint.Parent_object_id, defaultConstraint.ParentName
         , defaultConstraint.ParentSchema, SqlGroupFlags.Tables, defaultConstraint.DBName);
   }
}
