using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Xyaneon.Games.ConwaysGameOfLife.Avalonia.Models;

namespace Xyaneon.Games.ConwaysGameOfLife.Avalonia.Controls;

public class LifeGridDisplay : Control
{
    public LifeGridDisplay()
    {
        _cellOutlineBrush = Brushes.Gray;
        _cellOutlinePen = new Pen(_cellOutlineBrush);
        _livingCellBrush = Brushes.Black;
        _deadCellBrush = Brushes.White;
    }

    private IBrush _cellOutlineBrush;
    private IPen _cellOutlinePen;
    private IBrush _livingCellBrush;
    private IBrush _deadCellBrush;
    private GameOfLifeState? _state;

    public static readonly DirectProperty<LifeGridDisplay, GameOfLifeState?> StateProperty =
        AvaloniaProperty.RegisterDirect<LifeGridDisplay, GameOfLifeState?>(
            nameof(State),
            lifeGridDisplay => lifeGridDisplay.State,
            (lifeGridDisplay, setterValue) => lifeGridDisplay.State = setterValue);

    public GameOfLifeState? State
    {
        get => _state;
        set => SetAndRaise(StateProperty, ref _state, value);
    }

    public override void Render(DrawingContext context)
    {
        if (State is not null)
        {
            DrawGridState(context);
        }
        else
        {
            DrawPlaceholderText(context);
        }
    }

    private void DrawGridState(DrawingContext context)
    {
        double controlWidth = this.Bounds.Width;
        double controlHeight = this.Bounds.Height;

        double cellWidth = controlWidth / State!.ColumnCount;
        double cellHeight = controlHeight / State!.RowCount;

        for (int x = 0; x < State!.ColumnCount; x++)
        {
            for (int y = 0; y < State!.RowCount; y++)
            {
                double left = x * cellWidth;
                double top = y * cellHeight;
                var cellBorderRect = new Rect(left, top, cellWidth, cellHeight);
                context.DrawRectangle(null, _cellOutlinePen, cellBorderRect);

                if (State!.IsCellAliveAt(y, x))
                {
                    int cellPadding = 1;
                    var cellInteriorRect = ShrinkCenteredRect(cellBorderRect, cellPadding);
                    context.DrawRectangle(_livingCellBrush, null, cellInteriorRect);
                }
            }
        }
    }

    private void DrawPlaceholderText(DrawingContext context)
    {
        double controlWidth = this.Bounds.Width;
        double controlHeight = this.Bounds.Height;

        var text = new FormattedText
        {
            Text = "Open a pattern file to start",
            TextAlignment = TextAlignment.Center,
            Typeface = new Typeface(FontFamily.Default),
            FontSize = 16,
        };

        double renderedTextWidth = text.Bounds.Width;
        double renderedTextHeight = text.Bounds.Height;

        var textPoint = new Point(controlWidth / 2 - renderedTextWidth / 2, controlHeight / 2 - renderedTextHeight / 2);

        context.DrawText(Brushes.DarkGray, textPoint, text);
    }

    private static Rect ShrinkCenteredRect(Rect originalRect, double amountToShrinkBy)
    {
        double newX = originalRect.X + amountToShrinkBy;
        double newY = originalRect.Y + amountToShrinkBy;
        double newWidth = originalRect.Width - amountToShrinkBy * 2;
        double newHeight = originalRect.Height - amountToShrinkBy * 2;

        return new Rect(newX, newY, newWidth, newHeight);
    }
}
