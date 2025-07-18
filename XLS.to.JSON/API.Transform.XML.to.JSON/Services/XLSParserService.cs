﻿using ExcelDataReader;
using System.Data;

namespace API.Transform.XML.to.JSON.Services;

public class XLSParserService : IXLSParserService
{
    public List<Dictionary<string, object>> ParseXLS(IFormFile file)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using var stream = file.OpenReadStream();
        using var reader = ExcelReaderFactory.CreateReader(stream);

        var result = reader.AsDataSet(new ExcelDataSetConfiguration
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration
            {
                UseHeaderRow = true
            }
        });

        var table = result.Tables[0];
        var rows = new List<Dictionary<string, object>>();

        foreach (DataRow row in table.Rows)
        {
            var dict = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            rows.Add(dict);
        }

        return rows;
    }

    public Dictionary<string, List<Dictionary<string, object>>> ParseAllSheets(IFormFile file)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using var stream = file.OpenReadStream();
        using var reader = ExcelReaderFactory.CreateReader(stream);

        var result = reader.AsDataSet(new ExcelDataSetConfiguration
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration
            {
                UseHeaderRow = true
            }
        });

        var allSheets = new Dictionary<string, List<Dictionary<string, object>>>();

        foreach (DataTable table in result.Tables)
        {
            var rows = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = row.IsNull(col) ? null : row[col]; //Aqui ele coloca null

                }
                rows.Add(dict);
            }

            allSheets[table.TableName] = rows;
        }

        return allSheets;
    }
}
