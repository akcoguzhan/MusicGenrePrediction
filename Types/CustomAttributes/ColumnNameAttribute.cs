namespace Types.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ColumnNameAttribute : Attribute
{
    #region Properties
    #region Name
    public string? columnName;

    public string? ColumnName
    {
        get
        {
            return columnName;
        }
        set
        {
            if (columnName != value)
            {
                columnName = value;
            }
        }
    }

    #endregion Name
    #endregion Properties

    #region Constructors
    public ColumnNameAttribute(string columnName)
    {
        this.ColumnName = columnName;
    }
    #endregion Constructors
}
