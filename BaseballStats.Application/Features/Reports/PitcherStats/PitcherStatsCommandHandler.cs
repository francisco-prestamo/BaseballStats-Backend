using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Pitcher.GetAll;
using BaseballStats.Application.Utilities;
using FastEndpoints;
using iText.Layout.Element;

namespace BaseballStats.Application.Features.Reports.PitcherStats;

public class PitcherStatsCommandHandler : CommandHandler<PitcherStatsCommand, FileInfo>
{
    public override async Task<FileInfo> ExecuteAsync(PitcherStatsCommand command, CancellationToken ct = default(CancellationToken))
    {
        var data = await GetData(ct);
        var docName = "pitcher-stats.pdf";

        var document = DocumentTools.OpenDocument("reports", docName);

        document.AddTitle("Pitcher Statistics");

        document.AddLine();

        document.Add(new Paragraph("\n"));
        
        var table = new Table(4);
        table.AddCell(new Cell().Add(new Paragraph("ID")));
        table.AddCell(new Cell().Add(new Paragraph("Name")));
        table.AddCell(new Cell().Add(new Paragraph("Amount of Won Games")));
        table.AddCell(new Cell().Add(new Paragraph("Allowed Runs Average")));

        foreach (var pitcherDto in data)
        {
            table.AddCell(new Cell().Add(new Paragraph(pitcherDto.Id.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(pitcherDto.Name.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(pitcherDto.GamesWonNumber.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(pitcherDto.AllowedRunsAvg.ToString())));
        }

        document.Add(table);

        document.Close();

        return new FileInfo("reports/" + docName);
    }

    private static async Task<List<PitcherDto>> GetData(CancellationToken ct)
    {
        var data = await new GetAllPitchersCommand().ExecuteAsync(ct);

        return data;
    }

}

