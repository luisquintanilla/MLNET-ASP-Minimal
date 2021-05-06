global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.ML.Data;
global using Microsoft.Extensions.ML;

public class ModelInput
{
    public string SentimentText;

    [ColumnName("Label")]
    public bool Sentiment;
}

public class ModelOutput
{
    [ColumnName("PredictedLabel")]
    public bool Prediction { get; set; }

    public float Probability { get; set; }

    public float Score { get; set; }
}