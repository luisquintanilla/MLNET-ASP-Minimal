// Configure app
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPredictionEnginePool<ModelInput,ModelOutput>()
    .FromUri("https://github.com/dotnet/samples/raw/main/machine-learning/models/sentimentanalysis/sentiment_model.zip");

var app = builder.Build();

// Define prediction route & handler
app.MapPost("/predict", 
    async ([FromServices] PredictionEnginePool<ModelInput,ModelOutput> predictionEnginePool, ModelInput input) => 
        await Task.FromResult(predictionEnginePool.Predict(input)));

// Run app
app.Run();