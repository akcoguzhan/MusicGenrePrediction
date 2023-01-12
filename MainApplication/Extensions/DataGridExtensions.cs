using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Types;
using Types.Attributes;

namespace MainApplication
{
    public static class DataGridExtensions
    {
        public static DataGridRow GetRow(this DataGrid grid, int index)
        {
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.Items[index]);
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static DataGridCell GetCell(this DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = Visual<DataGridCellsPresenter>.GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = Visual<DataGridCellsPresenter>.GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        public static void FormatCellsIfHasFigure(this DataGrid grid)
        {
            for (int i = 0; i < grid.Items.Count; i++)
            {
                var gridRow = grid.GetRow(i);
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    var cell = grid.GetCell(gridRow, j);
                    if(cell is not null && cell.Visibility == Visibility.Visible)
                    {
                        var attr = TypeDescriptor.GetProperties(((IList)grid.ItemsSource)[i].GetType())[j].Attributes[typeof(FeatureHasPlotAttribute)];
                        if (((FeatureEntity)cell.DataContext).HasFigures && attr != null)
                        {
                            var plotData = ((FeatureHasPlotAttribute)attr);
                            Style cellStyle = new Style();
                            cellStyle.TargetType = cell.GetType();
                            Setter setter = new Setter();
                            setter.Property = Control.BackgroundProperty;
                            setter.Value = new SolidColorBrush(Color.FromRgb(r: plotData.R, g: plotData.G, b: plotData.B));
                            cellStyle.Setters.Add(setter);
                            cell.Style = cellStyle;
                        }
                    }
                }
            }
        }
    }
}
