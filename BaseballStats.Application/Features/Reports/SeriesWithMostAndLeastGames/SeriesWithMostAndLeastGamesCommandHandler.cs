using BaseballStats.Application.Utilities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using iText.Layout.Element;

namespace BaseballStats.Application.Features.Reports.SeriesWithMostAndLeastGames;

public class SeriesWithMostAndLeastGamesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<SeriesWithMostAndLeastGamesCommand, FileInfo>
{
   public override async Task<FileInfo> ExecuteAsync(SeriesWithMostAndLeastGamesCommand command, CancellationToken ct = default)
   {
        var series_table = unitOfWork.Repository<Domain.Entities.Series>().DbSet;  
        var game_table = unitOfWork.Repository<Domain.Entities.Game>().DbSet;

        var gamesBySeries = (
            from game in game_table
            join series in series_table on game.SeriesId equals series.Id
            group game by new { game.SeriesId, SeriesName = series.Name, series.SeasonId } into g
            select new {
                g.Key.SeriesId,
                g.Key.SeasonId,
                g.Key.SeriesName,
                AmountOfGames = g.Count()
            }
        );

        var seriesDataBySeason = (
            from series in gamesBySeries
            group series by series.SeasonId into g
            select new {
               SeasonId = g.Key,
               SeriesWithMostGames = g.OrderByDescending(x => x.AmountOfGames).Select(x => new {x.SeriesName, Amount = x.AmountOfGames}).First(),
               SeriesWithLeastGames = g.OrderBy(x => x.AmountOfGames).Select(x => new {x.SeriesName, Amount = x.AmountOfGames}).First()
            }
        ).ToList();

        // Season | Series With Most games | Amount of Games |  Series with least games | Amount of Games 
   
        var document = DocumentTools.OpenDocument("reports", "SeriesWithMostAndLeastGames.pdf");

        document.AddTitle("Series With Most And Least Games Played By Season Report");
        
        document.AddLine();

        document.Add(new Paragraph("\n"));

        var table = new Table(5);
        table.AddCell(new Cell().Add(new Paragraph("Season")));
        table.AddCell(new Cell().Add(new Paragraph("Series With Most Games")));
        table.AddCell(new Cell().Add(new Paragraph("Amount of Games")));
        table.AddCell(new Cell().Add(new Paragraph("Series With Least Games")));
        table.AddCell(new Cell().Add(new Paragraph("Amount of Games")));
        
        foreach (var series in seriesDataBySeason)
        {
            table.AddCell(new Cell().Add(new Paragraph(series.SeasonId.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(series.SeriesWithMostGames.SeriesName.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(series.SeriesWithMostGames.Amount.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(series.SeriesWithLeastGames.SeriesName.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(series.SeriesWithLeastGames.Amount.ToString())));
        }

        document.Add(table);

        document.Close();

        await Task.CompletedTask;

        return new FileInfo("reports/SeriesWithMostAndLeastGames.pdf");
    }
}

