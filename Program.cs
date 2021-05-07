using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;

// using Microsoft.Extensions.ML;

// Configure app
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTimeSeriesPredictionEngine("WeatherForecastModel.zip");

var app = builder.Build();

// Define prediction route & handler
app.MapPost("/predict",
    (Func<Task<ModelOutput>>)
    (async () =>
    {
        var engine = app.Services.GetRequiredService<TimeSeriesPredictionEngine<ModelInput,ModelOutput>>();
        return await Task.FromResult(engine.Predict(horizon: 7));
    }));

// Run app
app.Run();

public class ModelInput
{
    [LoadColumn(6)]
    public DateTime Date { get; set; }

    [LoadColumn(7)]
    public float MaxTemp { get; set; }
}

public class ModelOutput
{
    // Maximum Temperature (Farenheit). Each element is a day in the future it's forecasting for
    public float[] ForecastTemp { get; set; }

    public float[] LowerBoundTemp { get; set; }

    public float[] UpperBoundTemp { get; set; }
}