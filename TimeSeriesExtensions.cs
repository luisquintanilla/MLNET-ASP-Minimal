using System.IO;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using Microsoft.Extensions.DependencyInjection;

public static class TimeServiceExtensions
{
    public static void AddTimeSeriesPredictionEngine(this IServiceCollection services, string modelPath)
    {       
        services.AddSingleton<TimeSeriesPredictionEngine<ModelInput,ModelOutput>>(opt => 
        {
            MLContext ctx = new MLContext();
            var model =  ctx.Model.Load(modelPath, out var Schema);
            var engine = model.CreateTimeSeriesEngine<ModelInput,ModelOutput>(ctx);
            return engine;            
        });
    }
}