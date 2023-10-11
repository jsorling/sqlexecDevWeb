using Sorling.SqlExecMeta;

namespace Sorling.sqlexecDevWeb.extensions.sqlobjects;

public static class ISqlItemExtensions
{
   public static string FQItemName(this ISqlItem sqlItem) => sqlItem.SchemaName + '.' + sqlItem.Name;
}
